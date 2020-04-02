using NUnit.Framework;
using System;
using OneSpanSign.Sdk.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetSigningUrlExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            GetSigningUrlExample example = new GetSigningUrlExample();
            example.Run();

            Assert.IsNotEmpty(example.signingUrlForSigner1);
            Assert.IsNotEmpty(example.signingUrlForSigner2);
        }
    }
}

