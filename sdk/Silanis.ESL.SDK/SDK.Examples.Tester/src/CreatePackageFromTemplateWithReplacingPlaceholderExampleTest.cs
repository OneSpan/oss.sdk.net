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
            CreatePackageFromTemplateWithReplacingPlaceholderExample example = new CreatePackageFromTemplateWithReplacingPlaceholderExample(Props.GetInstance());
            example.Run();

            Assert.AreEqual(3, example.RetrievedPackage.Signers.Count);
            Assert.AreEqual(0, example.RetrievedPackage.Placeholders.Count);

            Signer signer1 = example.RetrievedPackage.GetSigner(example.email1);
            Signer signer2 = example.RetrievedPackage.GetSigner(example.email2);

            Assert.AreEqual(example.TEMPLATE_SIGNER_FIRST, signer1.FirstName);
            Assert.AreEqual(example.TEMPLATE_SIGNER_LAST, signer1.LastName);
            Assert.AreEqual(example.PACKAGE_SIGNER_FIRST, signer2.FirstName);
            Assert.AreEqual(example.PACKAGE_SIGNER_LAST, signer2.LastName);
            Assert.AreEqual(example.PLACEHOLDER_ID, signer2.Id);


            List<Signature> signatures = example.RetrievedPackage.GetDocument(example.DOCUMENT_NAME).Signatures;

            Assert.AreEqual(2, signatures.Count);
            Assert.AreEqual(example.email1, signatures[0].SignerEmail);
            Assert.AreEqual(example.email2, signatures[1].SignerEmail);
        }
    }
}

