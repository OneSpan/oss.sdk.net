using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentRetrievalExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentRetrievalExample example = new DocumentRetrievalExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.pdfDownloadedFile.Contents);
            Assert.IsNotNull(example.originalPdfDownloadedFile.Contents);
            Assert.AreNotEqual(example.pdfDownloadedFile.Contents, example.originalPdfDownloadedFile.Contents);

            Assert.IsNotNull(example.zippedDownloadedFile.Contents);
        }
    }
}

