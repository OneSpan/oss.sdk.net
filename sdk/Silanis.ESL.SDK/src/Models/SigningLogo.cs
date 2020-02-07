using System;
using Newtonsoft.Json;

namespace Silanis.ESL.API
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