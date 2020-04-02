using System;

namespace OneSpanSign.Sdk
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
