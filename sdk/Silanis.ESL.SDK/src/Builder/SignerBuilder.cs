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
		private string roleId;
		private bool canChangeSigner;
		private bool locked;
        private GroupId groupId;

		private SignerBuilder(string signerEmail)
		{
			this.signerEmail = signerEmail;
            this.groupId = null;
            this.roleId = null;
		}

        private SignerBuilder(GroupId groupId)
        {
            this.signerEmail = null;
            this.groupId = groupId;
            this.roleId = null;
        }
        
        private SignerBuilder(Placeholder roleId)
        {
            this.signerEmail = null;
            this.groupId = null;
            this.roleId = roleId.Id;
        }

        private static SignerBuilder NewSignerPlaceholderFromAPIRole(Silanis.ESL.API.Role role)
        {
            Asserts.NotEmptyOrNull(role.Id, "role.id");
            
            SignerBuilder builder = SignerBuilder.NewSignerPlaceholder(new Placeholder(role.Id))
                .SigningOrder(role.Index);
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
        
        private static SignerBuilder NewRegularSignerFromAPIRole(Silanis.ESL.API.Role role)
        {
            Silanis.ESL.API.Signer eslSigner = role.Signers[0];

            SignerBuilder builder = SignerBuilder.NewSignerWithEmail( eslSigner.Email )
                    .WithCustomId ( eslSigner.Id )                  
                    .WithFirstName( eslSigner.FirstName )
                    .WithLastName( eslSigner.LastName )
                    .WithCompany( eslSigner.Company )
                    .WithTitle( eslSigner.Title )
                    .SigningOrder( role.Index );

            if (role.Id != null) {
                builder.WithRoleId(role.Id);
            }

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

		internal static SignerBuilder NewSignerFromAPIRole(Silanis.ESL.API.Role role)
        {
            if (role.Signers == null || role.Signers.Count == 0)
            {
                return NewSignerPlaceholderFromAPIRole(role);
            }
            else
            {
                return NewRegularSignerFromAPIRole(role);
            }
        
		}

        public static SignerBuilder NewSignerPlaceholder(Placeholder roleId)
        {
            return new SignerBuilder(roleId);
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

        public SignerBuilder WithRoleId(string roleId)
        {
            return WithRoleId(new Placeholder(roleId));
        }

        public SignerBuilder WithRoleId ( Placeholder roleId )
        {
			this.roleId = roleId.Id;
			return this;
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

        private Signer BuildGroupSigner()
        {
            Support.LogMethodEntry();
            
            Signer result = new Signer(groupId);
            result.SigningOrder = signingOrder;
            result.CanChangeSigner = canChangeSigner;
            result.Message = message;
            result.Id = id;
            result.Locked = locked;
            result.RoleId = roleId;
            
            Support.LogMethodExit(result);

            return result;
        }
        
        private Signer BuildPlaceholderSigner()
        {
            Support.LogMethodEntry();
    
            Asserts.NotEmptyOrNull( roleId, "roleId" );
                    
            Signer result = new Signer(roleId);
            result.SigningOrder = signingOrder;
            result.CanChangeSigner = canChangeSigner;
            result.Message = message;
            result.Id = id;
            result.Locked = locked;
            
            Support.LogMethodExit(result);
            
            return result;
        }
        
        private Signer BuildRegularSigner()
        {
            Support.LogMethodEntry();
            
            Asserts.NotEmptyOrNull (firstName, "firstName");
            Asserts.NotEmptyOrNull (lastName, "lastName");
                        
            Authentication authentication = authenticationBuilder.Build();
            Signer result = new Signer (signerEmail, firstName, lastName, authentication);
            result.Title = title;
            result.Company = company;
            result.DeliverSignedDocumentsByEmail = deliverSignedDocumentsByEmail;

            result.SigningOrder = signingOrder;
            result.CanChangeSigner = canChangeSigner;
            result.Message = message;
            result.Id = id;
            result.Locked = locked;
            result.RoleId = roleId;

            Support.LogMethodExit(result);
            
            return result;
        }

		public Signer Build()
        {
            Support.LogMethodEntry();
            Signer result = null;
            if (isGroupSigner())
            {
                result = BuildGroupSigner();
            }
            else if (isPlaceholder())
            {
                result = BuildPlaceholderSigner();
            }
            else
            {
                result = BuildRegularSigner();
            }
            Support.LogMethodExit(result);
            return result;
		}

        private bool isGroupSigner() {
            return groupId != null;
        }
        
        private bool isPlaceholder()
        {
            return groupId == null && signerEmail == null;
        }
	}
}