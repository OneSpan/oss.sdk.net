using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class SystemService
    {
        private JsonSerializerSettings settings;
        private RestClient restClient;
        private string baseUrl;

        public SystemService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.settings = settings;
            this.baseUrl = baseUrl;
        }

        public string GetApplicationVersion() 
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SYSTEM_PATH)
                .Build();

            try
            {
                string response = restClient.Get(path);
                Dictionary<string, string> systemInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(response, settings);
                return systemInfo["version"];
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get application version." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get application version." + " Exception: " + e.Message, e);
            }
        }
    }
}

