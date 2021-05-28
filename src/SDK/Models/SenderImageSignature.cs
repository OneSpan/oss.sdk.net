using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
    internal class SenderImageSignature
    {
        [JsonProperty("fileName")]
        public string FileName
        {
            get; set;
        }

        [JsonProperty("mediaType")]
        public string MediaType
        {
            get; set;
        }

        [JsonProperty("content")]
        public string Content
        {
            get; set;
        }
    }
}
