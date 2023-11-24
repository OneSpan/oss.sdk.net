using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreatePackageFromBase64ContentExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CreatePackageFromBase64ContentExample example = new CreatePackageFromBase64ContentExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual( 2, documentPackage.Documents.Count );

            Document document = documentPackage.GetDocument(example.DOCUMENT_NAME);
            byte[] document1Binary = example.OssClient.DownloadDocument(example.PackageId, document.Id);

            Assert.Greater(document1Binary.Length, 1);
        }
    }
}

