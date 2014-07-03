using System;
using Silanis.ESL.SDK;
using System.IO;

namespace SDK.Examples
{
	public class EventNotificationRegistrationExample : SDKSample
	{
		private EventNotificationConfig eventNotificationConfig;
		public const string URL = "http://my.url.com";
		public const NotificationEvent EVENT1 = NotificationEvent.PACKAGE_ACTIVATE;
		public const NotificationEvent EVENT2 = NotificationEvent.PACKAGE_COMPLETE;
		public const NotificationEvent EVENT3 = NotificationEvent.PACKAGE_OPT_OUT;

		public static void Main(string[] args)
		{
			new EventNotificationRegistrationExample(Props.GetInstance()).Run();
		}

		public EventNotificationRegistrationExample(Props props) : base(props.Get("api.url"), props.Get("api.key"))
		{
		}

		public EventNotificationConfig EventNotificationConfig
		{
			get
			{
				return eventNotificationConfig;
			}
		}

		override public void Execute()
		{
			// Register for event notification
			eslClient.EventNotificationService.Register(EventNotificationConfigBuilder.NewEventNotificationConfig(URL)
                .ForEvent(NotificationEvent.DOCUMENT_SIGNED)
                .ForEvent(NotificationEvent.PACKAGE_ACTIVATE)
                .ForEvent(NotificationEvent.PACKAGE_COMPLETE)
                .ForEvent(NotificationEvent.PACKAGE_CREATE)
                .ForEvent(NotificationEvent.PACKAGE_DEACTIVATE)
                .ForEvent(NotificationEvent.PACKAGE_DECLINE)
                .ForEvent(NotificationEvent.PACKAGE_DELETE)
                .ForEvent(NotificationEvent.PACKAGE_EXPIRE)
                .ForEvent(NotificationEvent.PACKAGE_OPT_OUT)
                .ForEvent(NotificationEvent.PACKAGE_READY_FOR_COMPLETION)
                .ForEvent(NotificationEvent.PACKAGE_RESTORE)
                .ForEvent(NotificationEvent.PACKAGE_TRASH));
//				.ForEvent(EVENT1)
//				.ForEvent(EVENT2)
//				.ForEvent(EVENT3));

			// Get the registered event notifications
			eventNotificationConfig = eslClient.EventNotificationService.GetEventNotificationConfig();
		}
	}
}
