using System;

namespace Silanis.ESL.SDK
{
    public class EventNotificationService
    {


        public EventNotificationService( string apiKey, string apiUrl )
        {
        }

        public void register( EventNotificationConfig config ) {
        }

        public void register( EventNotificationConfigBuilder builder ) {
            register( builder.build() );
        }
    }
}

