using System;

namespace Silanis.ESL.SDK
{
    internal class SignerConverter
    {
        private Signer sdkSigner;
    
        public SignerConverter( Signer signer )
        {
            this.sdkSigner = signer;
        }

        public Silanis.ESL.API.Role ToAPIRole(string roleIdName)
        {
            Silanis.ESL.API.Role role = new Silanis.ESL.API.Role();

            if ( !sdkSigner.IsPlaceholderSigner() ) {
                role.AddSigner(sdkSigner.ToAPISigner());
            }
            role.Index = sdkSigner.SigningOrder;
            role.Reassign = sdkSigner.CanChangeSigner;

            if (String.IsNullOrEmpty(sdkSigner.RoleId))
            {
                role.Id = role.Name = roleIdName;
            }
            else
            {
                role.Id = sdkSigner.RoleId;
                role.Name = sdkSigner.RoleId;
            }

            if (!String.IsNullOrEmpty(sdkSigner.Message))
            {
                Silanis.ESL.API.BaseMessage message = new Silanis.ESL.API.BaseMessage();

                message.Content = sdkSigner.Message;
                role.EmailMessage = message;
            }
            
            return role;
        }
    }
}

