using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SenderAuthenticationTokenExampleTest
    {
        [Test()]
		public void VerifyResult()
        {
			SenderAuthenticationTokenExample example = new SenderAuthenticationTokenExample(Props.GetInstance());
			example.Run();

			Assert.IsNotNull(example.SenderAuthenticationToken);
        }
    }
}

