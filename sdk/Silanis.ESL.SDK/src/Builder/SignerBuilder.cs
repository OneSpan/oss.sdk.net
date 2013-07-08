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
		private string id;
		private bool canChangeSigner;
		private bool locked;

		private SignerBuilder(string signerEmail)
		{
			this.signerEmail = signerEmail;
		}

		internal static SignerBuilder NewSignerFromAPISigner (Silanis.ESL.API.Role role)
		{
			Silanis.ESL.API.Signer eslSigner = role.Signers [0];

			SignerBuilder builder = SignerBuilder.NewSignerWithEmail( eslSigner.Email )
					.WithId ( eslSigner.Id )					
					.WithFirstName( eslSigner.FirstName )
					.WithLastName( eslSigner.LastName )
					.WithCompany( eslSigner.Company )
					.WithTitle( eslSigner.Title )
					.SigningOrder( role.Index );

			if ( role.Reassign ) {
				builder.CanChangeSigner ();
			}

			if ( role.EmailMessage != null ) {
				builder.WithEmailMessage( role.EmailMessage.Content );
			}

			if ( role.Locked ) {
				builder.Lock();
			}

			return builder;
		}

		public static SignerBuilder NewSignerWithEmail (string signerEmail)
		{
			Asserts.NotEmptyOrNull (signerEmail, "signerEmail");
			return new SignerBuilder (signerEmail);
		}

		public SignerBuilder Lock ()
		{
			locked = true;
			return this;
		}

		public SignerBuilder WithId (string id)
		{
			this.id = id;
			return this;
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

		public SignerBuilder CanChangeSigner ()
		{
			canChangeSigner = true;
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
			signer.CanChangeSigner = canChangeSigner;
			signer.Id = id;
			signer.Locked = locked;
			return signer;
		}
	}
}