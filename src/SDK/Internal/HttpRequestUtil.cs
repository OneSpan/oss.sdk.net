using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
#if NET5_0_OR_GREATER
using System.Net.Http;
#endif

namespace OneSpanSign.Sdk.Internal
{
    public class HttpRequestUtil
    {
        public const string SESSION_TOKEN_COOKIE_VALUE_KEY = "ESIGNLIVE_SESSION_ID";
        public const string TEMP_SESSION_TOKEN_COOKIE_VALUE_KEY = "ESIGNLIVE_TEMP_TOKEN";
        public const string SESSION_TOKEN_COOKIE_KEY = "Cookie";

        public const string API_KEY_AUTHENTICATION_AUTHORIZATION_KEY = "Authorization";
        public const string API_KEY_AUTHENTICATION_BASIC_PREFIX = "Basic ";

#if NET5_0_OR_GREATER
        // ── .NET 5+ implementation ─────────────────────────────────────────────

        public static string GetUrlContent(string requestedURL)
        {
            try
            {
                var handler = new HttpClientHandler { AllowAutoRedirect = false };
                using var client = new HttpClient(handler);

                using var firstResponse = client.Send(new HttpRequestMessage(HttpMethod.Get, requestedURL));

                string location = firstResponse.Headers.Location?.ToString();
                string setCookieHeader = firstResponse.Headers.TryGetValues("Set-Cookie", out var cookieValues)
                    ? string.Join(";", cookieValues)
                    : "";

                string cookieSessionToken = ExtractCookieValue(setCookieHeader, SESSION_TOKEN_COOKIE_VALUE_KEY);
                string cookieTempTokenValue = ExtractCookieValue(setCookieHeader, TEMP_SESSION_TOKEN_COOKIE_VALUE_KEY);

                var redirectRequest = new HttpRequestMessage(HttpMethod.Get, location);
                SetAuthentication(redirectRequest, AuthRequestParameters.empty());
                redirectRequest.Headers.TryAddWithoutValidation(SESSION_TOKEN_COOKIE_KEY,
                    BuildSessionTokenCookieValue(cookieSessionToken) + ";" + BuildTempTokenCookieValue(cookieTempTokenValue));

                using var response = client.Send(redirectRequest);

                if (response.IsSuccessStatusCode)
                {
                    var charset = response.Content.Headers.ContentType?.CharSet;
                    var encoding = charset != null ? Encoding.GetEncoding(charset) : Encoding.UTF8;
                    using var stream = response.Content.ReadAsStream();
                    using var reader = new StreamReader(stream, encoding);
                    return reader.ReadToEnd();
                }

                return "";
            }
            catch (OssServerException) { throw; }
            catch (HttpRequestException e)
            {
                throw new OssException("Error communicating with oss server. " + e.Message, e);
            }
            catch (Exception e)
            {
                throw new OssException("Error communicating with oss server. " + e.Message, e);
            }
        }

        private static void SetAuthentication(HttpRequestMessage request, AuthRequestParameters authRequestParameters)
        {
            if (authRequestParameters.hasSessionToken())
                request.Headers.TryAddWithoutValidation(SESSION_TOKEN_COOKIE_VALUE_KEY, authRequestParameters.getSessionToken());
            else if (authRequestParameters.hasApiKey())
                request.Headers.TryAddWithoutValidation(API_KEY_AUTHENTICATION_BASIC_PREFIX, authRequestParameters.getApiKey());
            else if (authRequestParameters.hasTempToken())
                request.Headers.TryAddWithoutValidation(TEMP_SESSION_TOKEN_COOKIE_VALUE_KEY, authRequestParameters.getTempToken());
            else if (authRequestParameters.hasConnectorsAuth())
                request.Headers.TryAddWithoutValidation(API_KEY_AUTHENTICATION_BASIC_PREFIX, authRequestParameters.getConnectorsAuth());
        }

#else
        // ── netstandard2.0: HttpWebRequest implementation ──────────────────────

