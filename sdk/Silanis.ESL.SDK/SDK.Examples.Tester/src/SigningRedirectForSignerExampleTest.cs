using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SigningRedirectForSignerExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SigningRedirectForSignerExample example = new SigningRedirectForSignerExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.GeneratedLinkToSigningForSigner);
        }
    }
}

