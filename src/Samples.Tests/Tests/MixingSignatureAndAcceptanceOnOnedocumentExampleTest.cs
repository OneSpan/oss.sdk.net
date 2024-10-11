using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class MixingSignatureAndAcceptanceOnOnedocumentExampleTest
    {
        [Test()]
        public void VerifyResult()
        { 
            MixingSignatureAndAcceptanceOnOnedocumentExample example = new MixingSignatureAndAcceptanceOnOnedocumentExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            List<Signature> signatures = documentPackage.GetDocument("First Document").Signatures;

            Assert.AreEqual(2, signatures.Count);
            foreach (Signature signature in signatures)
            {
                if (signature.SignerEmail == example.email1) { Assert.AreEqual(SignatureStyle.FULL_NAME, signature.Style); }
                if (signature.SignerEmail == example.email2) { Assert.AreEqual(SignatureStyle.ACCEPTANCE, signature.Style); }
            }
        }
    }
}

