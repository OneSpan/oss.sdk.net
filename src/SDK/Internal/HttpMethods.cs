using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
#if NET5_0_OR_GREATER
using System.Net.Http;
#endif

namespace OneSpanSign.Sdk.Internal
{
    /// <summary>
    /// For internal use.
    /// </summary>
    public class HttpMethods
    {
        // ── Shared constants ───────────────────────────────────────────────────

        public const string ESL_API_VERSION = "11.70.0";
        public const string ESL_API_USER_AGENT = ".Net SDK v" + ESL_API_VERSION;
        private const string ESL_API_VERSION_HEADER = "esl-api-version=" + ESL_API_VERSION;

        private const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
        public const string ESL_CONTENT_TYPE_APPLICATION_JSON = CONTENT_TYPE_APPLICATION_JSON + "; " + ESL_API_VERSION_HEADER;

        private const string CONTENT_TYPE_APPLICATION_MULTIPART = "multipart/form-data";
        private const string ESL_CONTENT_TYPE_APPLICATION_MULTIPART = CONTENT_TYPE_APPLICATION_MULTIPART + "; " + ESL_API_VERSION_HEADER + "; boundary={0}";

        private const string ACCEPT_TYPE_APPLICATION_JSON = "application/json";
        private const string ACCEPT_TYPE_APPLICATION_OCTET_STREAM = "application/octet-stream";
        private const string ACCEPT_TYPE_APPLICATION = "*/*";
        private const string ACCEPT_TYPE_APPLICATION_FORM_URLENCODED = "application/x-www-form-urlencoded";
        public const string ESL_ACCEPT_TYPE_APPLICATION_JSON = ACCEPT_TYPE_APPLICATION_JSON + "; " + ESL_API_VERSION_HEADER;
        private const string ESL_ACCEPT_TYPE_APPLICATION_OCTET_STREAM = ACCEPT_TYPE_APPLICATION_OCTET_STREAM + "; " + ESL_API_VERSION_HEADER;
        private const string ESL_ACCEPT_TYPE_APPLICATION = ACCEPT_TYPE_APPLICATION + "; " + ESL_API_VERSION_HEADER;
        public const string ESL_ACCEPT_TYPE_APPLICATION_FORM_URLENCODED = ACCEPT_TYPE_APPLICATION_FORM_URLENCODED + "; " + ESL_API_VERSION_HEADER;

        public const string OAUTH_GRANT_TYPE = "grant_type=client_credentials";
        public const string OAUTH_SENDER_ID = "sender_id";
        public const string OAUTH_DELEGATOR_ID = "delegator_id";
        public const int REQUEST_TIMEOUT = 30000; // 30 seconds

        private static int _requestTimeout = REQUEST_TIMEOUT;

        /// <summary>
        /// Per-request timeout in milliseconds. Defaults to <see cref="REQUEST_TIMEOUT"/> (30 seconds).
        /// Raise it for callers that hit slow endpoints — large report queries or wide list pages can
        /// exceed the default on busy accounts.
        /// Assigning a new value discards the cached HttpClient so the change takes effect on the next
        /// request; set it during start-up rather than while requests are in flight.
        /// </summary>
        public static int RequestTimeout
        {
            get { return _requestTimeout; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        "RequestTimeout must be a positive number of milliseconds.");
                }

                if (value == _requestTimeout)
                {
                    return;
                }

                _requestTimeout = value;
                ResetHttpClients();
            }
        }

        public static ProxyConfiguration ProxyConfiguration;

        /// <summary>
        /// When true, SSL certificate validation is bypassed.
        /// On net5+, configures the shared HttpClient handler.
        /// On netstandard2.0, applied per-request via WithUserAgent() and via
        /// ServicePointManager in RestClient constructors.
        /// </summary>
        public static bool AllowAllSslCertificates;

        // ── Shared private helper ──────────────────────────────────────────────

        private static string GetFilename(string disposition)
        {
            string fileNameTitle = "filename*=UTF-8''";
            string[] parts = disposition.Split(';');
            foreach (string part in parts)
            {
                int index = part.IndexOf(fileNameTitle);
                if (index > 0)
                {
                    return Uri.UnescapeDataString(
                        part.Substring(fileNameTitle.Length + 1, part.Length - fileNameTitle.Length - 1));
                }
            }
            return "";
        }

        // ── no-op kept for binary compatibility ───────────────────────────────
        public static bool UseUnsafeHeaderParsing(ref string strError) => true;

