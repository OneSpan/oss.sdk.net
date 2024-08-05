using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace OneSpanSign.API
{
    internal class SupportedLanguages
    {
        
        [JsonProperty("defaultSignerLanguage")]
        public string DefaultSignerLanguage
        {
            get; set;
        }
        
        [JsonProperty("signerLanguages")]
        public List<string> SignerLanguages
        {
            get; set;
        }
    }
}