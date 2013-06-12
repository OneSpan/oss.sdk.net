using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class Signer
	{
		private readonly Authentication authentication;

		public Signer (string signerEmail, string firstName, string lastName, Authentication authentication)
		{
			Email = signerEmail;
			FirstName = firstName;
			LastName = lastName;
			this.authentication = authentication;
		}

		public string Email {
			get;
			private set;
		}

		public string FirstName {
			get;
			private set;
		}

		public string LastName {
			get;
			private set;
		}

		public string Title {
			get;
			set;
		}

		public string Company {
			get;
			set;
		}

		public AuthenticationMethod AuthenticationMethod {
			get
			{
				return authentication.Method;
			}
		}

		public IList<Challenge> ChallengeQuestion {
			get
			{
				return authentication.Challenges;
			}
		}

		public string PhoneNumber {
			get
			{
				return authentication.PhoneNumber;
			}
		}

		public bool DeliverSignedDocumentsByEmail {
			get;
			set;
		}

		public int SigningOrder {
			get;
			set;
		}

		public string Message {
			get;
			set;
		}

		internal Silanis.ESL.API.Signer ToAPISigner ()
		{
			Silanis.ESL.API.Signer signer = new Silanis.ESL.API.Signer ();

			signer.Email = Email;
			signer.FirstName = FirstName;
			signer.LastName = LastName;
			signer.Title = Title;
			signer.Company = Company;
			signer.Auth = authentication.ToAPIAuthentication ();

			return signer;
		}
	}
}