using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    internal class SignerVerificationConverter
    {
        private SignerVerification sdkSignerVerification = null;
        private OneSpanSign.API.Verification apiSignerVerification = null;
    
        public SignerVerificationConverter( SignerVerification signerVerification )
        {
            this.sdkSignerVerification = signerVerification;
        }

        public SignerVerificationConverter (OneSpanSign.API.Verification verification)
		{
            this.apiSignerVerification = verification;
		}

        public OneSpanSign.API.Verification ToAPISignerVerification()
        {
            if (sdkSignerVerification == null)
            {
                return apiSignerVerification;
            }

            OneSpanSign.API.Verification result = new OneSpanSign.API.Verification();
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