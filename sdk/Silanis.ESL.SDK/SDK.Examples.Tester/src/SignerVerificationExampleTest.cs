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

            Assert.AreEqual(example.CREATE_VERIFICATION_TYPE_ID, example.retrievedSignerVerificationAfterCreate.TypeId);
            Assert.AreEqual(example.CREATE_VERIFICATION_PAYLOAD, example.retrievedSignerVerificationAfterCreate.Payload);

            Assert.AreEqual(example.UPDATE_VERIFICATION_TYPE_ID, example.retrievedSignerVerificationAfterUpdate.TypeId);
            Assert.AreEqual(example.UPDATE_VERIFICATION_PAYLOAD, example.retrievedSignerVerificationAfterUpdate.Payload);

            Assert.IsNull(example.retrievedSignerVerificationAfterDelete);
        }
    }
}
