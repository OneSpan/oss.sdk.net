using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerSMSAuthenticationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerSMSAuthenticationExample example = new SignerSMSAuthenticationExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual(documentPackage.Signers[1].AuthenticationMethod, AuthenticationMethod.SMS);
            Assert.AreEqual(documentPackage.Signers[1].ChallengeQuestion.Count, 0);
            Assert.AreEqual(documentPackage.Signers[1].Authentication.PhoneNumber, example.sms1);
            Assert.AreEqual(documentPackage.Signers[1].PhoneNumber, example.sms1);
        }
    }
}

