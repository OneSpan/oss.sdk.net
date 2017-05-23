using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    public class SignerVerificationBuilder
    {
        private string typeId;
        private string payload;

        private SignerVerificationBuilder(string typeId) {
            this.typeId = typeId;
        }

        public static SignerVerificationBuilder NewSignerVerification(string typeId) {
            return new SignerVerificationBuilder(typeId);
        }

        public SignerVerificationBuilder WithPayload( string payload ) {
            this.payload = payload;
            return this;
        }

        public SignerVerification Build() {
            Asserts.NotEmptyOrNull( typeId, "No TypeId set for this signer verification!" );

            SignerVerification result = new SignerVerification(typeId);
            result.Payload = payload;

            return result;
        }
    }
}