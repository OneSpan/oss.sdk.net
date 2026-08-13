using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class CarbonCopyRecipientExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CarbonCopyRecipientExample example = new CarbonCopyRecipientExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual(DocumentPackageStatus.SENT, documentPackage.Status);

            // Role 1 — regular external signer
            Signer signer = documentPackage.GetSigner(example.email1);
            Assert.IsNotNull(signer);
            Assert.AreEqual(example.SIGNER_FIRST, signer.FirstName);
            Assert.AreEqual(example.SIGNER_LAST, signer.LastName);
            Assert.AreEqual(example.SIGNER_ID, signer.Id);
            Assert.IsFalse(signer.CarbonCopyRecipient);

            // Role 2 — carbon copy recipient: the CARBON_COPY_RECIPIENT role type must survive the
            // round-trip so that a subsequent update does not downgrade it to a regular signer
            Signer carbonCopyRecipient = documentPackage.GetSigner(example.email2);
            Assert.IsNotNull(carbonCopyRecipient);
            Assert.AreEqual(example.CARBON_COPY_FIRST, carbonCopyRecipient.FirstName);
            Assert.AreEqual(example.CARBON_COPY_LAST, carbonCopyRecipient.LastName);
            Assert.AreEqual(example.CARBON_COPY_ID, carbonCopyRecipient.Id);
            Assert.IsTrue(carbonCopyRecipient.CarbonCopyRecipient);
        }
    }
}
