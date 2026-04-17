using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class ExtractionResult
    {
        [JsonProperty("documentUuid")]
        public string DocumentUuid { get; set; }

        [JsonProperty("extractedFields")]
        public IDictionary<string, string> ExtractedFields { get; set; }

        [JsonProperty("providerName")]
        public string ProviderName { get; set; }
    }
}
