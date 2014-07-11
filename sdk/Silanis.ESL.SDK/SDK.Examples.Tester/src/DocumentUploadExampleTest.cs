using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentUploadExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentUploadExample example = new DocumentUploadExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);

            // Verify if the document was uploaded correctly.

            Document document = documentPackage.Documents[example.UPLOADED_DOCUMENT_NAME];
            byte[] documentBinary = example.EslClient.DownloadDocument(example.PackageId, document.Id);
            Assert.Greater(documentBinary.Length, 0);
        }
    }
}

