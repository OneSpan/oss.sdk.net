using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class Verification
    {
        // Fields

        // Accessors

        [JsonProperty("typeId")]
        internal string TypeId
        {
            get; set;
        }


        [JsonProperty("payload")]
        internal string Payload
        {
            get; set;
        }
    }
}

