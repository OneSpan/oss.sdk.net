using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    
    [TestFixture ()]
    public class AccountLimitSupportedLanguagesSettingsExampleTest
    {
        private AccountLimitSupportedLanguagesSettingsExample example;

        private readonly SupportedLanguages supportedLanguages1 = GetSupportedLanguages(new List<string>() { "en", "fr" } , "fr");
        private readonly SupportedLanguages supportedLanguages2 = GetSupportedLanguages(new List<string>() { "en", "de" } , "de");
        
        [Test]
        public void VerifyResult()
        {
            example = new AccountLimitSupportedLanguagesSettingsExample();
            example.Run();
            
            //Resource is empty when not configured
            Assert.Null(example.DefaultLimitSupportedLanguagesSettings);
            
            Assert.IsTrue(CompareLists(example.PatchedLimitSupportedLanguagesSettings1.SignerLanguages, 
                supportedLanguages1.SignerLanguages));
            Assert.AreEqual(example.PatchedLimitSupportedLanguagesSettings1.DefaultSignerLanguage, 
                "fr");
            
            Assert.IsTrue(CompareLists(example.PatchedLimitSupportedLanguagesSettings2.SignerLanguages, 
                supportedLanguages2.SignerLanguages));
            Assert.AreEqual(example.PatchedLimitSupportedLanguagesSettings2.DefaultSignerLanguage, 
                "de");
            
            //Resource is empty after delete operation
            Assert.Null(example.AfterDeleteLimitSupportedLanguagesSettings);
            
        }
        
        private static SupportedLanguages GetSupportedLanguages(List<string> signerLanguages, string defaultSignerLanguage) {
            SupportedLanguages supportedLanguages = new SupportedLanguages();
            supportedLanguages.DefaultSignerLanguage = defaultSignerLanguage;
            supportedLanguages.SignerLanguages = signerLanguages;
            return supportedLanguages;
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