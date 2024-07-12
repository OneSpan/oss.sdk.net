using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerQnASMSChallengeExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerQnASMSChallengeExample example = new SignerQnASMSChallengeExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            // Note that for security reasons, the backend doesn't return challenge answers, so we don't verify the answers here.
            foreach (Challenge challenge in documentPackage.GetSigner(example.email1).ChallengeQuestion)
            {
                Assert.IsTrue(String.Equals(challenge.Question, example.FIRST_QUESTION) 
                || String.Equals(challenge.Question, example.SECOND_QUESTION)
                || String.Equals(challenge.Question, example.PHONE_NUMBER));
            }
            Assert.AreEqual(documentPackage.GetSigner(example.email1).AuthenticationMethod, AuthenticationMethod.QASMS);

        }
    }
}


