using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Tests
{
	[TestFixture()]
	public class AuthenticationConverterTest
    {
		private Silanis.ESL.SDK.Authentication sdkAuth1 = null;
		private Silanis.ESL.API.Auth apiAuth1 = null;
		private Silanis.ESL.API.Auth apiAuth2 = null;
		private AuthenticationConverter converter = null;

		[Test()]
		public void ConvertNullSDKToAPI()
		{
			sdkAuth1 = null;
			converter = new AuthenticationConverter(sdkAuth1);
			Assert.IsNull(converter.ToAPIAuthentication());
		}

		[Test()]
		public void ConvertNullAPIToAPI()
		{
			apiAuth1 = null;
			converter = new AuthenticationConverter(apiAuth1);

			Assert.IsNull(converter.ToAPIAuthentication());
		}

		[Test()]
		public void ConvertAPIToAPI()
		{
			apiAuth1 = CreateTypicalAPIAuthentication();
			converter = new AuthenticationConverter(apiAuth1);
			apiAuth2 = converter.ToAPIAuthentication();

			Assert.IsNotNull(apiAuth2);
			Assert.AreEqual(apiAuth2, apiAuth1);
		}

		[Test()]
		public void ConvertSDKToAPI()
		{
			sdkAuth1 = CreateTypicalSDKAuthentication();
			apiAuth1 = new AuthenticationConverter(sdkAuth1).ToAPIAuthentication();

			Assert.IsNotNull(apiAuth1);
			Assert.AreEqual(apiAuth1.Scheme.ToString(), sdkAuth1.Method.ToString());
			Assert.AreEqual(apiAuth1.Challenges[0].Question, sdkAuth1.Challenges[0].Question);
			Assert.AreEqual(apiAuth1.Challenges[0].Answer, sdkAuth1.Challenges[0].Answer);
		}

		private Silanis.ESL.SDK.Authentication CreateTypicalSDKAuthentication()
		{
			IList<Challenge> sdkChallenges = new List<Challenge>();
			sdkChallenges.Add(new Challenge("What is the name of your dog?", "John"));
			Authentication result = new Authentication(sdkChallenges);

			return result;
		}

		private Silanis.ESL.API.Auth CreateTypicalAPIAuthentication()
		{
			Silanis.ESL.API.Auth result = new Silanis.ESL.API.Auth();
			Silanis.ESL.API.AuthChallenge authChallenge = new Silanis.ESL.API.AuthChallenge();
			authChallenge.Question = "What is the name of your dog?";
			authChallenge.Answer = "John";
			result.AddChallenge(authChallenge);

			return result;
		}
    }
}

