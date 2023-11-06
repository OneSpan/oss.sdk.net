using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountDesignerSettingsConverterTest
    {  
        private AccountDesignerSettings sdkAccountDesignerSettings = null;
        private OneSpanSign.API.AccountDesignerSettings apiAccountDesignerSettings = null;
        private AccountDesignerSettingsConverter converter = null;

 
         [Test()]
         public void ConvertNullAPIToSDK()
         {
             apiAccountDesignerSettings = null;
             converter = new AccountDesignerSettingsConverter(apiAccountDesignerSettings);
             Assert.IsNull(converter.ToSDKAccountDesignerSettings());
         }
 
         [Test()]
         public void ConvertNullSDKToSDK()
         {
             sdkAccountDesignerSettings = null;
             converter = new AccountDesignerSettingsConverter(sdkAccountDesignerSettings);
             Assert.IsNull(converter.ToSDKAccountDesignerSettings());
         }
 
         [Test()]
         public void ConvertSDKToSDK()
         {
             sdkAccountDesignerSettings = CreateTypicalSDKAccountDesignerSettings();
             converter = new AccountDesignerSettingsConverter(sdkAccountDesignerSettings);
             AccountDesignerSettings accountDesignerSettings = converter.ToSDKAccountDesignerSettings();
 
             Assert.IsNotNull(accountDesignerSettings);
             Assert.AreEqual(sdkAccountDesignerSettings, accountDesignerSettings);
         }

         [Test()]
         public void ConvertAPIToSDK()
         {
             apiAccountDesignerSettings = CreateTypicalAPIAccountDesignerSettings();
             converter = new AccountDesignerSettingsConverter(apiAccountDesignerSettings);
             AccountDesignerSettings accountDesignerSettings = converter.ToSDKAccountDesignerSettings();

             Assert.IsNotNull(accountDesignerSettings);
             Assert.IsTrue(accountDesignerSettings.Send);
             Assert.IsTrue(accountDesignerSettings.Done);
             Assert.IsTrue(accountDesignerSettings.DefaultSignatureType.Equals("capture"));
         }

         [Test()]
         public void ConvertNullSDKToAPI()
         {
             sdkAccountDesignerSettings = null;
             converter = new AccountDesignerSettingsConverter(sdkAccountDesignerSettings);
             OneSpanSign.API.AccountDesignerSettings accountDesignerSettings = converter.ToAPIAccountDesignerSettings();

             Assert.IsNull(accountDesignerSettings);
         }
 
         [Test()]
         public void ConvertNullAPIToAPI()
         {
             apiAccountDesignerSettings = null;
             converter = new AccountDesignerSettingsConverter(apiAccountDesignerSettings);
 
             Assert.IsNull(converter.ToAPIAccountDesignerSettings());
         }
 
         [Test()]
         public void ConvertAPIToAPI()
         {
             apiAccountDesignerSettings = CreateTypicalAPIAccountDesignerSettings();
             converter = new AccountDesignerSettingsConverter(apiAccountDesignerSettings);
 
             OneSpanSign.API.AccountDesignerSettings accountDesignerSettings = converter.ToAPIAccountDesignerSettings();
 
             Assert.IsNotNull(accountDesignerSettings);
             
             Assert.IsTrue(accountDesignerSettings.Send);
             Assert.IsTrue(accountDesignerSettings.Done);
             Assert.IsTrue(accountDesignerSettings.DefaultSignatureType.Equals("capture"));
         }
 
         [Test()]
         public void ConvertSDKToAPI()
         {
             sdkAccountDesignerSettings = CreateTypicalSDKAccountDesignerSettings();
             converter = new AccountDesignerSettingsConverter(sdkAccountDesignerSettings);
 
             OneSpanSign.API.AccountDesignerSettings accountDesignerSettings = converter.ToAPIAccountDesignerSettings();
 
             Assert.IsNotNull(accountDesignerSettings);
             
             Assert.IsFalse(accountDesignerSettings.Send);
             Assert.IsFalse(accountDesignerSettings.Done);
             Assert.IsTrue(accountDesignerSettings.DefaultSignatureType.Equals("mobile"));
         }
 
         private AccountDesignerSettings CreateTypicalSDKAccountDesignerSettings()
         {
             AccountDesignerSettings accountDesignerSettings = new AccountDesignerSettings();
             accountDesignerSettings.Send = false;
             accountDesignerSettings.Done = false;
             accountDesignerSettings.Settings = true;
             accountDesignerSettings.DocumentVisibility = true;
             accountDesignerSettings.AddDocument = false;
             accountDesignerSettings.EditDocument = false;
             accountDesignerSettings.DeleteDocument = true;
             accountDesignerSettings.AddSigner = true;
             accountDesignerSettings.EditRecipient = false;
             accountDesignerSettings.RolePickerSender = false;
             accountDesignerSettings.SaveLayout = true;
             accountDesignerSettings.ApplyLayout = true;
             accountDesignerSettings.ShowSharedLayouts = false;
             accountDesignerSettings.DefaultSignatureType = "mobile";
 
             return accountDesignerSettings;
         }
 
         private OneSpanSign.API.AccountDesignerSettings CreateTypicalAPIAccountDesignerSettings()
         {
             OneSpanSign.API.AccountDesignerSettings accountDesignerSettings = new OneSpanSign.API.AccountDesignerSettings();
             accountDesignerSettings.Send = true;
             accountDesignerSettings.Done = true;
             accountDesignerSettings.Settings = false;
             accountDesignerSettings.DocumentVisibility = false;
             accountDesignerSettings.AddDocument = true;
             accountDesignerSettings.EditDocument = true;
             accountDesignerSettings.DeleteDocument = false;
             accountDesignerSettings.AddSigner = false;
             accountDesignerSettings.EditRecipient = true;
             accountDesignerSettings.RolePickerSender = true;
             accountDesignerSettings.SaveLayout = false;
             accountDesignerSettings.ApplyLayout = false;
             accountDesignerSettings.ShowSharedLayouts = true;
             accountDesignerSettings.DefaultSignatureType = "capture";
 
             return accountDesignerSettings;
         }
    
    }
}
