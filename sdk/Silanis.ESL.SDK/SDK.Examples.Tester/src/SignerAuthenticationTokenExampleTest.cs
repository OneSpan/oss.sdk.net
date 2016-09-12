using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerAuthenticationTokenExampleTest
    {

        [Test()]
		public void VerifyResult()
        {
			SignerAuthenticationTokenExample example = new SignerAuthenticationTokenExample();
			example.Run();

            Assert.IsNotNull(example.SignerSessionIdForMultiUse);
			Assert.IsNotNull(example.SignerSessionIdForSingleUse);
        }
    }
}

