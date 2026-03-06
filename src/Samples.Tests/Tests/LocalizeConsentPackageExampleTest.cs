using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Globalization;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class LocalizeConsentPackageExampleTest
    {
        [Test]
        public void verify() {
            // Asserts that are commented out are so because updating them is not currently supported by the oss server.
        
            LocalizeConsentPackageExample example = new LocalizeConsentPackageExample();
            example.Run();
            ConsentLocalizationData response = example.result;
            
            Assert.NotNull(response);
            Assert.AreEqual("default-consent", response.ConsentId);
            Assert.AreEqual(example.PackageId.Id, response.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual(example.OLD_LANGUAGE, response.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("esignlive", response.ConsentMetadata.Properties.AccountId);
            Assert.AreEqual(example.NEW_LANGUAGE, response.ConsentMetadata.Properties.Language);
            Assert.AreEqual("esignlive", response.ConsentMetadata.Document.AccountId);
            Assert.AreEqual(example.NEW_LANGUAGE, response.ConsentMetadata.Document.Language);
        }
        
        
    }
}
