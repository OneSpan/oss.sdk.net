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

            Assert.IsNotNull(example.PdfDocumentBytes);
            Assert.IsNotNull(example.OriginalPdfDocumentBytes);
            Assert.AreNotEqual(example.PdfDocumentBytes, example.OriginalPdfDocumentBytes);
        }
    }
}

