using System;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class FieldCondition
    {
        [JsonProperty ("id")]
        public String Id 
        {
            get; set;
        }
        [JsonProperty ("condition")]
        public String Condition 
        {
            get; set;
        }
        [JsonProperty ("action")]
        public String Action 
        {
            get; set;
        }
    }
}
