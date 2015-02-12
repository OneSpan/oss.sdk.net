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

            Assert.AreEqual(documentPackage.Signers[example.Email1].AuthenticationMethod, AuthenticationMethod.EMAIL);
            Assert.AreEqual(documentPackage.Signers[example.Email1].ChallengeQuestion.Count, 0);
            Assert.IsNull(documentPackage.Signers[example.Email1].PhoneNumber);

            Assert.AreEqual(documentPackage.Signers[example.Email2].AuthenticationMethod, AuthenticationMethod.CHALLENGE);
            Assert.AreEqual(documentPackage.Signers[example.Email2].ChallengeQuestion[0].Question, AuthenticationMethodsExample.QUESTION1);
            Assert.AreEqual(documentPackage.Signers[example.Email2].ChallengeQuestion[1].Question, AuthenticationMethodsExample.QUESTION2);
            Assert.IsNull(documentPackage.Signers[example.Email2].PhoneNumber);
          
            Assert.AreEqual(documentPackage.Signers[example.Email3].AuthenticationMethod, AuthenticationMethod.SMS);
            Assert.AreEqual(documentPackage.Signers[example.Email3].ChallengeQuestion.Count, 0);
            Assert.AreEqual(documentPackage.Signers[example.Email3].PhoneNumber, example.Sms3);
        }
    }
}

