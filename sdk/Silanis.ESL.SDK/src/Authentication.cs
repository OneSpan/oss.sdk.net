using System;
using System.Collections.Generic;

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
			Silanis.ESL.API.Auth auth = new Silanis.ESL.API.Auth ();

			auth.Scheme = Scheme ();

			foreach (Challenge challenge in challenges)
			{
				Silanis.ESL.API.AuthChallenge authChallenge = new Silanis.ESL.API.AuthChallenge();

				authChallenge.Question = challenge.Question;
				authChallenge.Answer = challenge.Answer;
				authChallenge.MaskInput = challenge.MaskOption == Challenge.MaskOptions.MaskInput;
				auth.AddChallenge (authChallenge);
			}

			if (!String.IsNullOrEmpty (PhoneNumber))
			{
				Silanis.ESL.API.AuthChallenge challenge = new Silanis.ESL.API.AuthChallenge ();

				challenge.Question = PhoneNumber;
				auth.AddChallenge (challenge);
			}

			return auth;
		}

		private Silanis.ESL.API.AuthScheme Scheme ()
		{
			switch (method)
			{
			case AuthenticationMethod.EMAIL:
				return Silanis.ESL.API.AuthScheme.NONE;
			case AuthenticationMethod.CHALLENGE:
				return Silanis.ESL.API.AuthScheme.CHALLENGE;
			case AuthenticationMethod.SMS:
				return Silanis.ESL.API.AuthScheme.SMS;
			default:
				throw new EslException ("Unknown AuthenticationMethod");
			}
		}
	}
}