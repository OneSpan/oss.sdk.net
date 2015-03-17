using System;
using Silanis.ESL.SDK;
using System.IO;

namespace SDK.Examples
{
	public class EventNotificationRegistrationExample : SDKSample
	{
		private EventNotificationConfig eventNotificationConfig;
		public string URL = "http://my.url.com";

        public readonly NotificationEvent EVENT1 = NotificationEvent.PACKAGE_CREATE;
        public readonly NotificationEvent EVENT2 = NotificationEvent.PACKAGE_ACTIVATE;
        public readonly NotificationEvent EVENT3 = NotificationEvent.PACKAGE_DEACTIVATE;
        public readonly NotificationEvent EVENT4 = NotificationEvent.PACKAGE_READY_FOR_COMPLETION;
        public readonly NotificationEvent EVENT5 = NotificationEvent.PACKAGE_COMPLETE;
        public readonly NotificationEvent EVENT6 = NotificationEvent.PACKAGE_TRASH;
        public readonly NotificationEvent EVENT7 = NotificationEvent.PACKAGE_RESTORE;
        public readonly NotificationEvent EVENT8 = NotificationEvent.PACKAGE_DELETE;
        public readonly NotificationEvent EVENT9 = NotificationEvent.PACKAGE_DECLINE;
        public readonly NotificationEvent EVENT10 = NotificationEvent.PACKAGE_EXPIRE;
        public readonly NotificationEvent EVENT11 = NotificationEvent.PACKAGE_OPT_OUT;
        public readonly NotificationEvent EVENT12 = NotificationEvent.DOCUMENT_SIGNED;
        public readonly NotificationEvent EVENT13 = NotificationEvent.ROLE_REASSIGN;
        public readonly NotificationEvent EVENT14 = NotificationEvent.SIGNER_COMPLETE;
        public readonly NotificationEvent EVENT15 = NotificationEvent.KBA_FAILURE;
        public readonly NotificationEvent EVENT16 = NotificationEvent.EMAIL_BOUNCE;
        public readonly NotificationEvent EVENT17 = NotificationEvent.PACKAGE_ATTACHMENT;
        public readonly NotificationEvent EVENT18 = NotificationEvent.SIGNER_LOCKED;

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
                .ForEvent(EVENT3)
                .ForEvent(EVENT4)
                .ForEvent(EVENT5)
                .ForEvent(EVENT6)
                .ForEvent(EVENT7)
                .ForEvent(EVENT8)
                .ForEvent(EVENT9)
                .ForEvent(EVENT10)
                .ForEvent(EVENT11)
                .ForEvent(EVENT12)
                .ForEvent(EVENT13)
                .ForEvent(EVENT14)
                .ForEvent(EVENT15)
                .ForEvent(EVENT16)
                .ForEvent(EVENT17)
				.ForEvent(EVENT18));

			// Get the registered event notifications
			eventNotificationConfig = eslClient.EventNotificationService.GetEventNotificationConfig();
		}
	}
}
