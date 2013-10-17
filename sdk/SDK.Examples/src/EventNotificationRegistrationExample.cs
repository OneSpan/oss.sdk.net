using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class EventNotificationRegistrationExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

        public static void Main (string[] args)
        {
            // Create new esl client with api token and base url
            EslClient client = new EslClient (apiToken, baseUrl);

            client.EventNotificationService.Register(EventNotificationConfigBuilder("http://my.url.com")
                                                        .ForEvent(NotificationEvent.PACKAGE_ACTIVATE)
                                                        .ForEvent(NotificationEvent.PACKAGE_COMPLETE)
                                                        .ForEvent(NotificationEvent.PACKAGE_OPT_OUT));
        }
    }
}

