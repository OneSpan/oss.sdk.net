using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{

    internal class SignatureLogo
    {
        //Fields


        // Accessors    

        [JsonProperty ("image")]
        public String Image
        {
            get; set;
        }
    }
}