using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreatePackageFromTemplateWithReplacingPlaceholderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CreatePackageFromTemplateWithReplacingPlaceholderExample example = new CreatePackageFromTemplateWithReplacingPlaceholderExample();
            example.Run();

            Assert.AreEqual(6, example.RetrievedPackage.Signers.Count);
            Assert.AreEqual(0, example.RetrievedPackage.Placeholders.Count);

            Signer signer1 = example.RetrievedPackage.GetSigner(example.email1);
            Signer signer2 = example.RetrievedPackage.GetSigner(example.email2);
            Signer signer3 = example.RetrievedPackage.GetSigner(example.email3);
            Signer signer4 = example.RetrievedPackage.GetSigner(example.email4);
            Signer signer5 = example.RetrievedPackage.GetSigner(example.email5);

            Assert.AreEqual(example.TEMPLATE_SIGNER_FIRST, signer1.FirstName);
            Assert.AreEqual(example.TEMPLATE_SIGNER_LAST, signer1.LastName);
            Assert.AreEqual(3, signer1.SigningOrder);
            Assert.AreEqual(example.PACKAGE_SIGNER_FIRST, signer2.FirstName);
            Assert.AreEqual(example.PACKAGE_SIGNER_LAST, signer2.LastName);
            Assert.AreEqual(example.PLACEHOLDER_ID1, signer2.Id);
            Assert.AreEqual(1, signer2.SigningOrder);
            Assert.AreEqual(example.PACKAGE_SIGNER_FIRST, signer3.FirstName);
            Assert.AreEqual(example.PACKAGE_SIGNER_LAST, signer3.LastName);
            Assert.AreEqual(example.PLACEHOLDER_ID2, signer3.Id);
            Assert.AreEqual(2, signer3.SigningOrder);
            Assert.AreEqual(example.PACKAGE_SIGNER_FIRST, signer4.FirstName);
            Assert.AreEqual(example.PACKAGE_SIGNER_LAST, signer4.LastName);
            Assert.AreEqual(4, signer4.SigningOrder);
            Assert.AreEqual(example.PACKAGE_SIGNER_FIRST, signer5.FirstName);
            Assert.AreEqual(example.PACKAGE_SIGNER_LAST, signer5.LastName);
            Assert.AreEqual(5, signer5.SigningOrder);

            List<Signature> signatures = example.RetrievedPackage.GetDocument(example.DOCUMENT_NAME).Signatures;

            Assert.AreEqual(3, signatures.Count);

            Signature sig1 = getSignatureForEmail(signatures, example.email1);
            Assert.IsNotNull(sig1);
            Signature sig2 = getSignatureForEmail(signatures, example.email2);
            Assert.IsNotNull(sig2);
        }

        private Signature getSignatureForEmail(List<Signature> signatures, string email) 
        {
            foreach (Signature signature in signatures) 
            {
                if (String.Equals(signature.SignerEmail, email)) 
                {
                    return signature;
                }
            }
            return null;
        }

    }
}