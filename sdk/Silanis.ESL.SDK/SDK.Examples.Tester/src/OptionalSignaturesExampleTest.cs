using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class OptionalSignaturesExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            OptionalSignaturesExample example = new OptionalSignaturesExample();
            example.Run();

            Assert.AreEqual(2, example.signer1SignableSignatures.Count);
            Assert.AreEqual(example.email1, example.signer1SignableSignatures[0].SignerEmail);
            Assert.AreEqual(example.email1, example.signer1SignableSignatures[1].SignerEmail);

            Assert.AreEqual(2, example.signer2SignableSignatures.Count);
            Assert.AreEqual(example.email2, example.signer2SignableSignatures[0].SignerEmail);
            Assert.AreEqual(example.email2, example.signer2SignableSignatures[1].SignerEmail);

            if (example.signer2SignableSignatures[0].Optional)
            {
                Assert.IsFalse(example.signer2SignableSignatures[1].Optional);
            }
            else
            {
                Assert.IsTrue(example.signer2SignableSignatures[1].Optional);
            }
        }
    }
}

