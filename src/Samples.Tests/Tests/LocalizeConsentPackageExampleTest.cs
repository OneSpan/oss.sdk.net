using NUnit.Framework;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class LocalizeConsentPackageExampleTest
    {
        [Test]
        public void verify() {
        
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
