using System;

namespace OneSpanSign.Sdk
{
    public class PlaceholderSigner
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int SigningOrder
        {
            get;
            set;
        }

        public PlaceholderSigner( string id )
        {
            Id = id;
            Name = id;
        }

        public PlaceholderSigner( string id, string name )
        {
            Id = id;
            Name = name;
        }

        public PlaceholderSigner( string id, string name, int? signingOrder )
        {
            Id = id;
            Name = name;
            if (signingOrder != null)
            {
                SigningOrder = signingOrder.Value;
            }
        }
    }
}
