using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using Silanis.ESL.API;

namespace SDK.Tests
{
    [TestFixture()]
    public class EventNotificationConverterTest
    {
		private Silanis.ESL.API.CallbackEvent apiCallbackEvent1;
		private Silanis.ESL.SDK.NotificationEvent sdkNotificationEvent1;

		[Test()]
		public void convertAPIToSDK() {
			apiCallbackEvent1 = createTypicalAPICallbackEvent();
			sdkNotificationEvent1 = new EventNotificationConverter(apiCallbackEvent1).ToSDKNotificationEvent();

			Assert.AreEqual(sdkNotificationEvent1.ToString(), apiCallbackEvent1.ToString());
		}

		[Test()]
		public void convertSDKToAPI() {
			sdkNotificationEvent1 = createTypicalSDKNotificationEvent();
			apiCallbackEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

			Assert.AreEqual(apiCallbackEvent1.ToString(), sdkNotificationEvent1.ToString());
		}

		private Silanis.ESL.API.CallbackEvent createTypicalAPICallbackEvent() {
			return CallbackEvent.PACKAGE_COMPLETE;
		}

		private Silanis.ESL.SDK.NotificationEvent createTypicalSDKNotificationEvent() {
			return NotificationEvent.PACKAGE_DECLINE;
		}
    }
}
