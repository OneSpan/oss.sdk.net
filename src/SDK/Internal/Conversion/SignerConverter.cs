using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    internal class SignerConverter
    {
		private Signer sdkSigner = null;
		private OneSpanSign.API.Signer apiSigner = null;
		private OneSpanSign.API.Role apiRole = null;
    
        public SignerConverter( Signer signer )
        {
            this.sdkSigner = signer;
        }

        public SignerConverter( Placeholder placeholder )
        {
            this.sdkSigner = new Signer(placeholder.Id);
        }

		public SignerConverter (OneSpanSign.API.Role apiRole)
		{
			this.apiRole = apiRole;

			if (apiRole != null)
			{
				this.apiSigner = apiRole.Signers[0];
			}
		}

        public OneSpanSign.API.Role ToAPIRole(string roleIdName)
        {
            OneSpanSign.API.Role role = new OneSpanSign.API.Role();

            if ( !sdkSigner.IsPlaceholderSigner() ) {
				role.AddSigner(new SignerConverter(sdkSigner).ToAPISigner());
            }
            role.Index = sdkSigner.SigningOrder;
            role.Reassign = sdkSigner.CanChangeSigner;
            role.Locked = sdkSigner.Locked;

			foreach (AttachmentRequirement attachmentRequirement in sdkSigner.Attachments)
			{
				role.AddAttachmentRequirement(new AttachmentRequirementConverter(attachmentRequirement).ToAPIAttachmentRequirement());
			}

            if (!String.IsNullOrEmpty(sdkSigner.Id))
            {
                role.Id = sdkSigner.Id;
                role.Name = sdkSigner.Id;
            }
            else if (sdkSigner.IsGroupSigner())
            {
                role.Id = role.Name = sdkSigner.GroupId.Id;
            }
            else if (sdkSigner.IsAdHocGroupSigner()) // ????????
            {
                role.Id = sdkSigner.Id;
                role.Name = sdkSigner.Id;
                //todo: add ad hoc group logic here
            }
            else
            {
                role.Id = role.Name = roleIdName;
            }

            if (!String.IsNullOrEmpty(sdkSigner.Message))
            {
                OneSpanSign.API.BaseMessage message = new OneSpanSign.API.BaseMessage();

                message.Content = sdkSigner.Message;
                role.EmailMessage = message;
            }

            if (!String.IsNullOrEmpty(sdkSigner.LocalLanguage))
            {
                role.AddData("localLanguage", sdkSigner.LocalLanguage);
            }

            return role;
        }

        public OneSpanSign.API.Role ToAPIRole(string id, string name)
        {
            OneSpanSign.API.Role role = new OneSpanSign.API.Role();

            if ( !sdkSigner.IsPlaceholderSigner() ) {
                role.AddSigner(new SignerConverter(sdkSigner).ToAPISigner());
            }
            role.Index = sdkSigner.SigningOrder;
            role.Reassign = sdkSigner.CanChangeSigner;
            role.Locked = sdkSigner.Locked;

            foreach (AttachmentRequirement attachmentRequirement in sdkSigner.Attachments)
            {
                role.AddAttachmentRequirement(new AttachmentRequirementConverter(attachmentRequirement).ToAPIAttachmentRequirement());
            }

            if (!String.IsNullOrEmpty(sdkSigner.Id))
            {
                role.Id = sdkSigner.Id;
            }
            else if (sdkSigner.IsGroupSigner())
            {
                role.Id = sdkSigner.GroupId.Id;
            }
            else
            {
                role.Id = id;
            }

            if (!String.IsNullOrEmpty(sdkSigner.PlaceholderName))
            {
                role.Name = sdkSigner.PlaceholderName;
            }
            else
            {
                role.Name = name;
            }

            if (!String.IsNullOrEmpty(sdkSigner.Message))
            {
                OneSpanSign.API.BaseMessage message = new OneSpanSign.API.BaseMessage();

                message.Content = sdkSigner.Message;
                role.EmailMessage = message;
            }

            return role;
        }

		internal OneSpanSign.API.Signer ToAPISigner()
		{
			if (sdkSigner == null)
			{
				return apiSigner;
			}

			if (sdkSigner.IsPlaceholderSigner())
			{
				return null;
			}

			OneSpanSign.API.Signer signer = new OneSpanSign.API.Signer ();

			if (sdkSigner.IsGroupSigner())
            {
                signer.Group = new OneSpanSign.API.Group();
                signer.Group.Id = sdkSigner.GroupId.Id;
            } 
            else if (sdkSigner.IsAdHocGroupSigner())
            {
                signer.Group = new GroupConverter(sdkSigner.Group).ToAPIGroup();
                signer.FirstName = sdkSigner.FirstName;
                signer.Email = sdkSigner.Email;
                signer.SignerType = "AD_HOC_GROUP_SIGNER";
            }
            else
            {
                signer.Email = sdkSigner.Email;
                signer.FirstName = sdkSigner.FirstName;
                signer.LastName = sdkSigner.LastName;
                signer.Title = sdkSigner.Title;
                signer.Company = sdkSigner.Company;
                if (sdkSigner.DeliverSignedDocumentsByEmail)
                {
                    signer.Delivery = new OneSpanSign.API.Delivery();
                    signer.Delivery.Email = sdkSigner.DeliverSignedDocumentsByEmail;
                }
                signer.KnowledgeBasedAuthentication = new KnowledgeBasedAuthenticationConverter(sdkSigner.KnowledgeBasedAuthentication).ToAPIKnowledgeBasedAuthentication();
            }

            if (!String.IsNullOrEmpty(sdkSigner.Language)) 
            {
                signer.Language = sdkSigner.Language;
            }

			if (!String.IsNullOrEmpty(sdkSigner.Id))
			{
				signer.Id = sdkSigner.Id;
			}

			signer.Auth = new AuthenticationConverter(sdkSigner.Authentication).ToAPIAuthentication();
            
            if (sdkSigner.NotificationMethods != null)
            {
                signer.NotificationMethods = new NotificationMethodsConverter(sdkSigner.NotificationMethods).ToAPINotificationMethods();
                signer.Phone = sdkSigner.NotificationMethods.Phone;
            }

			return signer;
		}

        public OneSpanSign.Sdk.Signer ToSDKSigner()
        {
            if (apiRole == null)
            {
                return sdkSigner;
            }

            if (apiRole.Signers == null || apiRole.Signers.Count == 0)
            {
                return NewSignerPlaceholderFromAPIRole();
            }
            else
            {
                return NewRegularSignerFromAPIRole();
            }

        }

        private OneSpanSign.Sdk.Signer NewSignerPlaceholderFromAPIRole()
        {
            Asserts.NotEmptyOrNull(apiRole.Id, "role.id");

            SignerBuilder builder = SignerBuilder.NewSignerPlaceholder(new Placeholder(apiRole.Id))
                .SigningOrder(apiRole.Index.Value);

            if ( apiRole.Reassign.Value ) {
                builder.CanChangeSigner ();
            }

            if ( apiRole.EmailMessage != null ) {
                builder.WithEmailMessage( apiRole.EmailMessage.Content );
            }

            if ( apiRole.Index != null ) {
                builder.SigningOrder( apiRole.Index.Value );
            }

            Signer signer = builder.Build();

            if ( apiRole.Locked.Value ) {
                signer.Locked = true;
            }

            IDictionary<string, object> apiRoleData = apiRole.Data;
            if (apiRoleData != null && apiRoleData.ContainsKey ("localLanguage")) {
                object localLanguage = apiRoleData ["localLanguage"];
                if (localLanguage != null) {
                    signer.LocalLanguage = localLanguage.ToString ();
                }
            }

            return builder.Build();
        }

        private OneSpanSign.Sdk.Signer NewRegularSignerFromAPIRole()
        {
            OneSpanSign.API.Signer eslSigner = apiRole.Signers[0];

            SignerBuilder builder = SignerBuilder.NewSignerWithEmail(eslSigner.Email)
                .WithCustomId(eslSigner.Id)   
                .WithFirstName(eslSigner.FirstName)
                .WithLastName(eslSigner.LastName)
                .WithCompany(eslSigner.Company)
                .WithLanguage(eslSigner.Language)
                .WithTitle(eslSigner.Title)
                .ChallengedWithKnowledgeBasedAuthentication(new KnowledgeBasedAuthenticationConverter(eslSigner.KnowledgeBasedAuthentication).ToSDKKnowledgeBasedAuthentication());
                
            if (apiRole.Index.HasValue)
                builder.SigningOrder(apiRole.Index.Value);

            foreach (OneSpanSign.API.AttachmentRequirement attachmentRequirement in apiRole.AttachmentRequirements)
            {
                builder.WithAttachmentRequirement(new AttachmentRequirementConverter(attachmentRequirement).ToSDKAttachmentRequirement());
            }

            if (apiRole.Id != null) 
            {
                builder.WithCustomId(apiRole.Id);
            }

            if ( apiRole.Reassign.Value ) 
            {
                builder.CanChangeSigner ();
            }

            if ( apiRole.EmailMessage != null ) 
            {
                builder.WithEmailMessage( apiRole.EmailMessage.Content );
            }

            if (eslSigner.Delivery != null && eslSigner.Delivery.Email.Value) 
            {
                builder.DeliverSignedDocumentsByEmail();
            }

            builder.WithAuthentication(new AuthenticationConverter(eslSigner.Auth).ToSDKAuthentication());

            if (eslSigner.NotificationMethods != null)
            {
                builder.WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                    .WithPhoneNumber(eslSigner.Phone)
                    .WithPrimaryMethods(
                        NotificationMethodsConverter.ConvertNotificationMethodsToSDK(eslSigner.NotificationMethods.Primary))
                );
            }
                
            Signer signer = builder.Build();

            if ( apiSigner.SignerType != null ) 
            {
                signer.SignerType = apiSigner.SignerType;
            }

            if ( apiRole.Locked.Value ) 
            {
                signer.Locked = true;
            }

            IDictionary<string, object> apiRoleData = apiRole.Data;
            if (apiRoleData != null && apiRoleData.ContainsKey("localLanguage")) {
                object localLanguage = apiRoleData["localLanguage"];
                if (localLanguage != null) {
                    signer.LocalLanguage = localLanguage.ToString();
                }
            }

            return signer;
        }
    }
}