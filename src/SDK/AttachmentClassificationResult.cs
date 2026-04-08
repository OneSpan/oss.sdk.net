using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class AttachmentClassificationResult
    {
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("confidenceScore")]
        public double ConfidenceScore { get; set; }

        [JsonProperty("confidenceLevel")]
        public string ConfidenceLevel { get; set; }

        [JsonProperty("providerName")]
        public string ProviderName { get; set; }
    }
}
