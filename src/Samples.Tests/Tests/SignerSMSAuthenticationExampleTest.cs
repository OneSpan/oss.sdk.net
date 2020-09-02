using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerSMSAuthenticationExampleTest
    {
        [Test()]
        [Ignore("Since this example sends SMS, it is disabled")]
        public void VerifyResult()
        {
            SignerSMSAuthenticationExample example = new SignerSMSAuthenticationExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual(documentPackage.GetSigner(example.email1).AuthenticationMethod, AuthenticationMethod.SMS);
            Assert.AreEqual(documentPackage.GetSigner(example.email1).ChallengeQuestion.Count, 0);
        }
    }
}

