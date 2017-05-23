using System;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    internal class SignerVerificationConverter
    {
        private SignerVerification sdkSignerVerification = null;
        private Silanis.ESL.API.Verification apiSignerVerification = null;
    
        public SignerVerificationConverter( SignerVerification signerVerification )
        {
            this.sdkSignerVerification = signerVerification;
        }

        public SignerVerificationConverter (Silanis.ESL.API.Verification verification)
		{
            this.apiSignerVerification = verification;
		}

        public Silanis.ESL.API.Verification ToAPISignerVerification()
        {
            if (sdkSignerVerification == null)
            {
                return apiSignerVerification;
            }

            Silanis.ESL.API.Verification result = new Silanis.ESL.API.Verification();
            result.TypeId = sdkSignerVerification.TypeId;
            result.Payload = sdkSignerVerification.Payload;
            return result;
        }

        public SignerVerification ToSDKSignerVerification()
        {
            if (apiSignerVerification == null)
            {
                return sdkSignerVerification;
            }

            SignerVerification result = SignerVerificationBuilder.NewSignerVerification(apiSignerVerification.TypeId)
                .WithPayload(apiSignerVerification.Payload)
                .Build();

            return result;
        }
    }
}