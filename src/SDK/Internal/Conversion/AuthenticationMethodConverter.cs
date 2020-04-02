using OneSpanSign.Sdk.Internal;
using System;
using System.Reflection;

namespace OneSpanSign.Sdk
{
	internal class AuthenticationMethodConverter
	{
        private static ILogger log = LoggerFactory.get(typeof(AuthenticationMethodConverter));

		private OneSpanSign.Sdk.AuthenticationMethod sdkAuthMethod;
		private string apiAuthMethod;

		public AuthenticationMethodConverter(string apiAuthMethod)
		{
			this.apiAuthMethod = apiAuthMethod;
		}

		public AuthenticationMethodConverter(OneSpanSign.Sdk.AuthenticationMethod sdkAuthMethod)
		{
			this.sdkAuthMethod = sdkAuthMethod;
		}

		public string ToAPIAuthMethod()
		{
            return sdkAuthMethod.getApiValue();
		}

		public OneSpanSign.Sdk.AuthenticationMethod ToSDKAuthMethod()
		{
            return OneSpanSign.Sdk.AuthenticationMethod.valueOf(apiAuthMethod);
		}
	}
}

