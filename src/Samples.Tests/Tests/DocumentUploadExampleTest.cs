using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentUploadExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentUploadExample example = new DocumentUploadExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            // Verify if the documents was uploaded correctly.
            Assert.AreEqual( 3, documentPackage.Documents.Count );

            Document document1 = documentPackage.GetDocument(example.DOCUMENT1_NAME);
            byte[] document1Binary = example.OssClient.DownloadDocument(example.PackageId, document1.Id);
            Assert.Greater(document1Binary.Length, 32000);
            Assert.Less(document1Binary.Length, 33000);

            Document document2 = documentPackage.GetDocument(example.DOCUMENT2_NAME);
            byte[] documen2Binary = example.OssClient.DownloadDocument(example.PackageId, document2.Id);
            Assert.Greater(documen2Binary.Length, 50000);
            Assert.Less(documen2Binary.Length, 52000);
        }
    }
}

