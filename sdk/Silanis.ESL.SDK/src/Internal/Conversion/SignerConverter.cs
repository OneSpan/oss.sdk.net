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

            if (String.IsNullOrEmpty(sdkSigner.Id))
            {
                role.Id = role.Name = roleIdName;
            }
            else
            {
                role.Id = sdkSigner.Id;
                role.Name = sdkSigner.Id;
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

