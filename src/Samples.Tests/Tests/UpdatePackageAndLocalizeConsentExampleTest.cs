using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class UpdatePackageAndLocalizeConsentExampleTest
    {
        [Test]
        public void verify() {
       
            UpdatePackageAndLocalizeConsentExample example = new UpdatePackageAndLocalizeConsentExample();
            example.Run();

            assertPackage(example.createdPackage, example.packageToCreate);
            assertWorkflowResult(example.updateWorkflowResult, example.createdPackage);
        }

        private void assertPackage(DocumentPackage actualPackage, DocumentPackage expectedPackage) {
            Assert.AreEqual( expectedPackage.Name, actualPackage.Name );
            Assert.AreEqual( expectedPackage.Language, actualPackage.Language );
        }
        
        private void assertWorkflowResult(PackageUpdateWorkflowResult response, DocumentPackage createdPackage) {
            
            Assert.NotNull(response);
            Assert.NotNull(response.ConsentInfo);
            Assert.AreEqual(createdPackage.Id.Id, response.PackageUid);
            Assert.AreEqual("default-consent", response.ConsentInfo.Data.ConsentId);
            Assert.AreEqual(createdPackage.Id.Id, response.ConsentInfo.Data.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual("fr", response.ConsentInfo.Data.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("esignlive", response.ConsentInfo.Data.ConsentMetadata.Properties.AccountId);
            Assert.AreEqual("fr", response.ConsentInfo.Data.ConsentMetadata.Properties.Language);
            Assert.AreEqual("esignlive", response.ConsentInfo.Data.ConsentMetadata.Document.AccountId);
            Assert.AreEqual("fr", response.ConsentInfo.Data.ConsentMetadata.Document.Language);

        }
    }
}
