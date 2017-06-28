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

            Assert.AreEqual(document1.Signatures.Count, 3);
            Assert.AreEqual(document2.Signatures.Count, 4);
            Assert.AreEqual(document3.Signatures.Count, 7);
        }
    }
}

