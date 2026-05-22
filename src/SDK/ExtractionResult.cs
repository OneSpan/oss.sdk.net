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

        [JsonProperty("verificationCheckResults")]
        public IList<AttachmentVerificationCheckResult> VerificationCheckResults { get; set; }

        [JsonProperty("failed")]
        public bool? Failed { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("failureMessage")]
        public string FailureMessage { get; set; }
    }
}
