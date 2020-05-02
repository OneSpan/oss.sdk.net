using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class Provider
    {
        public IDictionary<string, object> Data { get; set; }

        public String Id { get; set; }

        public String Name { get; set; }

        public String Provides { get; set; }
    }
}