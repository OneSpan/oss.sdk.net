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

            Assert.AreEqual(example.CERTIFICATE, example.firstVerificationType);
            Assert.IsNull(example.deletedVerificationType);
        }
    }
}
