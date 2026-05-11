using System;
using Newtonsoft.Json;
namespace OneSpanSign.API.Models
{
    [Serializable]
    public class ConsentLocalizationRequest
    {
        public ConsentLocalizationRequest()
        {
        }

        public ConsentLocalizationRequest(string language)
        {
            Language = language ?? throw new ArgumentNullException(nameof(language));
        }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
