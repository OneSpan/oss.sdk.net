using System;

namespace Silanis.ESL.SDK
{
    public class RoleId
    {
        private string id;

        public string Id
        {
            get
            {
                return id;
            }
        }

        public RoleId( string id )
        {
            this.id = id;
        }
    }
}

