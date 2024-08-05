using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Linq;


namespace SDK.Tests
{
    [TestFixture]
    public class AccountLimitSupportedLanguagesConverterTest
    {
        
        private SupportedLanguages sdkAccountSupportedLanguages = null;
        private OneSpanSign.API.SupportedLanguages apiAccountSupportedLanguages = null;
        private AccountLimitSupportedLanguagesConverter converter = null;

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiAccountSupportedLanguages = null;
            converter = new AccountLimitSupportedLanguagesConverter(apiAccountSupportedLanguages);
            Assert.IsNull(converter.toSDKAccountLimitSupportedLanguagesSettings());

        }

        [Test()]
         public void ConvertNullSDKToSDK()
         {
             sdkAccountSupportedLanguages = null;
             converter = new AccountLimitSupportedLanguagesConverter(sdkAccountSupportedLanguages);
             Assert.IsNull(converter.toSDKAccountLimitSupportedLanguagesSettings());
         }
 
         [Test()]
         public void ConvertSDKToSDK()
         {
             sdkAccountSupportedLanguages = CreateTypicalSDKAccountLimitSupportedLanguagesSettings();
             converter = new AccountLimitSupportedLanguagesConverter(sdkAccountSupportedLanguages);
             SupportedLanguages accountSupportedLanguages = converter.toSDKAccountLimitSupportedLanguagesSettings();
 
             Assert.IsNotNull(accountSupportedLanguages);
             Assert.AreEqual(sdkAccountSupportedLanguages, accountSupportedLanguages);
         }

         [Test()]
         public void ConvertAPIToSDK()
         {
             apiAccountSupportedLanguages = CreateTypicalAPIAccountLimitSupportedLanguagesSettings();
             converter = new AccountLimitSupportedLanguagesConverter(apiAccountSupportedLanguages);
             OneSpanSign.API.SupportedLanguages accountSupportedLanguages = converter.toAPIAccountLimitSupportedLanguagesSettings();

             Assert.IsNotNull(accountSupportedLanguages);  Assert.IsNotNull(accountSupportedLanguages);
             Assert.AreEqual(accountSupportedLanguages.DefaultSignerLanguage, "ja");
             Assert.True(CompareLists(accountSupportedLanguages.SignerLanguages, new List<string>{"en","ja"}));
         }

         [Test()]
         public void ConvertNullSDKToAPI()
         {
             sdkAccountSupportedLanguages = null;
             converter = new AccountLimitSupportedLanguagesConverter(sdkAccountSupportedLanguages);
             OneSpanSign.API.SupportedLanguages accountSupportedLanguages = converter.toAPIAccountLimitSupportedLanguagesSettings();

             Assert.IsNull(accountSupportedLanguages);
         }
 
         [Test()]
         public void ConvertNullAPIToAPI()
         {
             apiAccountSupportedLanguages = null;
             converter = new AccountLimitSupportedLanguagesConverter(apiAccountSupportedLanguages);
 
             Assert.IsNull(converter.toAPIAccountLimitSupportedLanguagesSettings());
         }
 
         [Test()]
         public void ConvertAPIToAPI()
         {
             apiAccountSupportedLanguages = CreateTypicalAPIAccountLimitSupportedLanguagesSettings();
             converter = new AccountLimitSupportedLanguagesConverter(apiAccountSupportedLanguages);
 
             OneSpanSign.API.SupportedLanguages accountSupportedLanguages = converter.toAPIAccountLimitSupportedLanguagesSettings();
             
             Assert.IsNotNull(accountSupportedLanguages);
             Assert.AreEqual(accountSupportedLanguages.DefaultSignerLanguage, "ja");
             Assert.True(CompareLists(accountSupportedLanguages.SignerLanguages, new List<string>{"en","ja"}));
         }
 
         [Test()]
         public void ConvertSDKToAPI()
         {
             sdkAccountSupportedLanguages = CreateTypicalSDKAccountLimitSupportedLanguagesSettings();
             converter = new AccountLimitSupportedLanguagesConverter(sdkAccountSupportedLanguages);
 
             OneSpanSign.API.SupportedLanguages accountSupportedLanguages = converter.toAPIAccountLimitSupportedLanguagesSettings();
 
             Assert.IsNotNull(accountSupportedLanguages);
             Assert.AreEqual(accountSupportedLanguages.DefaultSignerLanguage, "en");
             Assert.True(CompareLists(accountSupportedLanguages.SignerLanguages, new List<string>{"en","ja"}));
         }
 
         private SupportedLanguages CreateTypicalSDKAccountLimitSupportedLanguagesSettings()
         {
             SupportedLanguages accountSupportedLanguages = new SupportedLanguages();
             accountSupportedLanguages.DefaultSignerLanguage = "en";
             accountSupportedLanguages.SignerLanguages = new List<string>(){"en","ja"};
 
             return accountSupportedLanguages;
         }
 
         private OneSpanSign.API.SupportedLanguages CreateTypicalAPIAccountLimitSupportedLanguagesSettings()
         {
             OneSpanSign.API.SupportedLanguages accountSupportedLanguages = new OneSpanSign.API.SupportedLanguages();
             accountSupportedLanguages.DefaultSignerLanguage = "ja";
             accountSupportedLanguages.SignerLanguages = new List<string>(){"en","ja"};
 
             return accountSupportedLanguages;
         }
         
         private static bool CompareLists(List<string> list1, List<string> list2) {
             // Check if lists are of the same size
             if (list1.Count() != list2.Count()) {
                 return false;
             }
             // Iterate through each element and compare
             for (int i = 0; i < list1.Count(); i++) {
                 if (!list1[i].Equals(list2[i])) {
                     return false;
                 }
             }
             return true;
         }
    }
}
