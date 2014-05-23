using System;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.API;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK.Services
{
    public class EventNotificationService
    {
        private RestClient restClient;
        private UrlTemplate template;
        private JsonSerializerSettings settings;

        public EventNotificationService( RestClient restClient, string apiUrl, JsonSerializerSettings settings )
        {
            this.restClient = restClient;
            template = new UrlTemplate(apiUrl);
            this.settings = settings;
        }

        public void Register( EventNotificationConfig config ) {
            string path = template.UrlFor(UrlTemplate.CALLBACK_PATH).Build();
            Callback callback = config.ToAPICallback();
            string json = JsonConvert.SerializeObject(callback, settings);

            restClient.Post(path, json);
        }

        public void Register( EventNotificationConfigBuilder builder ) {
            Register( builder.build() );
        }
    }
}

