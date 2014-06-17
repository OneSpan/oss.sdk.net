using System;

namespace Silanis.ESL.SDK
{
    internal class SignerConverter
    {
		private Signer sdkSigner = null;
		private Silanis.ESL.API.Signer apiSigner = null;
		private Silanis.ESL.API.Role apiRole = null;
    
        public SignerConverter( Signer signer )
        {
            this.sdkSigner = signer;
        }

		public SignerConverter (Silanis.ESL.API.Role apiRole)
		{
			this.apiRole = apiRole;

			if (apiRole != null)
			{
				this.apiSigner = apiRole.Signers[0];
			}
		}

        public Silanis.ESL.API.Role ToAPIRole(string roleIdName)
        {
            Silanis.ESL.API.Role role = new Silanis.ESL.API.Role();

            if ( !sdkSigner.IsPlaceholderSigner() ) {
				role.AddSigner(new SignerConverter(sdkSigner).ToAPISigner());
            }
            role.Index = sdkSigner.SigningOrder;
            role.Reassign = sdkSigner.CanChangeSigner;

			foreach (AttachmentRequirement attachmentRequirement in sdkSigner.Attachments.Values)
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
            else
            {
                role.Id = role.Name = roleIdName;
            }

            if (!String.IsNullOrEmpty(sdkSigner.Message))
            {
                Silanis.ESL.API.BaseMessage message = new Silanis.ESL.API.BaseMessage();

                message.Content = sdkSigner.Message;
                role.EmailMessage = message;
            }
            
            return role;
        }

		internal Silanis.ESL.API.Signer ToAPISigner()
		{
			if (sdkSigner == null)
			{
				return apiSigner;
			}

			if (sdkSigner.IsPlaceholderSigner())
			{
				return null;
			}

			Silanis.ESL.API.Signer signer = new Silanis.ESL.API.Signer ();

			if (!sdkSigner.IsGroupSigner())
			{
				signer.Email = sdkSigner.Email;
				signer.FirstName = sdkSigner.FirstName;
				signer.LastName = sdkSigner.LastName;
				signer.Title = sdkSigner.Title;
				signer.Company = sdkSigner.Company;
				if (sdkSigner.DeliverSignedDocumentsByEmail)
				{
					signer.Delivery = new Silanis.ESL.API.Delivery();
					signer.Delivery.Email = sdkSigner.DeliverSignedDocumentsByEmail;
				}
			}
			else
			{
				signer.Group = new Silanis.ESL.API.Group();
				signer.Group.Id = sdkSigner.GroupId.Id;
			}

			if (!String.IsNullOrEmpty(sdkSigner.Id))
			{
				signer.Id = sdkSigner.Id;
			}

			signer.Auth = new AuthenticationConverter(sdkSigner.Authentication).ToAPIAuthentication();

			return signer;
		}
    }
}

