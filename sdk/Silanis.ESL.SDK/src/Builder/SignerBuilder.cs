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
		private AuthenticationBuilder authenticationBuilder = new AuthenticationBuilder();
		private bool deliverSignedDocumentsByEmail;
		private int signingOrder;
		private string message;

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

		public SignerBuilder ChallengedWithQuestions (ChallengeBuilder challengeBuilder)
		{
			this.authenticationBuilder = challengeBuilder;
			return this;
		}

		public SignerBuilder WithSMSSentTo (string phoneNumber)
		{
			this.authenticationBuilder = new SMSAuthenticationBuilder (phoneNumber);
			return this;
		}

		public SignerBuilder DeliverSignedDocumentsByEmail ()
		{
			deliverSignedDocumentsByEmail = true;
			return this;
		}

		public SignerBuilder SigningOrder (int signingOrder)
		{
			this.signingOrder = signingOrder;
			return this;
		}

		public SignerBuilder WithEmailMessage (string message)
		{
			this.message = message;
			return this;
		}

		public Signer Build ()
		{
			Asserts.NotEmptyOrNull (firstName, "firstName");
			Asserts.NotEmptyOrNull (lastName, "lastName");

			Authentication authentication = authenticationBuilder.Build ();
			Signer signer = new Signer (signerEmail, firstName, lastName, authentication);

			signer.Title = title;
			signer.Company = company;
			signer.DeliverSignedDocumentsByEmail = deliverSignedDocumentsByEmail;
			signer.SigningOrder = signingOrder;
			signer.Message = message;
			return signer;
		}
	}
}