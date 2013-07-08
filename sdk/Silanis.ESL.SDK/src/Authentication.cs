using System;
using System.Collections.Generic;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	public class Authentication
	{
		private readonly AuthenticationMethod method;
		private List<Challenge> challenges = new List<Challenge>();

		public Authentication(AuthenticationMethod method)
		{
			this.method = method;
		}

		public Authentication(IList<Challenge> challenges) : this(AuthenticationMethod.CHALLENGE)
		{
			this.challenges.AddRange (challenges);
		}

		public Authentication(string phoneNumber) : this(AuthenticationMethod.SMS)
		{
			PhoneNumber = phoneNumber;
		}

		public AuthenticationMethod Method
		{
			get
			{
				return method;
			}
		}

		public IList<Challenge> Challenges
		{
			get
			{
				return challenges.AsReadOnly ();
			}
		}

		public string PhoneNumber {
			get;
			private set;
		}

		internal Silanis.ESL.API.Auth ToAPIAuthentication ()
		{
			Auth auth = new Auth ();

			auth.Scheme = Scheme ();

			foreach (Challenge challenge in challenges)
			{
				AuthChallenge authChallenge = new AuthChallenge();

				authChallenge.Question = challenge.Question;
				authChallenge.Answer = challenge.Answer;
				auth.AddChallenge (authChallenge);
			}

			if (!String.IsNullOrEmpty (PhoneNumber))
			{
				AuthChallenge challenge = new AuthChallenge ();

				challenge.Question = PhoneNumber;
				auth.AddChallenge (challenge);
			}

			return auth;
		}

		private AuthScheme Scheme ()
		{
			switch (method)
			{
			case AuthenticationMethod.EMAIL:
				return AuthScheme.NONE;
			case AuthenticationMethod.CHALLENGE:
				return AuthScheme.CHALLENGE;
			case AuthenticationMethod.SMS:
				return AuthScheme.SMS;
			default:
				throw new EslException ("Unknown AuthenticationMethod");
			}
		}
	}
}