#if NET5_0_OR_GREATER
        // ══════════════════════════════════════════════════════════════════════
        // .NET 5 / .NET 8 / .NET 10  —  HttpClient implementation
        // ══════════════════════════════════════════════════════════════════════

        private static HttpClient _httpClient;
        private static HttpClient _sslBypassHttpClient;
        private static readonly object _clientLock = new object();
        private static ProxyConfiguration _cachedProxy;

        /// <summary>
        /// Drops the cached clients so the next request picks up new settings.
        /// HttpClient.Timeout cannot be changed once a request has been issued, so the instance
        /// has to be rebuilt rather than mutated.
        /// </summary>
        private static void ResetHttpClients()
        {
            lock (_clientLock)
            {
                _httpClient?.Dispose();
                _httpClient = null;
                _sslBypassHttpClient?.Dispose();
                _sslBypassHttpClient = null;
            }
        }

        private static HttpClient GetHttpClient()
        {
            bool allowInvalidSsl = AllowAllSslCertificates ||
                "true".Equals(Environment.GetEnvironmentVariable("ALLOW_INVALID_SSL_CERTS"), StringComparison.OrdinalIgnoreCase);

            lock (_clientLock)
            {
                if (allowInvalidSsl)
                {
                    if (_sslBypassHttpClient == null || ProxyConfiguration != _cachedProxy)
                    {
                        _sslBypassHttpClient?.Dispose();
                        _sslBypassHttpClient = CreateHttpClient(true);
                        _cachedProxy = ProxyConfiguration;
                    }
                    return _sslBypassHttpClient;
                }

                if (_httpClient == null || ProxyConfiguration != _cachedProxy)
                {
                    _httpClient?.Dispose();
                    _httpClient = CreateHttpClient(false);
                    _cachedProxy = ProxyConfiguration;
                }
                return _httpClient;
            }
        }

        private static HttpClient CreateHttpClient(bool allowAllSsl)
        {
            var handler = new HttpClientHandler
            {
                // Do not persist cookies across requests — matches the old WebRequest behaviour
                // where each request was stateless. Without this, the server's Set-Cookie
                // (e.g. a signer session token) would leak into subsequent requests and override
                // the Authorization header, causing 403 errors for unrelated callers.
                UseCookies = false
            };

            if (allowAllSsl)
            {
                handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            }

            if (ProxyConfiguration != null)
            {
                handler.UseProxy = true;
                var proxy = new WebProxy(new Uri(
                    ProxyConfiguration.GetScheme() + "://" +
                    ProxyConfiguration.GetHost() + ":" +
                    ProxyConfiguration.GetPort()));
                if (ProxyConfiguration.HasCredentials())
                {
                    proxy.Credentials = new NetworkCredential(
                        ProxyConfiguration.GetUserName(),
                        ProxyConfiguration.GetPassword());
                }
                handler.Proxy = proxy;
            }

            return new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMilliseconds(RequestTimeout)
            };
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string path, string acceptType = null)
        {
            var request = new HttpRequestMessage(method, path);
            request.Headers.TryAddWithoutValidation("User-Agent", ESL_API_USER_AGENT);
            if (acceptType != null)
                request.Headers.TryAddWithoutValidation("Accept", acceptType);
            return request;
        }

        private static void SetupAuthorization(HttpRequestMessage request, AuthHeaderGenerator authHeaderGen)
        {
            if (authHeaderGen is ApiTokenAuthHeaderGenerator)
                SetupAuthorization(request, authHeaderGen.Value);
            else if (authHeaderGen is SessionIdAuthHeaderGenerator)
                request.Headers.TryAddWithoutValidation(authHeaderGen.Name, authHeaderGen.Value);
        }

        private static void SetupAuthorization(HttpRequestMessage request, string apiKey)
        {
            if (apiKey != null && !request.Headers.Contains("Authorization"))
                request.Headers.TryAddWithoutValidation("Authorization", "Basic " + apiKey);
        }

        private static void AddAdditionalHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
        {
            foreach (var entry in headers)
                request.Headers.TryAddWithoutValidation(entry.Key, entry.Value);
        }

        private static byte[] SendAndGetBytes(HttpRequestMessage request)
        {
            try
            {
                using var response = GetHttpClient().Send(request);
                if (!response.IsSuccessStatusCode)
                {
                    using var errStream = response.Content.ReadAsStream();
                    using var errReader = new StreamReader(errStream);
                    string errorDetails = errReader.ReadToEnd();
                    throw new OssServerException(
                        $"{(int)response.StatusCode} {response.ReasonPhrase} HTTP {request.Method.Method} on URI {request.RequestUri}. Optional details: {errorDetails}",
                        errorDetails, (Exception)null);
                }
                using var stream = response.Content.ReadAsStream();
                using var mem = new MemoryStream();
                stream.CopyTo(mem);
                return mem.ToArray();
            }
            catch (OssServerException) { throw; }
            catch (Exception e)
            {
                throw new OssException("Error communicating with oss server. " + e.Message, e);
            }
        }

        private static (byte[] bytes, string fileName) SendAndGetDownload(HttpRequestMessage request)
        {
            try
            {
                using var response = GetHttpClient().Send(request);
                if (!response.IsSuccessStatusCode)
                {
                    using var errStream = response.Content.ReadAsStream();
                    using var errReader = new StreamReader(errStream);
                    string errorDetails = errReader.ReadToEnd();
                    throw new OssServerException(
                        $"{(int)response.StatusCode} {response.ReasonPhrase} HTTP {request.Method.Method} on URI {request.RequestUri}. Optional details: {errorDetails}",
                        errorDetails, (Exception)null);
                }
                string contentDisp = response.Content.Headers.ContentDisposition?.ToString() ?? "";
                using var stream = response.Content.ReadAsStream();
                using var mem = new MemoryStream();
                stream.CopyTo(mem);
                return (mem.ToArray(), GetFilename(contentDisp));
            }
            catch (OssServerException) { throw; }
            catch (Exception e)
            {
                throw new OssException("Error communicating with oss server. " + e.Message, e);
            }
        }

        public static byte[] PostHttp(string apiToken, string path, byte[] content)
            => PostHttp(apiToken, path, content, new Dictionary<string, string>());

        public static byte[] PostHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Post, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(content);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", ESL_CONTENT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            return SendAndGetBytes(request);
        }

        public static byte[] PostHttp(AuthHeaderGenerator authHeaderGen, string path, byte[] content)
            => PostHttp(authHeaderGen, path, content, new Dictionary<string, string>());

        public static byte[] PostHttp(AuthHeaderGenerator authHeaderGen, string path, byte[] content, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Post, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(content);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", ESL_CONTENT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, authHeaderGen);
            return SendAndGetBytes(request);
        }

        public static byte[] PatchHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Patch, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(content);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", ESL_CONTENT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            return SendAndGetBytes(request);
        }

        public static byte[] PutHttp(string apiKey, string path, byte[] content)
            => PutHttp(apiKey, path, content, new Dictionary<string, string>());

        public static byte[] PutHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Put, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(content);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", ESL_CONTENT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            return SendAndGetBytes(request);
        }

        public static byte[] GetHttp(string path)
            => GetHttp(path, new Dictionary<string, string>());

        public static byte[] GetHttp(string path, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Get, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            return SendAndGetBytes(request);
        }

        public static DownloadedFile GetHttpJson(string apiKey, string path, string acceptType)
            => GetHttpJson(apiKey, path, acceptType, new Dictionary<string, string>());

        public static DownloadedFile GetHttpJson(string apiKey, string path, string acceptType, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Get, path, acceptType);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            var (bytes, _) = SendAndGetDownload(request);
            return new DownloadedFile("", bytes);
        }

        public static DownloadedFile GetHttp(string apiKey, string path)
            => GetHttp(apiKey, path, new Dictionary<string, string>());

        public static DownloadedFile GetHttp(string apiKey, string path, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Get, path, ESL_ACCEPT_TYPE_APPLICATION);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            var (bytes, fileName) = SendAndGetDownload(request);
            return new DownloadedFile(fileName, bytes);
        }

        public static DownloadedFile GetHttpAsOctetStream(string apiKey, string path)
            => GetHttpAsOctetStream(apiKey, path, new Dictionary<string, string>());

        public static DownloadedFile GetHttpAsOctetStream(string apiKey, string path, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Get, path, ESL_ACCEPT_TYPE_APPLICATION_OCTET_STREAM);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            var (bytes, _) = SendAndGetDownload(request);
            return new DownloadedFile("", bytes);
        }

        public static byte[] DeleteHttp(string apiKey, string path)
            => DeleteHttp(apiKey, path, new Dictionary<string, string>());

        public static byte[] DeleteHttp(string apiKey, string path, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Delete, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            return SendAndGetBytes(request);
        }

        public static byte[] DeleteHttp(AuthHeaderGenerator authHeader, string path, byte[] content, IDictionary<string, string> headers)
        {
            headers[authHeader.Name] = authHeader.Value;
            return DeleteHttp(path, content, headers, null);
        }

        public static byte[] DeleteHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
            => DeleteHttp(path, content, headers, apiKey);

        private static byte[] DeleteHttp(string path, byte[] content, IDictionary<string, string> headers, string apiKey)
        {
            var request = CreateRequest(HttpMethod.Delete, path, ESL_ACCEPT_TYPE_APPLICATION_JSON);
            request.Content = new ByteArrayContent(content);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", ESL_CONTENT_TYPE_APPLICATION_JSON);
            AddAdditionalHeaders(request, headers);
            SetupAuthorization(request, apiKey);
            return SendAndGetBytes(request);
        }

        public static void AddAuthorizationHeader(HttpRequestMessage request, AuthHeaderGenerator authHeaderGen)
        {
            if (authHeaderGen != null)
                request.Headers.TryAddWithoutValidation(authHeaderGen.Name, authHeaderGen.Value);
        }

        public static string MultipartPostHttp(string apiKey, string path, byte[] content, string boundary, AuthHeaderGenerator authHeaderGen)
            => MultipartPostHttp(apiKey, path, content, boundary, authHeaderGen, new Dictionary<string, string>());

        public static string MultipartPostHttp(string apiKey, string path, byte[] content, string boundary, AuthHeaderGenerator authHeaderGen, IDictionary<string, string> headers)
        {
            var request = CreateRequest(HttpMethod.Post, path);
            request.Content = new ByteArrayContent(content);
            request.Content.Headers.TryAddWithoutValidation("Content-Type", string.Format(ESL_CONTENT_TYPE_APPLICATION_MULTIPART, boundary));
            AddAdditionalHeaders(request, headers);
            AddAuthorizationHeader(request, authHeaderGen);
            SetupAuthorization(request, apiKey);

            try
            {
                using var response = GetHttpClient().Send(request);
                if (!response.IsSuccessStatusCode)
                {
                    using var errStream = response.Content.ReadAsStream();
                    using var errReader = new StreamReader(errStream);
                    string errorDetails = errReader.ReadToEnd();
                    throw new OssServerException(
                        $"{(int)response.StatusCode} {response.ReasonPhrase} HTTP {request.Method.Method} on URI {request.RequestUri}. Optional details: {errorDetails}",
                        errorDetails, (Exception)null);
                }
                using var stream = response.Content.ReadAsStream();
                using var reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (OssServerException) { throw; }
            catch (Exception e)
            {
                throw new OssException("Error communicating with oss server. " + e.Message, e);
            }
        }

#else
        // ══════════════════════════════════════════════════════════════════════
        // netstandard2.0  —  HttpWebRequest / WebRequest implementation
        // ══════════════════════════════════════════════════════════════════════

        /// <summary>
        /// No cached client to reset on this target — each HttpWebRequest reads
        /// <see cref="RequestTimeout"/> when it is created.
        /// </summary>
        private static void ResetHttpClients() { }

        /// <summary>
        /// Applies User-Agent and optional SSL bypass to a WebRequest.
        /// </summary>
        public static HttpWebRequest WithUserAgent(WebRequest request)
        {
            ((HttpWebRequest)request).UserAgent = ESL_API_USER_AGENT;
            if (AllowAllSslCertificates || "true".Equals(Environment.GetEnvironmentVariable("ALLOW_INVALID_SSL_CERTS")))
            {
                ((HttpWebRequest)request).ServerCertificateValidationCallback =
                    (sender, certificate, chain, errors) => true;
            }
            return (HttpWebRequest)request;
        }

        public static void SetProxy(WebRequest request)
        {
            if (ProxyConfiguration != null)
            {
                WebProxy webProxy = new WebProxy(new Uri(
                    ProxyConfiguration.GetScheme() + "://" +
                    ProxyConfiguration.GetHost() + ":" +
                    ProxyConfiguration.GetPort()));
                if (ProxyConfiguration.HasCredentials())
                {
                    webProxy.Credentials = new NetworkCredential(
                        ProxyConfiguration.GetUserName(),
                        ProxyConfiguration.GetPassword());
                }
                request.Proxy = webProxy;
            }
        }

        private static void SetupAuthorization(WebRequest request, AuthHeaderGenerator authHeaderGen)
        {
            if (typeof(ApiTokenAuthHeaderGenerator).IsInstanceOfType(authHeaderGen))
                SetupAuthorization(request, authHeaderGen.Value);
            else if (typeof(SessionIdAuthHeaderGenerator).IsInstanceOfType(authHeaderGen))
                request.Headers.Add(authHeaderGen.Name, authHeaderGen.Value);
        }

        private static void SetupAuthorization(WebRequest request, string apiKey)
        {
            if (apiKey != null && request.Headers.Get("Authorization") == null)
                request.Headers.Add("Authorization", "Basic " + apiKey);
        }

        private static void AddAdditionalHeaders(WebRequest request, IDictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> entry in headers)
                request.Headers.Add(entry.Key, entry.Value);
        }

        private static void CopyTo(Stream input, Stream output)
        {
            byte[] buffer = new byte[64 * 1024];
            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, bytesRead);
        }

        public static byte[] PostHttp(string apiToken, string path, byte[] content)
            => PostHttp(apiToken, path, content, new Dictionary<string, string>());

        public static byte[] PostHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "POST";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (Stream dataStream = request.GetRequestStream())
                    dataStream.Write(content, 0, content.Length);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static byte[] PostHttp(AuthHeaderGenerator authHeaderGen, string path, byte[] content)
            => PostHttp(authHeaderGen, path, content, new Dictionary<string, string>());

        public static byte[] PostHttp(AuthHeaderGenerator authHeaderGen, string path, byte[] content, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "POST";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, authHeaderGen);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (Stream dataStream = request.GetRequestStream())
                    dataStream.Write(content, 0, content.Length);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
                }
            }
            catch (WebException e)
            {
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new OssServerException(
                        $"{e.Message} HTTP {((HttpWebResponse)e.Response).Method} on URI {e.Response.ResponseUri}. Optional details: {errorDetails}",
                        errorDetails, e);
                }
            }
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static byte[] PatchHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "PATCH";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (Stream dataStream = request.GetRequestStream())
                    dataStream.Write(content, 0, content.Length);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static byte[] PutHttp(string apiKey, string path, byte[] content)
            => PutHttp(apiKey, path, content, new Dictionary<string, string>());

        public static byte[] PutHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "PUT";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (Stream dataStream = request.GetRequestStream())
                    dataStream.Write(content, 0, content.Length);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static byte[] GetHttp(string path)
            => GetHttp(path, new Dictionary<string, string>());

        public static byte[] GetHttp(string path, IDictionary<string, string> headers)
        {
            string message = "";
            UseUnsafeHeaderParsing(ref message);
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "GET";
                AddAdditionalHeaders(request, headers);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static DownloadedFile GetHttpJson(string apiKey, string path, string acceptType)
            => GetHttpJson(apiKey, path, acceptType, new Dictionary<string, string>());

        public static DownloadedFile GetHttpJson(string apiKey, string path, string acceptType, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "GET";
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = acceptType;
                SetProxy(request);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return new DownloadedFile("", mem.ToArray());
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static DownloadedFile GetHttp(string apiKey, string path)
            => GetHttp(apiKey, path, new Dictionary<string, string>());

        public static DownloadedFile GetHttp(string apiKey, string path, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "GET";
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION;
                SetProxy(request);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    string fileName = "";
                    if (!string.IsNullOrEmpty(response.Headers["Content-Disposition"]))
                        fileName = GetFilename(response.Headers["Content-Disposition"]);
                    return new DownloadedFile(fileName, mem.ToArray());
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static DownloadedFile GetHttpAsOctetStream(string apiKey, string path)
            => GetHttpAsOctetStream(apiKey, path, new Dictionary<string, string>());

        public static DownloadedFile GetHttpAsOctetStream(string apiKey, string path, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "GET";
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_OCTET_STREAM;
                SetProxy(request);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return new DownloadedFile("", mem.ToArray());
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static byte[] DeleteHttp(string apiKey, string path)
            => DeleteHttp(apiKey, path, new Dictionary<string, string>());

        public static byte[] DeleteHttp(string apiKey, string path, IDictionary<string, string> headers)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "DELETE";
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static byte[] DeleteHttp(AuthHeaderGenerator authHeader, string path, byte[] content, IDictionary<string, string> headers)
        {
            headers[authHeader.Name] = authHeader.Value;
            return DeleteHttp(path, content, headers, (string)null);
        }

        public static byte[] DeleteHttp(string apiKey, string path, byte[] content, IDictionary<string, string> headers)
            => DeleteHttp(path, content, headers, apiKey);

        private static byte[] DeleteHttp(string path, byte[] content, IDictionary<string, string> headers, string apiKey)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpWebRequest request = WithUserAgent(WebRequest.Create(path));
                request.Method = "DELETE";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders(request, headers);
                SetupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy(request);
                using (Stream dataStream = request.GetRequestStream())
                    dataStream.Write(content, 0, content.Length);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    CopyTo(responseStream, mem);
                    return mem.ToArray();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

        public static void AddAuthorizationHeader(WebRequest request, AuthHeaderGenerator authHeaderGen)
        {
            if (authHeaderGen != null)
                request.Headers.Add(authHeaderGen.Name, authHeaderGen.Value);
        }

        public static string MultipartPostHttp(string apiKey, string path, byte[] content, string boundary, AuthHeaderGenerator authHeaderGen)
            => MultipartPostHttp(apiKey, path, content, boundary, authHeaderGen, new Dictionary<string, string>());

        public static string MultipartPostHttp(string apiKey, string path, byte[] content, string boundary, AuthHeaderGenerator authHeaderGen, IDictionary<string, string> headers)
        {
            WebRequest request = WithUserAgent(WebRequest.Create(path));
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                request.Method = "POST";
                request.ContentType = string.Format(ESL_CONTENT_TYPE_APPLICATION_MULTIPART, boundary);
                request.ContentLength = content.Length;
                AddAdditionalHeaders(request, headers);
                AddAuthorizationHeader(request, authHeaderGen);
                SetupAuthorization(request, apiKey);
                SetProxy(request);
                using (Stream dataStream = request.GetRequestStream())
                    dataStream.Write(content, 0, content.Length);
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    return new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
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
            catch (Exception e) { throw new OssException("Error communicating with oss server. " + e.Message, e); }
        }

#endif
    }
}
