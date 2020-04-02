using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SigningRedirectForSignerForSingleUseExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SigningRedirectForSignerForSingleUseExample example = new SigningRedirectForSignerForSingleUseExample();
            example.Run();

            Assert.IsNotEmpty(example.GeneratedLinkToSigningForSigner);
        }
    }
}

