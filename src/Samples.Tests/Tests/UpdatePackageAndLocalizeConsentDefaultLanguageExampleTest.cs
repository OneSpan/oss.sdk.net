using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class UpdatePackageAndLocalizeConsentDefaultLanguageExampleTest
    {
        [Test]
        public void verify() {
            // Asserts that are commented out are so because updating them is not currently supported by the oss server.
        
            UpdatePackageAndLocalizeConsentDefaultLanguageExample example = new UpdatePackageAndLocalizeConsentDefaultLanguageExample();
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
            Assert.AreEqual(createdPackage.Id.Id, response.PackageUid);
            Assert.NotNull(response.ConsentInfo);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SKIPPED, response.ConsentInfo.Status);
            Assert.AreEqual("Consent localization not required because language did not change.", response.ConsentInfo.Message);
            Assert.Null(response.ConsentInfo.Data);

        }
    }
}

