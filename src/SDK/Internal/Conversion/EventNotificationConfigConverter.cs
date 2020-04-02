using System;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
	internal class EventNotificationConfigConverter
	{
		private OneSpanSign.API.Callback apiCallback = null;
		private OneSpanSign.Sdk.EventNotificationConfig sdkEventNotificationConfig = null;

		public EventNotificationConfigConverter(OneSpanSign.API.Callback apiCallback)
		{
			this.apiCallback = apiCallback;
		}

		public EventNotificationConfigConverter(OneSpanSign.Sdk.EventNotificationConfig sdkEventNotificationConfig)
		{
			this.sdkEventNotificationConfig = sdkEventNotificationConfig;
		}

		public OneSpanSign.API.Callback ToAPICallback()
		{
			if (sdkEventNotificationConfig == null)
			{
				return apiCallback;
			}

			Callback callback = new Callback();
            callback.Url = sdkEventNotificationConfig.Url;
			callback.Key = sdkEventNotificationConfig.Key;
			foreach (NotificationEvent notificationEvent in sdkEventNotificationConfig.NotificationEvents)
			{
				callback.AddEvent(new EventNotificationConverter(notificationEvent).ToAPICallbackEvent());
			}

			return callback;
		}

		public OneSpanSign.Sdk.EventNotificationConfig ToSDKEventNotificationConfig()
		{
			if (apiCallback == null)
			{
				return sdkEventNotificationConfig;
			}

			EventNotificationConfig eventNotificationConfig = new EventNotificationConfig(apiCallback.Url);
            eventNotificationConfig.Key = apiCallback.Key;
			foreach (string callbackEvent in apiCallback.Events)
			{
				eventNotificationConfig.AddEvent(new EventNotificationConverter(callbackEvent).ToSDKNotificationEvent());
			}

			return eventNotificationConfig;
		}
	}
}
