using NUnit.Framework;

namespace SDK.Examples
{
    
    [TestFixture ()]
    public class SupportingDocumentExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            SupportingDocumentExample example = new SupportingDocumentExample();
            example.Run ();
            
            Assert.AreEqual(3, example.SupportingDocumentAfterUpload.Count);
            Assert.AreEqual("The supporting document number one.pdf", example.DocumentMetadata.DocumentName);
            Assert.AreEqual("renamed.pdf", example.SupportingDocumentAfterRename.DocumentName);
            Assert.AreEqual(2, example.SupportingDocumentAfterDelete.Count);
            Assert.AreEqual(true, example.DownloadedAllSupportingDocumentsForPackage.Exists);
        }
    }
}