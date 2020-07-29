using System;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class RestClient
    {
        private readonly string apiKey;
        private AuthHeaderGenerator headerGen;
        private readonly Support support = new Support();

        private readonly ProxyConfiguration proxyConfiguration;
        private readonly IDictionary<string, string> additionalHeaders;
        private readonly ApiTokenConfig apiTokenConfig;
        private ApiToken apiToken;

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
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
            }
        }

        public RestClient(ApiTokenConfig apiTokenConfig, bool allowAllSslCertificates,
            ProxyConfiguration proxyConfiguration, IDictionary<string, string> headers)
        {
            this.proxyConfiguration = proxyConfiguration;
            additionalHeaders = headers;

            if (allowAllSslCertificates)
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
            }

            this.apiTokenConfig = apiTokenConfig;
        }

        public string Post(string path, string jsonPayload)
        {
            support.LogRequest("POST", path, jsonPayload);

            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte[0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            var responseBytes = HttpMethods.PostHttp(apiKey, path, payloadBytes, additionalHeaders);

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

            additionalHeaders.Remove("Authorization");
            byte[] responseBytes = HttpMethods.PostHttp(headerGen, path, payloadBytes, additionalHeaders);

            var response = Converter.ToString(responseBytes);
            support.LogResponse(response);
        }

        public string Put(string path, string jsonPayload)
        {
            support.LogRequest("PUT", path, jsonPayload);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            byte[] responseBytes = HttpMethods.PutHttp(apiKey, path, Converter.ToBytes(jsonPayload), additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);
            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string json)
        {
            support.LogRequest("POST", path, json);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            string response = HttpMethods.MultipartPostHttp(apiKey, path, fileBytes, boundary, null, additionalHeaders);
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string sessionId, string json)
        {
            support.LogRequest("POST", path, json);

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            additionalHeaders.Remove("Authorization");
            string response =
                HttpMethods.MultipartPostHttp(apiKey, path, fileBytes, boundary, headerGen, additionalHeaders);
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartPackage(string path, byte[] content, string boundary, string json)
        {
            support.LogRequest("POST", path, json);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            string response = HttpMethods.MultipartPostHttp(apiKey, path, content, boundary, null, additionalHeaders);
            support.LogResponse(response);

            return response;
        }

        public string Get(string path)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            DownloadedFile responseBytes = HttpMethods.GetHttpJson(apiKey, path,
                HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON, additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string Get(string path, string acceptType)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            DownloadedFile responseBytes = HttpMethods.GetHttpJson(apiKey, path, acceptType, additionalHeaders);
            return Converter.ToString(responseBytes);
        }

        public DownloadedFile GetBytes(string path)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            return HttpMethods.GetHttp(apiKey, path, additionalHeaders);
        }

        public DownloadedFile GetBytes(string path, string acceptType)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            return HttpMethods.GetHttpJson(apiKey, path, acceptType, additionalHeaders);
        }

        public DownloadedFile GetHttpAsOctetStream(string path)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            return HttpMethods.GetHttpAsOctetStream(apiKey, path, additionalHeaders);
        }

        public string Delete(string path)
        {
            support.LogRequest("DELETE", path);

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            byte[] responseBytes = HttpMethods.DeleteHttp(apiKey, path, additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public void Delete(string path, string jsonPayload, string sessionId)
        {
            support.LogRequest("DELETE", path);

            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte [0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            additionalHeaders.Remove("Authorization");
            headerGen = new SessionIdAuthHeaderGenerator(sessionId);
            HttpMethods.DeleteHttp(headerGen, path, payloadBytes, additionalHeaders);
        }

        public string Delete(string path, string jsonPayload)
        {
            var payloadBytes = jsonPayload != null ? Converter.ToBytes(jsonPayload) : new byte [0];

            if (proxyConfiguration != null)
                HttpMethods.ProxyConfiguration = proxyConfiguration;

            SetupAuthorization();
            byte[] responseBytes = HttpMethods.DeleteHttp(apiKey, path, payloadBytes, additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        private void SetupAuthorization()
        {
            additionalHeaders.Remove("Authorization");
            if (apiTokenConfig != null)
            {
                //Do we have an api token and is it still valid for at least a minute ?
                var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
                var milliseconds = (long) timeSpan.TotalMilliseconds;
                if (apiToken == null || milliseconds > apiToken.ExpiresAt - 60 * 1000)
                {
                    //We need to fetch a new access token using the clientAppId/Secret
                    var jsonPayload = String.Format("{{\"clientId\":\"{0}\",\"secret\":\"{1}\",\"type\":\"{2}\"",
                        apiTokenConfig.ClientAppId, apiTokenConfig.ClientAppSecret,
                        apiTokenConfig.TokenType.ToString());
                    if (apiTokenConfig.TokenType == ApiTokenType.SENDER)
                    {
                        jsonPayload += String.Format(",\"email\":\"{0}\"", apiTokenConfig.SenderEmail);
                    }

                    jsonPayload += "}";
                    var jsonPayloadBytes = Encoding.Unicode.GetBytes(jsonPayload);
                    var apiTokenRequest =
                        (HttpWebRequest) WebRequest.Create(apiTokenConfig.BaseUrl + "/apitoken/clientApp/accessToken");
                    apiTokenRequest.Method = "POST";
                    apiTokenRequest.ContentType = HttpMethods.ESL_CONTENT_TYPE_APPLICATION_JSON;
                    apiTokenRequest.ContentLength = jsonPayloadBytes.Length;
                    apiTokenRequest.Accept = HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON;
                    apiTokenRequest.Timeout = 30000; //30 seconds
                    HttpMethods.SetProxy(apiTokenRequest);

                    using (var dataStream = apiTokenRequest.GetRequestStream())
                    {
                        dataStream.Write(jsonPayloadBytes, 0, jsonPayloadBytes.Length);
                    }

                    var httpResponse = (HttpWebResponse) apiTokenRequest.GetResponse();

                    if (httpResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new OssException(
                            "Unable to fetch access token for " + apiTokenConfig + ", response was " + httpResponse,
                            null);
                    }

                    string result;
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                    httpResponse.Close();

                    apiToken = JsonConvert.DeserializeObject<ApiToken>(result);
                }

                additionalHeaders.Add("Authorization", "Bearer " + apiToken.AccessToken);
            }
            else
            {
                additionalHeaders.Add("Authorization", "Basic " + apiKey);
            }
        }
    }
}