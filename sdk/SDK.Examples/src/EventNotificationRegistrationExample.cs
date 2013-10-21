using System;
using Silanis.ESL.SDK;
using System.IO;

namespace SDK.Examples
{
    public class EventNotificationRegistrationExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new EventNotificationRegistrationExample(Props.GetInstance()).Run();
        }

        public EventNotificationRegistrationExample( Props props ) : base(props.Get("api.url"), props.Get("api.key") ) {
        }

        override public void Execute() {
            eslClient.EventNotificationService.Register(EventNotificationConfigBuilder.NewEventNotificationConfig("http://my.url.com")
                                                        .ForEvent(NotificationEvent.PACKAGE_ACTIVATE)
                                                        .ForEvent(NotificationEvent.PACKAGE_COMPLETE)
                                                        .ForEvent(NotificationEvent.PACKAGE_OPT_OUT));
        }
    }
}

