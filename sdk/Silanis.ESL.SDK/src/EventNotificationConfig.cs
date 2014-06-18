using System;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class EventNotificationConfig
	{
		private string url;
		private List<NotificationEvent> notificationEvents;

		public EventNotificationConfig(string url)
		{
			this.url = url;
			notificationEvents = new List<NotificationEvent>();
		}

		public string Url
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}

		public List<NotificationEvent> NotificationEvents
		{
			get
			{
				return notificationEvents;
			}
			set
			{
				notificationEvents = value;
			}
		}

		public void AddEvent(NotificationEvent notificationEvent)
		{
			this.notificationEvents.Add(notificationEvent);
		}
	}
}
