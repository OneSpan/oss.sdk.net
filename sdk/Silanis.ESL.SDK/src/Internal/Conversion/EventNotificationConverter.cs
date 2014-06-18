using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	internal class EventNotificationConverter
    {
		private Silanis.ESL.SDK.NotificationEvent sdkNotificationEvent;
		private Silanis.ESL.API.CallbackEvent apiCallbackEvent;

		/// <summary>
		/// Construct with SDK notification event object involved in conversion.
		/// </summary>
		/// <param name="sdkNotificationEvent">SDK notification event.</param>
		public EventNotificationConverter(Silanis.ESL.SDK.NotificationEvent sdkNotificationEvent)
        {
			this.sdkNotificationEvent = sdkNotificationEvent;
        }

		/// <summary>
		/// Construct with API callback event object involved in conversion.
		/// </summary>
		/// <param name="apiCallbackEvent">API callback event.</param>
		public EventNotificationConverter(Silanis.ESL.API.CallbackEvent apiCallbackEvent)
		{
			this.apiCallbackEvent = apiCallbackEvent;
		}

		/// <summary>
		/// Convert from SDK notification event to API callback event.
		/// </summary>
		/// <returns>The API callback event.</returns>
		internal Silanis.ESL.API.CallbackEvent ToAPICallbackEvent()
		{
			switch (sdkNotificationEvent)
			{
				case NotificationEvent.PACKAGE_ACTIVATE:
					return CallbackEvent.PACKAGE_ACTIVATE;
				case NotificationEvent.PACKAGE_COMPLETE:
					return CallbackEvent.PACKAGE_COMPLETE;
				case NotificationEvent.PACKAGE_EXPIRE:
					return CallbackEvent.PACKAGE_DELETE;
				case NotificationEvent.PACKAGE_OPT_OUT:
					return CallbackEvent.PACKAGE_OPT_OUT;
				case NotificationEvent.PACKAGE_DECLINE:
					return CallbackEvent.PACKAGE_DECLINE;
				case NotificationEvent.SIGNER_COMPLETE:
					return CallbackEvent.SIGNER_COMPLETE;
				case NotificationEvent.DOCUMENT_SIGNED:
					return CallbackEvent.DOCUMENT_SIGNED;
				case NotificationEvent.ROLE_REASSIGN:
					return CallbackEvent.ROLE_REASSIGN;
				case NotificationEvent.PACKAGE_CREATE:
					return CallbackEvent.PACKAGE_CREATE;
				case NotificationEvent.PACKAGE_DEACTIVATE:
					return CallbackEvent.PACKAGE_DEACTIVATE;
				case NotificationEvent.PACKAGE_READY_FOR_COMPLETION:
					return CallbackEvent.PACKAGE_READY_FOR_COMPLETE;
				case NotificationEvent.PACKAGE_TRASH:
					return CallbackEvent.PACKAGE_TRASH;
				case NotificationEvent.PACKAGE_RESTORE:
					return CallbackEvent.PACKAGE_RESTORE;
				case NotificationEvent.PACKAGE_DELETE:
					return CallbackEvent.PACKAGE_DELETE;
				default:
					throw new InvalidCastException();
			}
		}

		/// <summary>
		/// Convert from API callback event to SDK notification event.
		/// </summary>
		/// <returns>The SDK notification event.</returns>
		internal Silanis.ESL.SDK.NotificationEvent ToSDKNotificationEvent()
		{
			switch (apiCallbackEvent)
            {
				case CallbackEvent.PACKAGE_ACTIVATE:
					return NotificationEvent.PACKAGE_ACTIVATE;
				case CallbackEvent.PACKAGE_COMPLETE:
					return NotificationEvent.PACKAGE_COMPLETE;
				case CallbackEvent.PACKAGE_EXPIRE:
					return NotificationEvent.PACKAGE_DELETE;
				case CallbackEvent.PACKAGE_OPT_OUT:
					return NotificationEvent.PACKAGE_OPT_OUT;
				case CallbackEvent.PACKAGE_DECLINE:
					return NotificationEvent.PACKAGE_DECLINE;
				case CallbackEvent.SIGNER_COMPLETE:
					return NotificationEvent.SIGNER_COMPLETE;
				case CallbackEvent.DOCUMENT_SIGNED:
					return NotificationEvent.DOCUMENT_SIGNED;
				case CallbackEvent.ROLE_REASSIGN:
					return NotificationEvent.ROLE_REASSIGN;
				case CallbackEvent.PACKAGE_CREATE:
					return NotificationEvent.PACKAGE_CREATE;
				case CallbackEvent.PACKAGE_DEACTIVATE:
					return NotificationEvent.PACKAGE_DEACTIVATE;
				case CallbackEvent.PACKAGE_READY_FOR_COMPLETE:
					return NotificationEvent.PACKAGE_READY_FOR_COMPLETION;
				case CallbackEvent.PACKAGE_TRASH:
					return NotificationEvent.PACKAGE_TRASH;
				case CallbackEvent.PACKAGE_RESTORE:
					return NotificationEvent.PACKAGE_RESTORE;
				case CallbackEvent.PACKAGE_DELETE:
					return NotificationEvent.PACKAGE_DELETE;
                default:
                    throw new InvalidCastException();
            }
		}
    }
}
