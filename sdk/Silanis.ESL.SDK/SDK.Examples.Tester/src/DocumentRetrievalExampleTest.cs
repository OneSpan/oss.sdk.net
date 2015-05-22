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

            Assert.IsNotNull(example.pdfDownloadedBytes);
            Assert.IsNotNull(example.originalPdfDownloadedBytes);
            Assert.AreNotEqual(example.pdfDownloadedBytes, example.originalPdfDownloadedBytes);

            Assert.IsNotNull(example.zippedDownloadedBytes);
        }
    }
}

