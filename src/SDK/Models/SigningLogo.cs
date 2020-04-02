using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{

    internal class SigningLogo
    {
        //Fields


        // Accessors

        [JsonProperty ("language")]
        public String Language
        {
            get; set;
        }

        [JsonProperty ("image")]
        public String Image
        {
            get; set;
        }
    }
}