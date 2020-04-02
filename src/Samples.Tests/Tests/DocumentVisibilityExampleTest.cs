using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentVisibilityExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentVisibilityExample example = new DocumentVisibilityExample(Props.GetInstance());
            example.Run();

            Assert.AreEqual(3, example.retrievedVisibility.Configurations.Count);

            CollectionAssert.AreEquivalent(new List<string>{example.SIGNER1_ID, example.SIGNER3_ID}, example.retrievedVisibility.GetConfiguration(example.DOC1_ID).SignerIds);
            CollectionAssert.AreEquivalent(new List<string>{example.SIGNER2_ID, example.SIGNER3_ID}, example.retrievedVisibility.GetConfiguration(example.DOC2_ID).SignerIds);
            CollectionAssert.AreEquivalent(new List<string>{example.SIGNER2_ID, example.SIGNER3_ID}, example.retrievedVisibility.GetConfiguration(example.DOC3_ID).SignerIds);

            Assert.AreEqual(1, example.documentsForSigner1.Count);
            Assert.AreEqual(example.DOC1_NAME, example.documentsForSigner1[0].Name);

            Assert.AreEqual(2, example.documentsForSigner2.Count);
            Assert.AreEqual(example.DOC2_NAME, example.documentsForSigner2[0].Name);
            Assert.AreEqual(example.DOC3_NAME, example.documentsForSigner2[1].Name);

            Assert.AreEqual(3, example.documentsForSigner3.Count);
            Assert.AreEqual(example.DOC1_NAME, example.documentsForSigner3[0].Name);
            Assert.AreEqual(example.DOC2_NAME, example.documentsForSigner3[1].Name);
            Assert.AreEqual(example.DOC3_NAME, example.documentsForSigner3[2].Name);

            Assert.AreEqual(2, example.signersForDocument1.Count);
            Assert.AreEqual(example.SIGNER1_ID, example.signersForDocument1[0].Id);
            Assert.AreEqual(example.SIGNER3_ID, example.signersForDocument1[1].Id);

            Assert.AreEqual(2, example.signersForDocument2.Count);
            Assert.AreEqual(example.SIGNER2_ID, example.signersForDocument2[0].Id);
            Assert.AreEqual(example.SIGNER3_ID, example.signersForDocument2[1].Id);

            Assert.AreEqual(2, example.signersForDocument3.Count);
            Assert.AreEqual(example.SIGNER2_ID, example.signersForDocument3[0].Id);
            Assert.AreEqual(example.SIGNER3_ID, example.signersForDocument3[1].Id);
        }
    }
}

