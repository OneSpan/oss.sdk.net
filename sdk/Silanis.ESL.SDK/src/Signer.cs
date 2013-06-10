using System;

namespace Silanis.ESL.SDK
{
	public class Signer
	{
		public Signer (string signerEmail, string firstName, string lastName)
		{
			Email = signerEmail;
			FirstName = firstName;
			LastName = lastName;
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

		internal Silanis.ESL.API.Signer ToAPISigner ()
		{
			Silanis.ESL.API.Signer signer = new Silanis.ESL.API.Signer ();

			signer.Email = Email;
			signer.FirstName = FirstName;
			signer.LastName = LastName;
			signer.Title = Title;
			signer.Company = Company;
			return signer;
		}
	}
}