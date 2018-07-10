using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture ()]
    public class SignerSSOExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            SignerSSOExample example = new SignerSSOExample ();
            example.Run ();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreEqual (documentPackage.GetSigner (example.email4).AuthenticationMethod, AuthenticationMethod.SSO);
            Assert.AreEqual (documentPackage.GetSigner (example.email4).ChallengeQuestion.Count, 0);
        }
    }
}