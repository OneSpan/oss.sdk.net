using System;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class Verification
    {
        // Fields

        // Accessors

        [JsonProperty("typeKey")]
        internal string TypeKey
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

