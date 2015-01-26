using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetSigningUrlExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            GetSigningUrlExample example = new GetSigningUrlExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.signingUrlForSigner1);
            Assert.IsNotEmpty(example.signingUrlForSigner1);
            Assert.IsNotNull(example.signingUrlForSigner2);
            Assert.IsNotEmpty(example.signingUrlForSigner2);
        }
    }
}

