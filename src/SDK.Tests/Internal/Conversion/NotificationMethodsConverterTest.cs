using NUnit.Framework;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using NotificationMethods = OneSpanSign.API.NotificationMethods;

namespace SDK.Tests
{
    public class NotificationMethodsConverterTest
    {
        private NotificationMethodsConverter converter = null;
        private OneSpanSign.Sdk.NotificationMethods sdkNotificationMethods;
        private OneSpanSign.API.NotificationMethods apiNotificationMethods;

        [Test]
        public void ConvertNullAPIToSDK()
        {
            apiNotificationMethods = null;
            sdkNotificationMethods = new NotificationMethodsConverter(apiNotificationMethods).ToSDKNotificationMethods();

            Assert.IsNull(sdkNotificationMethods);
        }
        
        [Test]
        public void ConvertNullSDKToAPI()
        {
            sdkNotificationMethods = null;
            apiNotificationMethods = new NotificationMethodsConverter(sdkNotificationMethods).ToAPINotificationMethods();

            Assert.IsNull(apiNotificationMethods);
        }

        [Test]
        public void ConvertNullAPIToAPI()
        {
            apiNotificationMethods = null;

            Assert.IsNull(new NotificationMethodsConverter(apiNotificationMethods).ToAPINotificationMethods());
        }

        [Test]
        public void ConvertNullSDKToSDK()
        {
            sdkNotificationMethods = null;

            Assert.IsNull(new NotificationMethodsConverter(sdkNotificationMethods).ToSDKNotificationMethods());
            
        }


        [Test]
        public void ConvertAPIToAPI()
        {
            apiNotificationMethods = ConstructAPIEmailNM();
            Assert.AreEqual(apiNotificationMethods, new NotificationMethodsConverter(apiNotificationMethods).ToAPINotificationMethods());

            apiNotificationMethods = ConstructAPIEmailAndSmsNM();
            Assert.AreEqual(apiNotificationMethods, new NotificationMethodsConverter(apiNotificationMethods).ToAPINotificationMethods());
        }

        [Test]
        public void ConvertSDKToSDK()
        {
            sdkNotificationMethods = ConstructSDKEmailNM();
            Assert.AreEqual(sdkNotificationMethods, new NotificationMethodsConverter(sdkNotificationMethods).ToSDKNotificationMethods());

            sdkNotificationMethods = ConstructSDKEmailAndSmsNM();
            Assert.AreEqual(sdkNotificationMethods, new NotificationMethodsConverter(sdkNotificationMethods).ToSDKNotificationMethods());

        }

        [Test]
        public void ConvertAPIToSDKAndViceVersa()
        {
            apiNotificationMethods = ConstructAPIEmailNM();
            sdkNotificationMethods = ConstructSDKEmailNM();
            OneSpanSign.Sdk.NotificationMethods sdkNotificationMethods2 = new NotificationMethodsConverter(apiNotificationMethods).ToSDKNotificationMethods();
            AssertSdkNMEquality(new NotificationMethodsConverter(apiNotificationMethods).ToSDKNotificationMethods(), sdkNotificationMethods);
            AssertApiNMEquality(new NotificationMethodsConverter(sdkNotificationMethods).ToAPINotificationMethods(), apiNotificationMethods);
        }

        private OneSpanSign.API.NotificationMethods ConstructAPIEmailNM()
        {
            HashSet<OneSpanSign.API.NotificationMethod> apiNMSet = new HashSet<OneSpanSign.API.NotificationMethod>();
            apiNMSet.Add(OneSpanSign.API.NotificationMethod.EMAIL);
            OneSpanSign.API.NotificationMethods apiNM = new NotificationMethods(apiNMSet);

            return apiNM;
        }
        
        private OneSpanSign.Sdk.NotificationMethods ConstructSDKEmailNM()
        {
            OneSpanSign.Sdk.NotificationMethods sdkNM = new OneSpanSign.Sdk.NotificationMethods();
            sdkNM.AddPrimaryMethods(OneSpanSign.Sdk.NotificationMethod.EMAIL);

            return sdkNM;
        }
        
        private OneSpanSign.API.NotificationMethods ConstructAPIEmailAndSmsNM()
        {
            HashSet<OneSpanSign.API.NotificationMethod> apiNMSet = new HashSet<OneSpanSign.API.NotificationMethod>();
            apiNMSet.Add(OneSpanSign.API.NotificationMethod.EMAIL);
            apiNMSet.Add(OneSpanSign.API.NotificationMethod.SMS);
            OneSpanSign.API.NotificationMethods apiNM = new NotificationMethods(apiNMSet);

            return apiNM;
        }
        
        private OneSpanSign.Sdk.NotificationMethods ConstructSDKEmailAndSmsNM()
        {
            OneSpanSign.Sdk.NotificationMethods sdkNM = new OneSpanSign.Sdk.NotificationMethods();
            sdkNM.Phone = "+15236286354";
            sdkNM.AddPrimaryMethods(OneSpanSign.Sdk.NotificationMethod.EMAIL, NotificationMethod.SMS);
            
            return sdkNM;
        }
        
        private void AssertApiNMEquality(OneSpanSign.API.NotificationMethods expected, OneSpanSign.API.NotificationMethods actual)
        {
            Assert.AreEqual(expected.Primary?.Count, actual.Primary?.Count, "Primary count mismatch");
    
            if (expected.Primary != null && actual.Primary != null)
            {
                Assert.IsTrue(expected.Primary.SetEquals(actual.Primary), "Primary collections don't match");
            }
        }
        
        private void AssertSdkNMEquality(OneSpanSign.Sdk.NotificationMethods expected, OneSpanSign.Sdk.NotificationMethods actual)
        {
            Assert.AreEqual(expected.Primary?.Count, actual.Primary?.Count, "Primary count mismatch");
    
            if (expected.Primary != null && actual.Primary != null)
            {
                Assert.IsTrue(expected.Primary.SetEquals(actual.Primary), "Primary collections don't match");
            }
        }
    }
}