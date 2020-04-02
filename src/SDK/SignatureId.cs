using System;

namespace OneSpanSign.Sdk
{
    public class SignatureId
    {
        private string id;

        public string Id
        {
            get
            {
                return id;
            }
        }

        public SignatureId( string id )
        {
            this.id = id;
        }
    }
}

