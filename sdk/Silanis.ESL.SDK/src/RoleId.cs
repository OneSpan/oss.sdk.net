using System;

namespace Silanis.ESL.SDK
{
    public class Placeholder
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

        public Placeholder( string id )
        {
            Id = id;
            Name = id;
        }

        public Placeholder( string id, string name )
        {
            Id = id;
            Name = name;
        }

        public Placeholder( string id, string name, int? signingOrder )
        {
            Id = id;
            Name = name;
            if(signingOrder != null) SigningOrder = signingOrder.Value;
        }
    }
}

