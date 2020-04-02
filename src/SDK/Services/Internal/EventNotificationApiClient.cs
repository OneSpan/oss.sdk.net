using System;
using Newtonsoft.Json;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class EventNotificationApiClient
    {
        private RestClient restClient;
        private UrlTemplate template;
        private JsonSerializerSettings settings;

        public EventNotificationApiClient(RestClient restClient, string apiUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            template = new UrlTemplate(apiUrl);
            this.settings = settings;
        }
        
        public void Register(Callback callback)
        {
            try
            {
                string path = template.UrlFor(UrlTemplate.CALLBACK_PATH).Build();
                string json = JsonConvert.SerializeObject(callback, settings);

                restClient.Post(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException( "Unable to configure event notification. " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw new OssException("Unable to configure event notification. " + e.Message, e);
            }
        }

        public void Register(string origin, Callback callback)
        {
            try
            {
                string path = template.UrlFor(UrlTemplate.CONNECTORS_CALLBACK_PATH)
                    .Replace("{origin}", origin)
                    .Build();
                string json = JsonConvert.SerializeObject(callback, settings);

                restClient.Post(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException( "Unable to configure event notification for this connector. " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw new OssException("Unable to configure event notification for this connector. " + e.Message, e);
            }
        }

        public Callback GetEventNotificationConfig()
        {
            string path = template.UrlFor(UrlTemplate.CALLBACK_PATH).Build();

            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<Callback>(stringResponse, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not retrieve event notification. " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not retrieve event notification. " + e.Message, e);
            }
        }

        public Callback GetEventNotificationConfig(string origin)
        {
            string path = template.UrlFor(UrlTemplate.CONNECTORS_CALLBACK_PATH)
                .Replace("{origin}", origin)
                .Build();

            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<Callback>(stringResponse, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not retrieve event notification for this connector. " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not retrieve event notification for this connector. " + e.Message, e);
            }
        }
    }
}

