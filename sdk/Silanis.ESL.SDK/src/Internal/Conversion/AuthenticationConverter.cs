using System; 

namespace Silanis.ESL.SDK
{
	internal class AuthenticationConverter
	{
		private Silanis.ESL.API.Auth apiAuth = null;
		private Silanis.ESL.SDK.Authentication sdkAuth = null;

		public AuthenticationConverter(Silanis.ESL.API.Auth apiAuth)
		{
			this.apiAuth = apiAuth;
		}

		public AuthenticationConverter(Silanis.ESL.SDK.Authentication sdkAuth)
		{
			this.sdkAuth = sdkAuth;
		}

		internal Silanis.ESL.API.Auth ToAPIAuthentication()
		{
			if (sdkAuth == null)
			{
				return apiAuth;
			}

			Silanis.ESL.API.Auth auth = new Silanis.ESL.API.Auth();

			auth.Scheme = new AuthenticationMethodConverter(sdkAuth.Method).ToAPIAuthMethod();

			foreach (Challenge challenge in sdkAuth.Challenges)
			{
				Silanis.ESL.API.AuthChallenge authChallenge = new Silanis.ESL.API.AuthChallenge();

				authChallenge.Question = challenge.Question;
				authChallenge.Answer = challenge.Answer;
				authChallenge.MaskInput = challenge.MaskOption == Challenge.MaskOptions.MaskInput;
				auth.AddChallenge(authChallenge);
			}

			if (!String.IsNullOrEmpty(sdkAuth.PhoneNumber))
			{
				Silanis.ESL.API.AuthChallenge challenge = new Silanis.ESL.API.AuthChallenge();

				challenge.Question = sdkAuth.PhoneNumber;
				auth.AddChallenge(challenge);
			}

			return auth;
		}
	}
}

