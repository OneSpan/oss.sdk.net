using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountEmailReminderSettingsConverterTest
    {
        private AccountEmailReminderSettings sdkAccountEmailReminderSettings = null;
        private OneSpanSign.API.AccountEmailReminderSettings apiAccountEmailReminderSettings = null;
        private AccountEmailReminderSettingsConverter converter = null;
        
          [Test()]
         public void ConvertNullAPIToSDK()
         {
             apiAccountEmailReminderSettings = null;
             converter = new AccountEmailReminderSettingsConverter(apiAccountEmailReminderSettings);
             Assert.IsNull(converter.ToSDKAccountEmailReminderSettings());
         }
 
         [Test()]
         public void ConvertNullSDKToSDK()
         {
             sdkAccountEmailReminderSettings = null;
             converter = new AccountEmailReminderSettingsConverter(sdkAccountEmailReminderSettings);
             Assert.IsNull(converter.ToSDKAccountEmailReminderSettings());
         }
 
         [Test()]
         public void ConvertSDKToSDK()
         {
             sdkAccountEmailReminderSettings = CreateTypicalSDKAccountEmailReminderSettings();
             converter = new AccountEmailReminderSettingsConverter(sdkAccountEmailReminderSettings);
             AccountEmailReminderSettings accountEmailReminderSettings = converter.ToSDKAccountEmailReminderSettings();
 
             Assert.IsNotNull(accountEmailReminderSettings);
             Assert.AreEqual(sdkAccountEmailReminderSettings, accountEmailReminderSettings);
         }

         [Test()]
         public void ConvertAPIToSDK()
         {
             apiAccountEmailReminderSettings = CreateTypicalAPIAccountEmailReminderSettings();
             converter = new AccountEmailReminderSettingsConverter(apiAccountEmailReminderSettings);
             AccountEmailReminderSettings accountEmailReminderSettings = converter.ToSDKAccountEmailReminderSettings();

             Assert.IsNotNull(accountEmailReminderSettings);
             Assert.AreEqual(accountEmailReminderSettings.StartInDaysDelay, 30);
             Assert.AreEqual(accountEmailReminderSettings.RepetitionsCount, 3);
             Assert.AreEqual(accountEmailReminderSettings.IntervalInDays, 300);
         }

         [Test()]
         public void ConvertNullSDKToAPI()
         {
             sdkAccountEmailReminderSettings = null;
             converter = new AccountEmailReminderSettingsConverter(sdkAccountEmailReminderSettings);
             OneSpanSign.API.AccountEmailReminderSettings accountEmailReminderSettings = converter.ToAPIAccountEmailReminderSettings();

             Assert.IsNull(accountEmailReminderSettings);
         }
 
         [Test()]
         public void ConvertNullAPIToAPI()
         {
             apiAccountEmailReminderSettings = null;
             converter = new AccountEmailReminderSettingsConverter(apiAccountEmailReminderSettings);
 
             Assert.IsNull(converter.ToAPIAccountEmailReminderSettings());
         }
 
         [Test()]
         public void ConvertAPIToAPI()
         {
             apiAccountEmailReminderSettings = CreateTypicalAPIAccountEmailReminderSettings();
             converter = new AccountEmailReminderSettingsConverter(apiAccountEmailReminderSettings);
 
             OneSpanSign.API.AccountEmailReminderSettings accountEmailReminderSettings = converter.ToAPIAccountEmailReminderSettings();
 
             Assert.IsNotNull(accountEmailReminderSettings);
             Assert.AreEqual(accountEmailReminderSettings.StartInDaysDelay, 30);
             Assert.AreEqual(accountEmailReminderSettings.RepetitionsCount, 3);
             Assert.AreEqual(accountEmailReminderSettings.IntervalInDays, 300);
         }
 
         [Test()]
         public void ConvertSDKToAPI()
         {
             sdkAccountEmailReminderSettings = CreateTypicalSDKAccountEmailReminderSettings();
             converter = new AccountEmailReminderSettingsConverter(sdkAccountEmailReminderSettings);
 
             OneSpanSign.API.AccountEmailReminderSettings accountEmailReminderSettings = converter.ToAPIAccountEmailReminderSettings();
 
             Assert.IsNotNull(accountEmailReminderSettings);
             Assert.AreEqual(accountEmailReminderSettings.StartInDaysDelay, 20);
             Assert.AreEqual(accountEmailReminderSettings.RepetitionsCount, 2);
             Assert.AreEqual(accountEmailReminderSettings.IntervalInDays, 200);
         }
 
         private AccountEmailReminderSettings CreateTypicalSDKAccountEmailReminderSettings()
         {
             AccountEmailReminderSettings accountEmailReminderSettings = new AccountEmailReminderSettings();
             accountEmailReminderSettings.RepetitionsCount = 2;
             accountEmailReminderSettings.StartInDaysDelay = 20;
             accountEmailReminderSettings.IntervalInDays = 200;
 
             return accountEmailReminderSettings;
         }
 
         private OneSpanSign.API.AccountEmailReminderSettings CreateTypicalAPIAccountEmailReminderSettings()
         {
             OneSpanSign.API.AccountEmailReminderSettings accountEmailReminderSettings = new OneSpanSign.API.AccountEmailReminderSettings();
             accountEmailReminderSettings.RepetitionsCount = 3;
             accountEmailReminderSettings.StartInDaysDelay = 30;
             accountEmailReminderSettings.IntervalInDays = 300;
 
             return accountEmailReminderSettings;
         }
    }
}