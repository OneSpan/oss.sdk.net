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

            Assert.AreEqual(example.VERIFICATION_TYPE, example.RetrievedSignerVerification.TypeId);
            Assert.AreEqual(example.VERIFICATION_PAYLOAD, example.RetrievedSignerVerification.Payload);
        }
    }
}
