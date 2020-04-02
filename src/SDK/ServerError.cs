using System;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    public class ServerError
    {

        public Nullable<Int32> Code
        {
            get; set;
        }

        public String Message
        {
            get; set;
        }

        public String MessageKey
        {
            get; set;
        }

        public String Name
        {
            get; set;
        }

        public String Technical
        {
            get; set;
        }
    }
}

