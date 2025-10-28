using NUnit.Framework;

namespace SDK.Examples
{
    
    [TestFixture ()]
    public class SupportingDocumentExampleExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            SupportingDocumentExample example = new SupportingDocumentExample();
            example.Run ();
            
            Assert.AreEqual(3, example.supportingDocumentAfterUpload.Count);
            Assert.AreEqual("The supporting document number one.pdf", example.documentMetadata.DocumentName);
            Assert.AreEqual("renamed.pdf", example.supportingDocumentAfterRename.DocumentName);
            Assert.AreEqual(2, example.supportingDocumentAfterDelete.Count);
            Assert.AreEqual(true, example.downloadedAllSupportingDocumentsForPackage.Exists);
        }
    }
}