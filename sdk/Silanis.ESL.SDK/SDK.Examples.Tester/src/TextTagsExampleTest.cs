using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class TextTagsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            TextTagsExample example = new TextTagsExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Document document1 = documentPackage.GetDocument(example.DOCUMENT1_NAME);
            Document document2 = documentPackage.GetDocument(example.DOCUMENT2_NAME);
            Document document3 = documentPackage.GetDocument(example.DOCUMENT3_NAME);

            Assert.AreEqual(2, document1.Signatures.Count);
            Assert.AreEqual(4, document2.Signatures.Count);
            Assert.AreEqual(6, document3.Signatures.Count);
        }
    }
}

