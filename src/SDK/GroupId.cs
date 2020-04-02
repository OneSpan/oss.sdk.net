using System;

namespace OneSpanSign.Sdk
{
    public class GroupId
    {
        private string id;

        public string Id
        {
            get
            {
                return id;
            }
        }

        public GroupId( string id )
        {
            this.id = id;
        }
    }
}

