using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    public class VerificationType
    {
        // Fields

        // Accessors

        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get; set;
        }


        [JsonProperty("method")]
        public string Method
        {
            get; set;
        }
    }
}

