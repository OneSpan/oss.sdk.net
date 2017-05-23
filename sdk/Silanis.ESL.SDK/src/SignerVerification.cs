using System;

namespace Silanis.ESL.SDK
{
    public class SignerVerification
    {
        public SignerVerification (string typeId)
        {
            TypeId = typeId;
        }

        public string TypeId
        {
            get; private set;
        }

        public string Payload
        {
            get; set;
        }
    }
}
