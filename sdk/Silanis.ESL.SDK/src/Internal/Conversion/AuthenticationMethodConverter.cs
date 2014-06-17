using System;

namespace Silanis.ESL.SDK
{
	internal class AuthenticationMethodConverter
	{
		private Silanis.ESL.SDK.AuthenticationMethod sdkAuthMethod;
		private Silanis.ESL.API.AuthScheme apiAuthMethod;

		public AuthenticationMethodConverter(Silanis.ESL.API.AuthScheme apiAuthMethod)
		{
			this.apiAuthMethod = apiAuthMethod;
		}

		public AuthenticationMethodConverter(Silanis.ESL.SDK.AuthenticationMethod sdkAuthMethod)
		{
			this.sdkAuthMethod = sdkAuthMethod;
		}

		public Silanis.ESL.API.AuthScheme ToAPIAuthMethod()
		{
			switch (sdkAuthMethod)
			{
				case AuthenticationMethod.EMAIL:
					return Silanis.ESL.API.AuthScheme.NONE;
				case AuthenticationMethod.CHALLENGE:
					return Silanis.ESL.API.AuthScheme.CHALLENGE;
				case AuthenticationMethod.SMS:
					return Silanis.ESL.API.AuthScheme.SMS;
				default:
					throw new EslException("Unknown AuthenticationMethod", null);
			}
		}

		public Silanis.ESL.SDK.AuthenticationMethod ToSDKAuthMethod()
		{
			switch (apiAuthMethod)
			{
				case Silanis.ESL.API.AuthScheme.CHALLENGE:
					return Silanis.ESL.SDK.AuthenticationMethod.CHALLENGE;
				case Silanis.ESL.API.AuthScheme.SMS:
					return Silanis.ESL.SDK.AuthenticationMethod.SMS;
				default:
					return Silanis.ESL.SDK.AuthenticationMethod.EMAIL;
			}
		}
	}
}

