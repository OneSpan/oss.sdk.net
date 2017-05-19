using System;

namespace Silanis.ESL.SDK
{
    public class SignerVerificationBuilder
    {
        private string typeId;
        private string payload;

        private SignerVerificationBuilder(string typeId) {
            this.typeId = typeId;
        }

        public static SignerVerificationBuilder SignerVerificationFor(string typeId) {
            return new SignerVerificationBuilder(typeId);
        }

        public SignerVerificationBuilder WithPayload( string payload ) {
            this.payload = payload;
            return this;
        }

        public SignerVerification Build() {
            SignerVerification result = new SignerVerification(typeId);
            result.Payload = payload;

            return result;
        }
    }
}