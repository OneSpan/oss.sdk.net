using System;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class RestClient
    {
        private string apiKey;
		private AuthHeaderGenerator headerGen;
        private Support support = new Support();

        private ProxyConfiguration proxyConfiguration;
        private readonly Boolean allowAllSSLCertificates;
        private IDictionary<string, string> additionalHeaders = new Dictionary<string, string>();

        public RestClient(string apiKey) : this(apiKey, false){}

        public RestClient(string apiKey, Boolean allowAllSSLCertificates)
            : this(apiKey, allowAllSSLCertificates, null) {}

        public RestClient(string apiKey, ProxyConfiguration proxyConfiguration)
            : this(apiKey, false, proxyConfiguration) {}

        public RestClient(string apiKey, Boolean allowAllSSLCertificates, ProxyConfiguration proxyConfiguration)
            : this (apiKey, allowAllSSLCertificates, proxyConfiguration, new Dictionary<string, string> ()) {}

        public RestClient (string apiKey, Boolean allowAllSSLCertificates, ProxyConfiguration proxyConfiguration, IDictionary<string, string> headers)
        {
            this.apiKey = apiKey;
            this.allowAllSSLCertificates = allowAllSSLCertificates;
            this.proxyConfiguration = proxyConfiguration;
            this.additionalHeaders = headers;

            if (allowAllSSLCertificates) {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
        }

        public RestClient (ApiTokenConfig apiTokenConfig, Boolean allowAllSSLCertificates, ProxyConfiguration proxyConfiguration, IDictionary<string, string> headers)
        {
            this.allowAllSSLCertificates = allowAllSSLCertificates;
            this.proxyConfiguration = proxyConfiguration;
            this.additionalHeaders = headers;

            if (allowAllSSLCertificates) {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            HttpMethods.apiTokenConfig = apiTokenConfig;
        }

        public string Post(string path, string jsonPayload) {
            support.LogRequest("POST", path, jsonPayload);

			byte[] payloadBytes = null;
			if (jsonPayload != null)
			{
				payloadBytes = Converter.ToBytes(jsonPayload);
			}
			else
			{
				payloadBytes = new byte[0];
			}

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.PostHttp(apiKey, path, payloadBytes, additionalHeaders);
            
            String response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string Post(string path, string jsonPayload, string sessionId) {
            support.LogRequest("POST", path, jsonPayload);

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);

            byte[] payloadBytes = null;
            if (jsonPayload != null)
            {
                payloadBytes = Converter.ToBytes(jsonPayload);
            }
            else
            {
                payloadBytes = new byte[0];
            }

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.PostHttp(headerGen, path, payloadBytes, additionalHeaders);

            String response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string Put(string path, string jsonPayload) {
            support.LogRequest("PUT", path, jsonPayload);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.PutHttp(apiKey, path, Converter.ToBytes(jsonPayload), additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);
            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string json) {
            support.LogRequest("POST", path, json);

            headerGen = new ApiTokenAuthHeaderGenerator(apiKey);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            string response = HttpMethods.MultipartPostHttp(apiKey, path, fileBytes, boundary, headerGen, additionalHeaders);
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string sessionId, string json) {
            support.LogRequest("POST", path, json);

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            string response = HttpMethods.MultipartPostHttp(apiKey, path, fileBytes, boundary, headerGen, additionalHeaders);
            support.LogResponse(response);

            return response;
		}

        public string PostMultipartPackage(string path, byte[] content, string boundary, string json) {
            support.LogRequest("POST", path, json);

            headerGen = new ApiTokenAuthHeaderGenerator(apiKey);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            string response = HttpMethods.MultipartPostHttp(apiKey, path, content, boundary, headerGen, additionalHeaders);
            support.LogResponse(response);

            return response;
        }

        public string Get(string path) {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            DownloadedFile responseBytes = HttpMethods.GetHttpJson(apiKey, path, HttpMethods.ESL_ACCEPT_TYPE_APPLICATION_JSON, additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }
        
        public string Get(string path, string acceptType)
        {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            DownloadedFile responseBytes = HttpMethods.GetHttpJson(apiKey, path, acceptType, additionalHeaders);
            return Converter.ToString(responseBytes);
        }

        public DownloadedFile GetBytes(string path) {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            return HttpMethods.GetHttp(apiKey, path, additionalHeaders);
        }

        public DownloadedFile GetBytes(string path, string acceptType) {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            return HttpMethods.GetHttpJson(apiKey, path, acceptType, additionalHeaders);
        }

        public DownloadedFile GetHttpAsOctetStream(string path) {
            support.LogRequest("GET", path);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            return HttpMethods.GetHttpAsOctetStream(apiKey, path, additionalHeaders);
        }

        public string Delete(string path) {
            support.LogRequest("DELETE", path);

            if (proxyConfiguration != null) 
                HttpMethods.proxyConfiguration = proxyConfiguration;

            byte[] responseBytes = HttpMethods.DeleteHttp(apiKey, path, additionalHeaders);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public void Delete(string path, string jsonPayload, string sessionId)
        {
            support.LogRequest ("DELETE", path);

            byte [] payloadBytes = null;
            if (jsonPayload != null) 
            {
                payloadBytes = Converter.ToBytes (jsonPayload);
            } 
            else 
            {
                payloadBytes = new byte [0];
            }

            if (proxyConfiguration != null)
                HttpMethods.proxyConfiguration = proxyConfiguration;

            headerGen = new SessionIdAuthHeaderGenerator (sessionId);
            byte [] responseBytes = HttpMethods.DeleteHttp (headerGen, path, payloadBytes, additionalHeaders);
        }

        public string Delete (string path, string jsonPayload)
        {

            byte [] payloadBytes = null;
            if (jsonPayload != null) {
                payloadBytes = Converter.ToBytes (jsonPayload);
            } 
            else 
            {
                payloadBytes = new byte [0];
            }

            if (proxyConfiguration != null)
                HttpMethods.proxyConfiguration = proxyConfiguration;

            byte [] responseBytes = HttpMethods.DeleteHttp (apiKey, path, payloadBytes, additionalHeaders);
            string response = Converter.ToString (responseBytes);
            support.LogResponse (response);

            return response;
        }

    }
}

