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

            Assert.IsNotNull(example.pdfDownloadedFile);
            Assert.IsNotNull(example.originalPdfDownloadedFile);
            Assert.AreNotEqual(example.pdfDownloadedFile, example.originalPdfDownloadedFile);

            Assert.IsNotNull(example.zippedDownloadedFile);
        }
    }
}

