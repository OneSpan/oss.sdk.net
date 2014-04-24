using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SigningRedirectForSignerExampleTest
    {
        /** 
        Will not be supported until later release.
        **/

        [Test()]
        public void VerifyResult()
        {
            SigningRedirectForSignerExample example = new SigningRedirectForSignerExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.GeneratedLinkToSigningForSigner);
        }
    }
}

