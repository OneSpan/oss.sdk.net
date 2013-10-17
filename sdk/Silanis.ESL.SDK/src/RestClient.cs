using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    public class RestClient
    {
        private string apiToken;
        private Support support;

        public RestClient(string apiToken)
        {
            this.apiToken = apiToken;
            support = new Support();
        }

        public string Post(string path, string jsonPayload) {
            byte[] responseBytes = HttpMethods.PostHttp(apiToken, path, Converter.ToBytes(jsonPayload));
            return Converter.ToString(responseBytes);
        }

        public string Put(string path, string jsonPayload) {
            byte[] responseBytes = HttpMethods.PutHttp(apiToken, path, Converter.ToBytes(jsonPayload));
            return Converter.ToString(responseBytes);
        }

        public string PostMultipartFile(string path, byte[] fileBytes, string boundary) {
            HttpMethods.MultipartPostHttp(apiToken, path, fileBytes, boundary);
        }

//        private static <T> T Execute() {
//        }

        public string Get(string path) {
            byte[] responseBytes = HttpMethods.GetHttp(apiToken, path);
            return Converter.ToString(responseBytes);
        }

        public byte[] GetBytes(string path) {
            return HttpMethods.GetHttp(apiToken, path);
        }

        public string delete(string path) {
            byte[] responseBytes = HttpMethods.DeleteHttp(apiToken, path);
            return Converter.ToString(responseBytes);
        }

    }
}

