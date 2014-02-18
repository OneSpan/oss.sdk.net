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
        private GroupId groupId;

		private SignerBuilder(string signerEmail)
		{
			this.signerEmail = signerEmail;
            this.groupId = null;
		}

        private SignerBuilder(GroupId groupId)
        {
            this.signerEmail = null;
            this.groupId = groupId;
        }

		internal static SignerBuilder NewSignerFromAPISigner (Silanis.ESL.API.Role role)
		{
			Silanis.ESL.API.Signer eslSigner = role.Signers [0];

			SignerBuilder builder = SignerBuilder.NewSignerWithEmail( eslSigner.Email )
					.WithCustomId ( eslSigner.Id )					
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

            if (eslSigner.Delivery != null && eslSigner.Delivery.Email) {
                builder.DeliverSignedDocumentsByEmail();
            }

			return builder;
		}

		public static SignerBuilder NewSignerWithEmail (string signerEmail)
		{
			Asserts.NotEmptyOrNull (signerEmail, "signerEmail");
			return new SignerBuilder (signerEmail);
		}

		public static SignerBuilder NewSignerFromGroup(GroupId groupId)
        {
			return new SignerBuilder(groupId);
        }

		public SignerBuilder Lock ()
		{
			locked = true;
			return this;
		}

		public SignerBuilder WithCustomId (string id)
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

        [Obsolete("Please use WithCustomId() instead")]
        public SignerBuilder WithRoleId ( string roleId )
        {
            return WithCustomId(roleId);
        }

		[Obsolete("Please use WithCustomId() instead")]
		public SignerBuilder WithId ( string id )
		{
			return WithCustomId(id);
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
			Support.LogMethodEntry();

            Signer signer;
            if (isGroupSigner())
            {
                signer = new Signer(groupId);
            }
            else
            {
                Asserts.NotEmptyOrNull (firstName, "firstName");
                Asserts.NotEmptyOrNull (lastName, "lastName");
                Authentication authentication = authenticationBuilder.Build();
                signer = new Signer (signerEmail, firstName, lastName, authentication);
                signer.Title = title;
                signer.Company = company;
                signer.DeliverSignedDocumentsByEmail = deliverSignedDocumentsByEmail;
            }


			signer.SigningOrder = signingOrder;
            signer.CanChangeSigner = canChangeSigner;
			signer.Message = message;
			signer.Id = id;
			signer.Locked = locked;

			Support.LogMethodExit(signer);
			return signer;
		}

        private bool isGroupSigner() {
            return groupId != null;
        }
	}
}