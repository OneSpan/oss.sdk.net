using System;
using Silanis.ESL.SDK.Internal;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class RestClient
    {
        private string apiToken;
		private AuthHeaderGenerator headerGen;
        private Support support = new Support();

        public RestClient(string apiToken)
        {
            this.apiToken = apiToken;
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
			byte[] responseBytes = HttpMethods.PostHttp(apiToken, path, payloadBytes);
            
            String response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string Put(string path, string jsonPayload) {
            support.LogRequest("PUT", path, jsonPayload);
            byte[] responseBytes = HttpMethods.PutHttp(apiToken, path, Converter.ToBytes(jsonPayload));
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);
            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string json) {
            support.LogRequest("POST", path, json);

            headerGen = new ApiTokenAuthHeaderGenerator(apiToken);

			byte[] responseBytes = HttpMethods.MultipartPostHttp(apiToken, path, fileBytes, boundary, headerGen);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary, string sessionId, string json) {
            support.LogRequest("POST", path, json);

            headerGen = new SessionIdAuthHeaderGenerator(sessionId);

			byte[] responseBytes = HttpMethods.MultipartPostHttp(apiToken, path, fileBytes, boundary, headerGen);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
		}

        public string PostMultipartPackage(string path, byte[] content, string boundary, string json) {
            support.LogRequest("POST", path, json);

            headerGen = new ApiTokenAuthHeaderGenerator(apiToken);

            byte[] responseBytes = HttpMethods.MultipartPostHttp(apiToken, path, content, boundary, headerGen);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

        public string Get(string path) {
            support.LogRequest("GET", path);

            byte[] responseBytes = HttpMethods.GetHttpJson(apiToken, path, "application/json");
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }
        
        public string Get(string path, string acceptType)
        {
            support.LogRequest("GET", path);

            byte[] responseBytes = HttpMethods.GetHttpJson(apiToken, path, acceptType);
            return Converter.ToString(responseBytes);
        }

        public byte[] GetBytes(string path) {
            support.LogRequest("GET", path);
            return HttpMethods.GetHttp(apiToken, path);
        }

        public string Delete(string path) {
            support.LogRequest("DELETE", path);

            byte[] responseBytes = HttpMethods.DeleteHttp(apiToken, path);
            string response = Converter.ToString(responseBytes);
            support.LogResponse(response);

            return response;
        }

    }
}

