using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class SignedDocument : Document
    {
        [JsonProperty("handdrawn")]
        public String Handdrawn
        {
            get; set;
        }
    }
}
