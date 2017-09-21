using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentAttributesExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentAttributesExample example = new DocumentAttributesExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;
            IDictionary<string, object> documentAttributes = documentPackage.GetDocument(example.DOCUMENT_NAME).Data;

            Assert.IsTrue(documentAttributes.ContainsKey(example.ATTRIBUTE_KEY_1));
            Assert.IsTrue(documentAttributes.ContainsKey(example.ATTRIBUTE_KEY_2));
            Assert.IsTrue(documentAttributes.ContainsKey(example.ATTRIBUTE_KEY_3));

            Assert.AreEqual(example.ATTRIBUTE_1, documentAttributes[example.ATTRIBUTE_KEY_1]);
            Assert.AreEqual(example.ATTRIBUTE_2, documentAttributes[example.ATTRIBUTE_KEY_2]);
            Assert.AreEqual(example.ATTRIBUTE_3, documentAttributes[example.ATTRIBUTE_KEY_3]);
        }
    }
}

