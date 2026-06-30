using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class SpecifyRecipientExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SpecifyRecipientExample example = new SpecifyRecipientExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual(DocumentPackageStatus.SENT, documentPackage.Status);

            // Role 1 — regular external signer
            Signer regularSigner = documentPackage.GetSigner(example.email1);
            Assert.IsNotNull(regularSigner);
            Assert.AreEqual(example.REGULAR_SIGNER_FIRST, regularSigner.FirstName);
            Assert.AreEqual(example.REGULAR_SIGNER_LAST, regularSigner.LastName);
            Assert.AreEqual(example.REGULAR_SIGNER_ID, regularSigner.Id);

            // Role 2 — PLACEHOLDER role type: role contains a signer but signer fields are empty;
            // after round-trip the SDK must identify it as the new placeholder type
            Signer placeholderSigner = documentPackage.GetPlaceholder(example.PLACEHOLDER_ID);
            Assert.IsNotNull(placeholderSigner);
            Assert.AreEqual(example.PLACEHOLDER_ID, placeholderSigner.Id);
            Assert.IsTrue(placeholderSigner.IsNewPlaceholderSigner());

            // Role 3 — specifier: regular signer whose specifier flag must come back as true
            Signer specifierSigner = documentPackage.GetSigner(example.email2);
            Assert.IsNotNull(specifierSigner);
            Assert.AreEqual(example.SPECIFIER_FIRST, specifierSigner.FirstName);
            Assert.AreEqual(example.SPECIFIER_LAST, specifierSigner.LastName);
            Assert.AreEqual(example.SPECIFIER_ID, specifierSigner.Id);
            Assert.IsTrue(specifierSigner.Specifier);
        }
    }
}
