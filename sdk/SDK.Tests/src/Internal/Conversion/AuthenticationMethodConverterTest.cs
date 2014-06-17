using NUnit.Framework;
using System;
using Silanis.ESL.API;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture()]
    public class AuthenticationMethodConverterTest
    {
		private Silanis.ESL.SDK.AuthenticationMethod sdkAuthScheme1;
		private Silanis.ESL.API.AuthScheme apiAuthScheme1;

		[Test]
		public void ConvertAPIToSDK()
		{
			apiAuthScheme1 = CreateTypicalAPIAuthScheme();
			sdkAuthScheme1 = new AuthenticationMethodConverter(apiAuthScheme1).ToSDKAuthMethod();

			Assert.AreEqual(sdkAuthScheme1.ToString(), apiAuthScheme1.ToString());
		}

		[Test]
		public void ConvertSDKToAPI()
		{
			sdkAuthScheme1 = CreateTypicalSDKAuthScheme();
			apiAuthScheme1 = new AuthenticationMethodConverter(sdkAuthScheme1).ToAPIAuthMethod();

			Assert.AreEqual(AuthScheme.NONE, apiAuthScheme1);
		}

		private Silanis.ESL.SDK.AuthenticationMethod CreateTypicalSDKAuthScheme()
		{
			return AuthenticationMethod.EMAIL;
		}

		private Silanis.ESL.API.AuthScheme CreateTypicalAPIAuthScheme()
		{
			return Silanis.ESL.API.AuthScheme.SMS;
		}
    }
}

