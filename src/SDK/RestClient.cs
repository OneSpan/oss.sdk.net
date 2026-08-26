using System;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OneSpanSign.Sdk.Oauth;
#if NET5_0_OR_GREATER
using System.Net.Http;
#endif

namespace OneSpanSign.Sdk
{
    public class RestClient : IRestClient
    {
        private readonly string apiKey;
        private AuthHeaderGenerator headerGen;
        private readonly Support support = new Support();

        private readonly ProxyConfiguration proxyConfiguration;
        private readonly IDictionary<string, string> additionalHeaders;
        private readonly ApiTokenConfig apiTokenConfig;
        private readonly OAuthTokenConfig oauthTokenConfig;
        private ApiToken apiToken;
        private OAuthAccessToken oAuthAccessToken;
        private readonly Oauth2TokenManager oauth2TokenManager = new Oauth2TokenManager();

        public RestClient(string apiKey) : this(apiKey, false)
        {
        }

        public RestClient(string apiKey, bool allowAllSslCertificates)
            : this(apiKey, allowAllSslCertificates, null)
        {
        }

        public RestClient(string apiKey, ProxyConfiguration proxyConfiguration)
            : this(apiKey, false, proxyConfiguration)
        {
        }

        public RestClient(string apiKey, bool allowAllSslCertificates, ProxyConfiguration proxyConfiguration)
            : this(apiKey, allowAllSslCertificates, proxyConfiguration, new Dictionary<string, string>())
        {
        }

        public RestClient(string apiKey, bool allowAllSslCertificates, ProxyConfiguration proxyConfiguration,
            IDictionary<string, string> headers)
        {
            this.apiKey = apiKey;
            this.proxyConfiguration = proxyConfiguration;
            this.additionalHeaders = headers;

            if (allowAllSslCertificates)
            {
#if NET5_0_OR_GREATER
                HttpMethods.AllowAllSslCertificates = true;
#else
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
#endif
            }
        }

        public RestClient(ApiTokenConfig apiTokenConfig, bool allowAllSslCertificates,
            ProxyConfiguration proxyConfiguration, IDictionary<string, string> headers)
        {
            this.proxyConfiguration = proxyConfiguration;
            additionalHeaders = headers;

            if (allowAllSslCertificates)
            {
#if NET5_0_OR_GREATER
                HttpMethods.AllowAllSslCertificates = true;
#else
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
#endif
            }

            this.apiTokenConfig = apiTokenConfig;
        }

        public RestClient(OAuthTokenConfig oauthTokenConfig, bool allowAllSSLCertificates,
            ProxyConfiguration proxyConfiguration, IDictionary<string, string> headers)
        {
            this.oauthTokenConfig = oauthTokenConfig;

            if (allowAllSSLCertificates)
            {
#if NET5_0_OR_GREATER
                HttpMethods.AllowAllSslCertificates = true;
#else
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
#endif
            }

            this.proxyConfiguration = proxyConfiguration;
            additionalHeaders = headers;
        }

