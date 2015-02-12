using System;
using Silanis.ESL.SDK;
using System.IO;

namespace SDK.Examples
{
	public class EventNotificationRegistrationExample : SDKSample
	{
		private EventNotificationConfig eventNotificationConfig;
		public const string URL = "http://my.url.com";
		public static readonly NotificationEvent EVENT1 = NotificationEvent.PACKAGE_ACTIVATE;
        public static readonly NotificationEvent EVENT2 = NotificationEvent.PACKAGE_COMPLETE;
        public static readonly NotificationEvent EVENT3 = NotificationEvent.PACKAGE_OPT_OUT;

		public static void Main(string[] args)
		{
			new EventNotificationRegistrationExample(Props.GetInstance()).Run();
		}

		public EventNotificationRegistrationExample(Props props) : base(props.Get("api.key"), props.Get("api.url"))
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
				.ForEvent(EVENT1)
				.ForEvent(EVENT2)
				.ForEvent(EVENT3));

			// Get the registered event notifications
			eventNotificationConfig = eslClient.EventNotificationService.GetEventNotificationConfig();
		}
	}
}
