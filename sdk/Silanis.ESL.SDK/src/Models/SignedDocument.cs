using System;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class SignedDocument : Document
    {
        [JsonProperty("handdrawn")]
        public String Handdrawn
        {
            get; set;
        }
    }
}
