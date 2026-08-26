using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OneSpanSign.API;

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

        [JsonProperty("extractionStatus")]
        [JsonConverter(typeof(UnknownEnumValuesAsNullConverter))]
        public ExtractionStatus? ExtractionStatus { get; set; }

        [JsonProperty("reasonCode")]
        [JsonConverter(typeof(UnknownEnumValuesAsNullConverter))]
        public ExtractionReasonCode? ReasonCode { get; set; }

        [Obsolete("Coarse flag derived by the server from ExtractionStatus: true whenever the status is not COMPLETED. Prefer ExtractionStatus and ReasonCode, which distinguish \"not performed\" from a genuine failure.")]
        [JsonProperty("failed")]
        public bool? Failed { get; set; }

        [Obsolete("No longer returned by the server; use ReasonCode instead. Always null against a server that reports the structured outcome.")]
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("failureMessage")]
        public string FailureMessage { get; set; }
    }
}