        public string Post(string path, string jsonPayload)
        {
            support.LogRequest("POST", path, jsonPayload);

            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte[0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            var responseBytes = HttpMethods.PostHttp(apiKey, path, payloadBytes, SetupAuthorization(null));

            var response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public void Post(string path, string jsonPayload, string sessionId)
        {
            support.LogRequest("POST", path, jsonPayload);

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);

            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte[0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.PostHttp(headerGen, path, payloadBytes, SetupAuthorization(sessionId));

            var response = Converter.ToString(responseBytes);
            support.LogResponse(response);
        }

        public string Put(string path, string jsonPayload)
        {
            support.LogRequest("PUT", path, jsonPayload);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.PutHttp(apiKey, path, Converter.ToBytes(jsonPayload), SetupAuthorization(null));
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);
            return response;
        }

        public string Patch(string path, string jsonPayload)
        {
            support.LogRequest("PATCH", path, jsonPayload);

            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte[0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            var responseBytes = HttpMethods.PatchHttp(apiKey, path, payloadBytes, SetupAuthorization(null));

            var response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string json)
        {
            support.LogRequest("POST", path, json);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            string response = HttpMethods.MultipartPostHttp(apiKey, path, fileBytes, boundary, null, SetupAuthorization(null));
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string sessionId, string json)
        {
            support.LogRequest("POST", path, json);

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            string response =
                HttpMethods.MultipartPostHttp(apiKey, path, fileBytes, boundary, headerGen, SetupAuthorization(sessionId));
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartPackage(string path, byte[] content, string boundary, string json)
        {
            support.LogRequest("POST", path, json);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            string response = HttpMethods.MultipartPostHttp(apiKey, path, content, boundary, null, SetupAuthorization(null));
            support.LogResponse(response);

            return response;
        }

        public string Get(string path)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            DownloadedFile responseBytes = HttpMethods.GetHttpJson(apiKey, path,
                HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON, SetupAuthorization(null));
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string Get(string path, string acceptType)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            DownloadedFile responseBytes = HttpMethods.GetHttpJson(apiKey, path, acceptType, SetupAuthorization(null));
            return Converter.ToString(responseBytes);
        }

        public DownloadedFile GetBytes(string path)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            return HttpMethods.GetHttp(apiKey, path, SetupAuthorization(null));
        }

        public DownloadedFile GetBytes(string path, string acceptType)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            return HttpMethods.GetHttpJson(apiKey, path, acceptType, SetupAuthorization(null));
        }

        public DownloadedFile GetHttpAsOctetStream(string path)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            return HttpMethods.GetHttpAsOctetStream(apiKey, path, SetupAuthorization(null));
        }

