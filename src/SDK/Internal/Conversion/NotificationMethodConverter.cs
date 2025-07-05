using System;

namespace OneSpanSign.Sdk
{
    public class NotificationMethodConverter
    {
        private NotificationMethod sdkNotificationMethod;
        private OneSpanSign.API.NotificationMethod apiNotificationMethod;

        public NotificationMethodConverter(OneSpanSign.Sdk.NotificationMethod sdkMethod)
        {
            this.sdkNotificationMethod = sdkMethod;
        }

        public NotificationMethodConverter(OneSpanSign.API.NotificationMethod apiMethod)
        {
            this.apiNotificationMethod = apiMethod;
        }

        public OneSpanSign.API.NotificationMethod ToAPINotificationMethod()
        {
            return (OneSpanSign.API.NotificationMethod)Enum.Parse(
                typeof(OneSpanSign.API.NotificationMethod), 
                sdkNotificationMethod.ToString(), 
                true);
        }
        
        public OneSpanSign.Sdk.NotificationMethod ToSDKNotificationMethod()
        {
            return (OneSpanSign.Sdk.NotificationMethod)Enum.Parse(
                typeof(OneSpanSign.Sdk.NotificationMethod), 
                apiNotificationMethod.ToString(), 
                true);
        }
    }
}