using System;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    public class Translation
    {
        public Translation()
        {
        }

        [JsonProperty("name")]
        public string Name {
                get;
                set;
        }

        [JsonProperty("description")]
        public String Description
        {
            get; set;
        }

        [JsonProperty("language")]
        public String Language
        {
            get; set;
        }

    }
}

