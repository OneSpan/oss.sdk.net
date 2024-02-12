using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentUploadFromBase64ContentExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentUploadFromBase64ContentExample example = new DocumentUploadFromBase64ContentExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual( 3, documentPackage.Documents.Count );

            Document document1 = documentPackage.GetDocument(example.DOCUMENT1_NAME);
            Document document2 = documentPackage.GetDocument(example.DOCUMENT2_NAME);
            byte[] document1Binary = example.OssClient.DownloadDocument(example.PackageId, document1.Id);
            byte[] document2Binary = example.OssClient.DownloadDocument(example.PackageId, document2.Id);

            Assert.Greater(document1Binary.Length, 1);
            Assert.Greater(document2Binary.Length, 1);
        }
    }
}