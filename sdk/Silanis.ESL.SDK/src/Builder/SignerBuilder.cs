using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK.Builder
{
	public class SignerBuilder
	{
		private string signerEmail;
		private string firstName;
		private string lastName;
		private string title;
		private string company;

		private SignerBuilder(string signerEmail)
		{
			this.signerEmail = signerEmail;
		}

		public static SignerBuilder NewSignerWithEmail (string signerEmail)
		{
			Asserts.NotEmptyOrNull (signerEmail, "signerEmail");
			return new SignerBuilder (signerEmail);
		}

		public SignerBuilder WithFirstName (string firstName)
		{
			this.firstName = firstName;
			return this;
		}

		public SignerBuilder WithLastName (string lastName)
		{
			this.lastName = lastName;
			return this;
		}

		public SignerBuilder WithTitle (string title)
		{
			this.title = title;
			return this;
		}

		public SignerBuilder WithCompany (string company)
		{
			this.company = company;
			return this;
		}

		public Signer Build ()
		{
			Asserts.NotEmptyOrNull (firstName, "firstName");
			Asserts.NotEmptyOrNull (lastName, "lastName");

			Signer signer = new Signer (signerEmail, firstName, lastName);

			signer.Title = title;
			signer.Company = company;
			return signer;
		}
	}
}