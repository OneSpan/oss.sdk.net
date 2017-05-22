using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerVerificationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerVerificationExample example = new SignerVerificationExample();
            example.Run();

            Assert.AreEqual(example.VERIFICATION_TYPE, example.RetrievedSignerVerification1.TypeId);
            Assert.AreEqual(example.VERIFICATION_PAYLOAD, example.RetrievedSignerVerification1.Payload);

            Assert.AreEqual(example.VERIFICATION_TYPE, example.RetrievedSignerVerification2.TypeId);
            Assert.AreEqual(example.VERIFICATION_PAYLOAD_UPDATED, example.RetrievedSignerVerification2.Payload);

            Assert.IsNull(example.RetrievedSignerVerification3);
        }
    }
}
