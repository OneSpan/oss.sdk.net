using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class AttachmentClassificationResult
    {
        [JsonProperty("documentUuid")]
        public string DocumentUuid { get; set; }

        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("confidenceScore")]
        public double ConfidenceScore { get; set; }

        [JsonProperty("confidenceLevel")]
        public string ConfidenceLevel { get; set; }

        [JsonProperty("providerName")]
        public string ProviderName { get; set; }

        [JsonProperty("failed")]
        public bool? Failed { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("failureMessage")]
        public string FailureMessage { get; set; }
    }
}