        public string Delete(string path)
        {
            support.LogRequest("DELETE", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.DeleteHttp(apiKey, path, SetupAuthorization(null));
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public void Delete(string path, string jsonPayload, string sessionId)
        {
            support.LogRequest("DELETE", path);

            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte[0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);
            HttpMethods.DeleteHttp(headerGen, path, payloadBytes, SetupAuthorization(sessionId));
        }

        public string Delete(string path, string jsonPayload)
        {
            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte[0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.DeleteHttp(apiKey, path, payloadBytes, SetupAuthorization(null));
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        private IDictionary<string, string> SetupAuthorization(string sessionId)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>(additionalHeaders);
            if (sessionId != null)
                return dictionary;

            if (oauthTokenConfig != null)
            {
                if (oAuthAccessToken == null || oauth2TokenManager.IsOAuth2TokenExpired(oAuthAccessToken.AccessToken))
                {
#if NET5_0_OR_GREATER
                    oAuthAccessToken = FetchOAuthAccessToken();
#else
                    oAuthAccessToken = FetchOAuthAccessTokenLegacy();
#endif
                }
                dictionary.Add("Authorization", "Bearer " + oAuthAccessToken.AccessToken);
            }
            else if (apiTokenConfig != null)
            {
                var milliseconds = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
                if (apiToken == null || milliseconds > apiToken.ExpiresAt - 60 * 1000)
                {
#if NET5_0_OR_GREATER
                    apiToken = FetchApiToken();
#else
                    apiToken = FetchApiTokenLegacy();
#endif
                }
                dictionary.Add("Authorization", "Bearer " + apiToken.AccessToken);
            }
            else
            {
                dictionary.Add("Authorization", "Basic " + apiKey);
            }

            return dictionary;
        }

#if NET5_0_OR_GREATER
        // ── .NET 5+ helpers using HttpClient.Send() ────────────────────────────

        private OAuthAccessToken FetchOAuthAccessToken()
        {
            string grantTypeEntity = HttpMethods.OAUTH_GRANT_TYPE;
            string senderIdEntity = oauthTokenConfig.SenderId != null
                ? "&" + HttpMethods.OAUTH_SENDER_ID + "=" + oauthTokenConfig.SenderId
                : "";
            string delegatorIdEntity = oauthTokenConfig.DelegatorId != null
                ? "&" + HttpMethods.OAUTH_DELEGATOR_ID + "=" + oauthTokenConfig.DelegatorId
                : "";
            byte[] bodyBytes = Encoding.UTF8.GetBytes(grantTypeEntity + senderIdEntity + delegatorIdEntity);

            var request = new HttpRequestMessage(HttpMethod.Post, oauthTokenConfig.AuthenticationServer);
            request.Headers.TryAddWithoutValidation("User-Agent", HttpMethods.ESL_API_USER_AGENT);
            var authHeader = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{oauthTokenConfig.ClientId}:{oauthTokenConfig.ClientSecret}"));
            request.Headers.TryAddWithoutValidation("Authorization", "Basic " + authHeader);
            request.Headers.TryAddWithoutValidation("Accept", HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(bodyBytes);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_FORM_URLENCODED);

            try
            {
                using var client = CreateAuthHttpClient();
                using var response = client.Send(request);
                using var stream = response.Content.ReadAsStream();
                using var reader = new StreamReader(stream);
                string responseString = reader.ReadToEnd();

                if (!response.IsSuccessStatusCode)
                    throw new OssException(
                        $"Unable to fetch access token for {oauthTokenConfig}, status was {(int)response.StatusCode}, response was {responseString}",
                        null);

                return JsonConvert.DeserializeObject<OAuthAccessToken>(responseString);
            }
            catch (OssException) { throw; }
            catch (Exception ex)
            {
                throw new OssException($"Unable to fetch access token for {oauthTokenConfig}. {ex.Message}", ex);
            }
        }

        private ApiToken FetchApiToken()
        {
            var jsonPayload = $"{{\"clientId\":\"{apiTokenConfig.ClientAppId}\",\"secret\":\"{apiTokenConfig.ClientAppSecret}\",\"type\":\"{apiTokenConfig.TokenType}\"";
            if (apiTokenConfig.TokenType == ApiTokenType.SENDER)
                jsonPayload += $",\"email\":\"{apiTokenConfig.SenderEmail}\"";
            jsonPayload += "}";
            byte[] bodyBytes = Encoding.Unicode.GetBytes(jsonPayload);

            var request = new HttpRequestMessage(HttpMethod.Post, apiTokenConfig.BaseUrl + "/apitoken/clientApp/accessToken");
            request.Headers.TryAddWithoutValidation("User-Agent", HttpMethods.ESL_API_USER_AGENT);
            request.Headers.TryAddWithoutValidation("Accept", HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(bodyBytes);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", HttpMethods.ESL_CONTENT_TYPE_APPLICATION_JSON);

            using var client = CreateAuthHttpClient();
            using var response = client.Send(request);
            using var stream = response.Content.ReadAsStream();
            using var reader = new StreamReader(stream);
            string result = reader.ReadToEnd();

            if (!response.IsSuccessStatusCode)
                throw new OssException(
                    $"Unable to fetch access token for {apiTokenConfig}, status was {(int)response.StatusCode}, response was {result}",
                    null);

            return JsonConvert.DeserializeObject<ApiToken>(result);
        }

        private HttpClient CreateAuthHttpClient()
        {
            var handler = new HttpClientHandler();
            if (HttpMethods.AllowAllSslCertificates)
                handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            if (proxyConfiguration != null)
            {
                handler.UseProxy = true;
                var proxy = new WebProxy(new Uri(
                    proxyConfiguration.GetScheme() + "://" +
                    proxyConfiguration.GetHost() + ":" +
                    proxyConfiguration.GetPort()));
                if (proxyConfiguration.HasCredentials())
                    proxy.Credentials = new NetworkCredential(
                        proxyConfiguration.GetUserName(), proxyConfiguration.GetPassword());
                handler.Proxy = proxy;
            }
            return new HttpClient(handler) { Timeout = TimeSpan.FromMilliseconds(HttpMethods.REQUEST_TIMEOUT) };
        }

#else
        // ── netstandard2.0 helpers using HttpWebRequest ────────────────────────

        private OAuthAccessToken FetchOAuthAccessTokenLegacy()
        {
            HttpWebRequest oauthTokenRequest =
                HttpMethods.WithUserAgent(WebRequest.Create(oauthTokenConfig.AuthenticationServer));
            var authHeader = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{oauthTokenConfig.ClientId}:{oauthTokenConfig.ClientSecret}"));
            oauthTokenRequest.Headers.Add("Authorization", "Basic " + authHeader);
            oauthTokenRequest.ContentType = HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_FORM_URLENCODED;
            oauthTokenRequest.Method = "POST";
            oauthTokenRequest.Accept = HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON;
            oauthTokenRequest.Timeout = HttpMethods.REQUEST_TIMEOUT;
            HttpMethods.SetProxy(oauthTokenRequest);

            string grantTypeEntity = HttpMethods.OAUTH_GRANT_TYPE;
            string senderIdEntity = oauthTokenConfig.SenderId != null
                ? "&" + HttpMethods.OAUTH_SENDER_ID + "=" + oauthTokenConfig.SenderId
                : "";
            string delegatorIdEntity = oauthTokenConfig.DelegatorId != null
                ? "&" + HttpMethods.OAUTH_DELEGATOR_ID + "=" + oauthTokenConfig.DelegatorId
                : "";
            byte[] entitiesBytes = Encoding.UTF8.GetBytes(grantTypeEntity + senderIdEntity + delegatorIdEntity);
            oauthTokenRequest.ContentLength = entitiesBytes.Length;

            using (Stream dataStream = oauthTokenRequest.GetRequestStream())
                dataStream.Write(entitiesBytes, 0, entitiesBytes.Length);

            try
            {
                using (WebResponse response = oauthTokenRequest.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string responseString = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<OAuthAccessToken>(responseString);
                }
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    if (response == null) throw;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (var stream = httpResponse.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string responseBody = reader.ReadToEnd();
                        throw new OssException(
                            "Unable to fetch access token for " + oauthTokenConfig
                            + ", status was " + httpResponse.StatusCode
                            + ", response was " + responseBody,
                            null);
                    }
                }
            }
        }

        private ApiToken FetchApiTokenLegacy()
        {
            var jsonPayload = string.Format(
                "{{\"clientId\":\"{0}\",\"secret\":\"{1}\",\"type\":\"{2}\"",
                apiTokenConfig.ClientAppId, apiTokenConfig.ClientAppSecret, apiTokenConfig.TokenType);
            if (apiTokenConfig.TokenType == ApiTokenType.SENDER)
                jsonPayload += string.Format(",\"email\":\"{0}\"", apiTokenConfig.SenderEmail);
            jsonPayload += "}";

            var jsonPayloadBytes = Encoding.Unicode.GetBytes(jsonPayload);
            HttpWebRequest apiTokenRequest =
                HttpMethods.WithUserAgent(WebRequest.Create(apiTokenConfig.BaseUrl + "/apitoken/clientApp/accessToken"));
            apiTokenRequest.Method = "POST";
            apiTokenRequest.ContentType = HttpMethods.ESL_CONTENT_TYPE_APPLICATION_JSON;
            apiTokenRequest.ContentLength = jsonPayloadBytes.Length;
            apiTokenRequest.Accept = HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON;
            apiTokenRequest.Timeout = HttpMethods.REQUEST_TIMEOUT;
            HttpMethods.SetProxy(apiTokenRequest);

            using (var dataStream = apiTokenRequest.GetRequestStream())
                dataStream.Write(jsonPayloadBytes, 0, jsonPayloadBytes.Length);

            var httpResponse = (HttpWebResponse)apiTokenRequest.GetResponse();

            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new OssException(
                    "Unable to fetch access token for " + apiTokenConfig + ", response was " + httpResponse,
                    null);

            string result;
            using (var streamReader = new StreamReader(
                httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                result = streamReader.ReadToEnd();
            }
            httpResponse.Close();

            return JsonConvert.DeserializeObject<ApiToken>(result);
        }

#endif
    }
}
