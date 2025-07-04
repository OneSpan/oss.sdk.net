using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class AuthenticationMethodsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            AuthenticationMethodsExample example = new AuthenticationMethodsExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual(documentPackage.GetSigner(example.email1).AuthenticationMethod, AuthenticationMethod.EMAIL);
            Assert.AreEqual(documentPackage.GetSigner(example.email1).ChallengeQuestion.Count, 0);
            Assert.IsNull(documentPackage.GetSigner(example.email2).AuthPhoneNumber);

            Assert.AreEqual(documentPackage.GetSigner(example.email2).AuthenticationMethod, AuthenticationMethod.CHALLENGE);
            Assert.AreEqual(documentPackage.GetSigner(example.email2).ChallengeQuestion[0].Question, AuthenticationMethodsExample.QUESTION1);
            Assert.AreEqual(documentPackage.GetSigner(example.email2).ChallengeQuestion[1].Question, AuthenticationMethodsExample.QUESTION2);
            Assert.IsNull(documentPackage.GetSigner(example.email2).AuthPhoneNumber);
          
            Assert.AreEqual(documentPackage.GetSigner(example.email3).AuthenticationMethod, AuthenticationMethod.EMAIL);
            Assert.AreEqual(documentPackage.GetSigner(example.email3).ChallengeQuestion.Count, 0);
        }
    }
}

