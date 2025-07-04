using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class NotificationMethodsConverter
    {
        private NotificationMethods sdkNotificationMethods = null;
        private OneSpanSign.API.NotificationMethods apiNotificationMethods = null;
        
        public NotificationMethodsConverter(NotificationMethods sdkNotificationMethods)
        {
            this.sdkNotificationMethods = sdkNotificationMethods;
        }

        public NotificationMethodsConverter(OneSpanSign.API.NotificationMethods apiNotificationMethods)
        {
            this.apiNotificationMethods = apiNotificationMethods;
        }

        public NotificationMethods ToSDKNotificationMethods()
        {
            return apiNotificationMethods == null ? sdkNotificationMethods : new NotificationMethods(ConvertNotificationMethodsToSDK(apiNotificationMethods.Primary), null);
        }

        public OneSpanSign.API.NotificationMethods ToAPINotificationMethods()
        {
            return sdkNotificationMethods == null ? apiNotificationMethods : new OneSpanSign.API.NotificationMethods(ConvertNotificationMethodsToAPI(sdkNotificationMethods.Primary));
        }
        
        public static ISet<NotificationMethod> ConvertNotificationMethodsToSDK(ISet<OneSpanSign.API.NotificationMethod> apiMethods)
        {
            ISet<NotificationMethod> methods = new HashSet<NotificationMethod>();
            foreach (OneSpanSign.API.NotificationMethod method in apiMethods)
            {
                NotificationMethodConverter converter = new NotificationMethodConverter(method);
                methods.Add(converter.ToSDKNotificationMethod());
            }
            return methods;
        }

        public static ISet<OneSpanSign.API.NotificationMethod> ConvertNotificationMethodsToAPI(ISet<NotificationMethod> sdkMethods)
        {
            ISet<OneSpanSign.API.NotificationMethod> methods = new HashSet<OneSpanSign.API.NotificationMethod>();
            foreach (NotificationMethod method in sdkMethods)
            {
                NotificationMethodConverter converter = new NotificationMethodConverter(method);
                methods.Add(converter.ToAPINotificationMethod());
            }
            return methods;
        }
    }
}