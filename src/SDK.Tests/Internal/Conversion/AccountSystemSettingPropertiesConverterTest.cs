using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountSystemSettingPropertiesConverterTest
    {
        private AccountSystemSettingProperties sdkAccountSystemSettingProperties = null;
        private OneSpanSign.API.AccountSystemSettingProperties apiAccountSystemSettingProperties = null;
        private AccountSystemSettingPropertiesConverter converter = null;
        
          [Test()]
         public void ConvertNullAPIToSDK()
         {
             apiAccountSystemSettingProperties = null;
             converter = new AccountSystemSettingPropertiesConverter(apiAccountSystemSettingProperties);
             Assert.IsNull(converter.ToSDKAccountSystemSettingProperties());
         }
 
         [Test()]
         public void ConvertNullSDKToSDK()
         {
             sdkAccountSystemSettingProperties = null;
             converter = new AccountSystemSettingPropertiesConverter(sdkAccountSystemSettingProperties);
             Assert.IsNull(converter.ToSDKAccountSystemSettingProperties());
         }
 
         [Test()]
         public void ConvertSDKToSDK()
         {
             sdkAccountSystemSettingProperties = CreateTypicalSDKSystemSettingProperties();
             converter = new AccountSystemSettingPropertiesConverter(sdkAccountSystemSettingProperties);
             AccountSystemSettingProperties accountSystemSettingProperties = converter.ToSDKAccountSystemSettingProperties();
 
             Assert.IsNotNull(accountSystemSettingProperties);
             Assert.AreEqual(sdkAccountSystemSettingProperties, accountSystemSettingProperties);
         }

         [Test()]
         public void ConvertAPIToSDK()
         {
             apiAccountSystemSettingProperties = CreateTypicalAPISystemSettingProperties();
             converter = new AccountSystemSettingPropertiesConverter(apiAccountSystemSettingProperties);
             AccountSystemSettingProperties accountSystemSettingProperties =
                 converter.ToSDKAccountSystemSettingProperties();

             Assert.IsNotNull(accountSystemSettingProperties);
             Assert.IsTrue(accountSystemSettingProperties.SenderLoginMaxFailedAttempts.Equals(4));
             Assert.IsTrue(accountSystemSettingProperties.LoginSessionTimeout == 40000);
             Assert.IsTrue(accountSystemSettingProperties.SessionTimeoutWarning == 120000);
         }

         [Test()]
         public void ConvertNullSDKToAPI()
         {
             sdkAccountSystemSettingProperties = null;
             converter = new AccountSystemSettingPropertiesConverter(sdkAccountSystemSettingProperties);
             OneSpanSign.API.AccountSystemSettingProperties accountSystemSettingProperties = converter.ToAPIAccountSystemSettingProperties();

             Assert.IsNull(accountSystemSettingProperties);
         }
 
         [Test()]
         public void ConvertNullAPIToAPI()
         {
             apiAccountSystemSettingProperties = null;
             converter = new AccountSystemSettingPropertiesConverter(apiAccountSystemSettingProperties);
 
             Assert.IsNull(converter.ToAPIAccountSystemSettingProperties());
         }
 
         [Test()]
         public void ConvertAPIToAPI()
         {
             apiAccountSystemSettingProperties = CreateTypicalAPISystemSettingProperties();
             converter = new AccountSystemSettingPropertiesConverter(apiAccountSystemSettingProperties);
 
             OneSpanSign.API.AccountSystemSettingProperties accountSystemSettingProperties = converter.ToAPIAccountSystemSettingProperties();
 
             Assert.IsNotNull(accountSystemSettingProperties);
             Assert.IsTrue(accountSystemSettingProperties.SenderLoginMaxFailedAttempts.Equals(4));
             Assert.IsTrue(accountSystemSettingProperties.LoginSessionTimeout == 40000);
             Assert.IsTrue(accountSystemSettingProperties.SessionTimeoutWarning == 120000);
         }
 
         [Test()]
         public void ConvertSDKToAPI()
         {
             sdkAccountSystemSettingProperties = CreateTypicalSDKSystemSettingProperties();
             converter = new AccountSystemSettingPropertiesConverter(sdkAccountSystemSettingProperties);
 
             OneSpanSign.API.AccountSystemSettingProperties accountSystemSettingProperties = converter.ToAPIAccountSystemSettingProperties();
 
             Assert.IsNotNull(accountSystemSettingProperties);
             Assert.IsTrue(accountSystemSettingProperties.SenderLoginMaxFailedAttempts.Equals(2));
             Assert.IsTrue(accountSystemSettingProperties.LoginSessionTimeout == 20000);
             Assert.IsTrue(accountSystemSettingProperties.SessionTimeoutWarning == 60000);
         }
 
         private AccountSystemSettingProperties CreateTypicalSDKSystemSettingProperties()
         {
             AccountSystemSettingProperties accountSystemSettingProperties = new AccountSystemSettingProperties();

             accountSystemSettingProperties.SessionTimeoutWarning = 60000;
             accountSystemSettingProperties.LoginSessionTimeout = 20000;
             accountSystemSettingProperties.SenderLoginMaxFailedAttempts = 2;
             
             return accountSystemSettingProperties;
         }
 
         private OneSpanSign.API.AccountSystemSettingProperties CreateTypicalAPISystemSettingProperties()
         {
             OneSpanSign.API.AccountSystemSettingProperties accountSystemSettingProperties = new OneSpanSign.API.AccountSystemSettingProperties();
             
             accountSystemSettingProperties.SessionTimeoutWarning = 120000;
             accountSystemSettingProperties.LoginSessionTimeout = 40000;
             accountSystemSettingProperties.SenderLoginMaxFailedAttempts = 4;
             
             return accountSystemSettingProperties;
         }
    }
}