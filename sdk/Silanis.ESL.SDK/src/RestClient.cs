using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    public class RestClient
    {
        private string apiToken;
//        private Support support;

        public RestClient(string apiToken)
        {
            this.apiToken = apiToken;
//            support = new Support();
        }

        public string Post(string path, string jsonPayload) {
//            support.LogRequest("POST", path, jsonPayload);
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
            return Converter.ToString(responseBytes);
        }

        public string Put(string path, string jsonPayload) {
//            support.LogRequest("PUT", path, jsonPayload);
            byte[] responseBytes = HttpMethods.PutHttp(apiToken, path, Converter.ToBytes(jsonPayload));
            return Converter.ToString(responseBytes);
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary) {
            byte[] responseBytes = HttpMethods.MultipartPostHttp(apiToken, path, fileBytes, boundary);
            return Converter.ToString(responseBytes);
        }

        public string Get(string path) {
//            support.LogRequest("GET", path);
            byte[] responseBytes = HttpMethods.GetHttp(apiToken, path);
            return Converter.ToString(responseBytes);
        }

        public byte[] GetBytes(string path) {
//            support.LogRequest("GET", path);
            return HttpMethods.GetHttp(apiToken, path);
        }

        public string Delete(string path) {
//            support.LogRequest("DELETE", path);
            byte[] responseBytes = HttpMethods.DeleteHttp(apiToken, path);
            return Converter.ToString(responseBytes);
        }

    }
}

