using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentPackageAttributesExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentPackageAttributesExample example = new DocumentPackageAttributesExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;
            DocumentPackageAttributes attributes = documentPackage.Attributes;
            IDictionary<string, object> attributeMap = attributes.Contents;

            Assert.IsTrue(attributeMap.ContainsKey(example.ATTRIBUTE_KEY_1));
            Assert.IsTrue(attributeMap.ContainsKey(example.ATTRIBUTE_KEY_2));
            Assert.IsTrue(attributeMap.ContainsKey(example.ATTRIBUTE_KEY_3));

            Assert.AreEqual(attributeMap[example.ATTRIBUTE_KEY_1], example.ATTRIBUTE_1);
            Assert.AreEqual(attributeMap[example.ATTRIBUTE_KEY_2], example.ATTRIBUTE_2);
            Assert.AreEqual(attributeMap[example.ATTRIBUTE_KEY_3], example.ATTRIBUTE_3);
        }
    }
}