        public static string GetUrlContent(string requestedURL)
        {
            string urlContent = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestedURL);
                request.AllowAutoRedirect = false;

                // Make a single first request and extract both Location and Set-Cookie
                string location;
                string cookieSessionToken;
                string cookieTempTokenValue;
                using (WebResponse firstResponse = request.GetResponse())
                {
                    location = firstResponse.Headers["Location"];
                    string setCookieHeader = firstResponse.Headers["Set-Cookie"] ?? "";
                    cookieSessionToken = ExtractCookieValue(setCookieHeader, SESSION_TOKEN_COOKIE_VALUE_KEY);
                    cookieTempTokenValue = ExtractCookieValue(setCookieHeader, TEMP_SESSION_TOKEN_COOKIE_VALUE_KEY);
                }

                HttpWebRequest redirectRequest = (HttpWebRequest)WebRequest.Create(location);
                SetAuthentication(redirectRequest, AuthRequestParameters.empty());
                redirectRequest.Headers.Add(SESSION_TOKEN_COOKIE_KEY,
                    BuildSessionTokenCookieValue(cookieSessionToken) + ";" + BuildTempTokenCookieValue(cookieTempTokenValue));

                using (HttpWebResponse response = (HttpWebResponse)redirectRequest.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        using (StreamReader readStream = response.CharacterSet == null
                            ? new StreamReader(receiveStream)
                            : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)))
                        {
                            urlContent = readStream.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new OssServerException(
                        string.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                            ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                        errorDetails, e);
                }
            }
            catch (Exception e)
            {
                throw new OssException("Error communicating with oss server. " + e.Message, e);
            }
            return urlContent;
        }

        private static void SetAuthentication(HttpWebRequest request, AuthRequestParameters authRequestParameters)
        {
            if (authRequestParameters.hasSessionToken())
                request.Headers.Add(SESSION_TOKEN_COOKIE_VALUE_KEY, authRequestParameters.getSessionToken());
            else if (authRequestParameters.hasApiKey())
                request.Headers.Add(API_KEY_AUTHENTICATION_BASIC_PREFIX, authRequestParameters.getApiKey());
            else if (authRequestParameters.hasTempToken())
                request.Headers.Add(TEMP_SESSION_TOKEN_COOKIE_VALUE_KEY, authRequestParameters.getTempToken());
            else if (authRequestParameters.hasConnectorsAuth())
                request.Headers.Add(API_KEY_AUTHENTICATION_BASIC_PREFIX, authRequestParameters.getConnectorsAuth());
        }

#endif

        // ── Shared helpers (used by both implementations) ──────────────────────

        /// <summary>
        /// Extracts a named cookie value from a raw Set-Cookie header string.
        /// </summary>
        private static string ExtractCookieValue(string setCookieValue, string sessionKeystring)
        {
            if (!string.IsNullOrEmpty(setCookieValue)
                && setCookieValue.Contains(sessionKeystring)
                && HasCookieValue(setCookieValue, sessionKeystring))
            {
                string keyWithEqual = sessionKeystring + "=";
                int start = setCookieValue.IndexOf(keyWithEqual) + keyWithEqual.Length;
                string remainder = setCookieValue.Substring(start);
                int end = remainder.IndexOf(";");
                return end >= 0 ? remainder.Substring(0, end) : remainder;
            }
            return "";
        }

        private static string BuildSessionTokenCookieValue(string sessionTokenValue)
        {
            return SESSION_TOKEN_COOKIE_VALUE_KEY + "=" + sessionTokenValue;
        }

        private static string BuildTempTokenCookieValue(string sessionTokenValue)
        {
            return TEMP_SESSION_TOKEN_COOKIE_VALUE_KEY + "=" + sessionTokenValue;
        }

        private static bool HasCookieValue(string setCookieValue, string sessionKeystring)
        {
            Regex rgx1 = new Regex(sessionKeystring);
            Regex rgx2 = new Regex("=");
            return !string.IsNullOrEmpty(rgx2.Replace(rgx1.Replace(setCookieValue, ""), ""));
        }
    }
}
