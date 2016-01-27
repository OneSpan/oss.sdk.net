using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Silanis.ESL.API
{
    internal class SessionFields
    {
        
        // Fields

        // Accessors

        [JsonProperty("fields")]
        public IDictionary<string, string> Fields
        {
            get; set;
        }
    }
}