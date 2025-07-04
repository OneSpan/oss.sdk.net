using NUnit.Framework;
using System;
using OneSpanSign.API;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class NotificationMethodConverterTest
    {
        private OneSpanSign.Sdk.NotificationMethod sdkNotificationMethod;
        private OneSpanSign.API.NotificationMethod apiNotificationMethod;

		[Test]
		public void ConvertAPIToSDK()
		{
            apiNotificationMethod = OneSpanSign.API.NotificationMethod.EMAIL;
            sdkNotificationMethod = new NotificationMethodConverter(apiNotificationMethod).ToSDKNotificationMethod();

			Assert.AreEqual(apiNotificationMethod.ToString(), sdkNotificationMethod.ToString());
            
            apiNotificationMethod = OneSpanSign.API.NotificationMethod.SMS;
            sdkNotificationMethod = new NotificationMethodConverter(apiNotificationMethod).ToSDKNotificationMethod();
            
            Assert.AreEqual(apiNotificationMethod.ToString(), sdkNotificationMethod.ToString());
		}
        
        [Test]
        public void ConvertSDKToAPI()
        {
            sdkNotificationMethod = OneSpanSign.Sdk.NotificationMethod.EMAIL;
            apiNotificationMethod = new NotificationMethodConverter(sdkNotificationMethod).ToAPINotificationMethod();

            Assert.AreEqual(apiNotificationMethod.ToString(), sdkNotificationMethod.ToString());
            
            sdkNotificationMethod = OneSpanSign.Sdk.NotificationMethod.SMS;
            apiNotificationMethod = new NotificationMethodConverter(sdkNotificationMethod).ToAPINotificationMethod();
            
            Assert.AreEqual(apiNotificationMethod.ToString(), sdkNotificationMethod.ToString());
        }
        
    }

}
