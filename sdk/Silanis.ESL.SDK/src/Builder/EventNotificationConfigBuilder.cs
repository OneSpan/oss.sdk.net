using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class EventNotificationConfigBuilder
    {
        private string url;
        private List<NotificationEvent> events;

        private EventNotificationConfigBuilder(string url)
        {
            this.url = url;
            this.events = new List<NotificationEvent>();
        }

        public static EventNotificationConfigBuilder NewEventNotificationConfig( string url ) {
            return new EventNotificationConfigBuilder(url);
        }

        public EventNotificationConfigBuilder ForEvent( NotificationEvent notificationEvent ) {
            events.Add( notificationEvent );
            return this;
        }

        public EventNotificationConfig build() {
            EventNotificationConfig result = new EventNotificationConfig(url);
            foreach (NotificationEvent notificationEvent in events)
            {
                result.NotificationEvents.Add(notificationEvent);
            }

            return result;
        }
    }
}

