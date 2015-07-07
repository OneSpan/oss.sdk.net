using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class AuthenticationMethodsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            AuthenticationMethodsExample example = new AuthenticationMethodsExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual(documentPackage.Signers[1].AuthenticationMethod, AuthenticationMethod.EMAIL);
            Assert.AreEqual(documentPackage.Signers[1].ChallengeQuestion.Count, 0);
            Assert.IsNull(documentPackage.Signers[2].PhoneNumber);

            Assert.AreEqual(documentPackage.Signers[2].AuthenticationMethod, AuthenticationMethod.CHALLENGE);
            Assert.AreEqual(documentPackage.Signers[2].ChallengeQuestion[0].Question, AuthenticationMethodsExample.QUESTION1);
            Assert.AreEqual(documentPackage.Signers[2].ChallengeQuestion[1].Question, AuthenticationMethodsExample.QUESTION2);
            Assert.IsNull(documentPackage.Signers[2].PhoneNumber);
          
            Assert.AreEqual(documentPackage.Signers[3].AuthenticationMethod, AuthenticationMethod.SMS);
            Assert.AreEqual(documentPackage.Signers[3].ChallengeQuestion.Count, 0);
            Assert.AreEqual(documentPackage.Signers[3].PhoneNumber, example.Sms3);
        }
    }
}

