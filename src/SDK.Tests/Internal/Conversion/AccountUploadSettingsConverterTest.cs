using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountUploadSettingsConverterTest
    {
        private AccountUploadSettings sdkAccountUploadSettings = null;
        private OneSpanSign.API.AccountUploadSettings apiAccountUploadSettings = null;
        private AccountUploadSettingsConverter converter = null;
        
          [Test()]
         public void ConvertNullAPIToSDK()
         {
             apiAccountUploadSettings = null;
             converter = new AccountUploadSettingsConverter(apiAccountUploadSettings);
             Assert.IsNull(converter.ToSDKAccountUploadSettings());
         }
 
         [Test()]
         public void ConvertNullSDKToSDK()
         {
             sdkAccountUploadSettings = null;
             converter = new AccountUploadSettingsConverter(sdkAccountUploadSettings);
             Assert.IsNull(converter.ToSDKAccountUploadSettings());
         }
 
         [Test()]
         public void ConvertSDKToSDK()
         {
             sdkAccountUploadSettings = CreateTypicalSdkAccountUploadSettings();
             converter = new AccountUploadSettingsConverter(sdkAccountUploadSettings);
             AccountUploadSettings accountUploadSettings = converter.ToSDKAccountUploadSettings();
 
             Assert.IsNotNull(accountUploadSettings);
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType1"));
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType2"));
         }

         [Test()]
         public void ConvertAPIToSDK()
         {
             apiAccountUploadSettings = CreateTypicalApiAccountUploadSettings();
             converter = new AccountUploadSettingsConverter(apiAccountUploadSettings);
             AccountUploadSettings accountUploadSettings = converter.ToSDKAccountUploadSettings();

             Assert.IsNotNull(accountUploadSettings);
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType3"));
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType4"));
         }

         [Test()]
         public void ConvertNullSDKToAPI()
         {
             sdkAccountUploadSettings = null;
             converter = new AccountUploadSettingsConverter(sdkAccountUploadSettings);
             OneSpanSign.API.AccountUploadSettings accountUploadSettings = converter.ToAPIAccountUploadSettings();

             Assert.IsNull(accountUploadSettings);
         }
 
         [Test()]
         public void ConvertNullAPIToAPI()
         {
             apiAccountUploadSettings = null;
             converter = new AccountUploadSettingsConverter(apiAccountUploadSettings);
 
             Assert.IsNull(converter.ToAPIAccountUploadSettings());
         }
 
         [Test()]
         public void ConvertAPIToAPI()
         {
             apiAccountUploadSettings = CreateTypicalApiAccountUploadSettings();
             converter = new AccountUploadSettingsConverter(apiAccountUploadSettings);
 
             OneSpanSign.API.AccountUploadSettings accountUploadSettings = converter.ToAPIAccountUploadSettings();
 
             Assert.IsNotNull(accountUploadSettings);
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType3"));
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType4"));
         }
 
         [Test()]
         public void ConvertSDKToAPI()
         {
             sdkAccountUploadSettings = CreateTypicalSdkAccountUploadSettings();
             converter = new AccountUploadSettingsConverter(sdkAccountUploadSettings);
 
             OneSpanSign.API.AccountUploadSettings accountUploadSettings = converter.ToAPIAccountUploadSettings();
 
             Assert.IsNotNull(accountUploadSettings);
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType1"));
             Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("TestFileType2"));
         }
 
         private AccountUploadSettings CreateTypicalSdkAccountUploadSettings()
         {
             AccountUploadSettings accountUploadSettings = new AccountUploadSettings();
             accountUploadSettings.AllowedFileTypes = new List<string>{"TestFileType1","TestFileType2"};
             
             return accountUploadSettings;
         }
 
         private OneSpanSign.API.AccountUploadSettings CreateTypicalApiAccountUploadSettings()
         {
             OneSpanSign.API.AccountUploadSettings accountUploadSettings = new OneSpanSign.API.AccountUploadSettings();
             accountUploadSettings.AllowedFileTypes = new List<string>{"TestFileType3","TestFileType4"};
 
             return accountUploadSettings;
         }
    }
}