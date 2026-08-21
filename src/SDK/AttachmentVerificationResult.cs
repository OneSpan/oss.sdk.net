using System;
using Newtonsoft.Json;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    public class AttachmentVerificationResult
    {
        [JsonProperty("attachmentUuid")]
        public string AttachmentUuid { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileId")]
        public string FileId { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("classificationResult")]
        public AttachmentClassificationResult ClassificationResult { get; set; }

        [JsonProperty("extractionResult")]
        public ExtractionResult ExtractionResult { get; set; }

        [JsonProperty("extractionStatus")]
        [JsonConverter(typeof(UnknownEnumValuesAsNullConverter))]
        public ExtractionStatus? ExtractionStatus { get; set; }

        [JsonProperty("reasonCode")]
        [JsonConverter(typeof(UnknownEnumValuesAsNullConverter))]
        public ExtractionReasonCode? ReasonCode { get; set; }

        [Obsolete("No longer returned by the server; use ExtractionStatus instead. Always false against a server that reports the structured outcome.")]
        [JsonProperty("extractionFailed")]
        public bool ExtractionFailed { get; set; }

        [Obsolete("No longer returned by the server; use ReasonCode instead. Always null against a server that reports the structured outcome.")]
        [JsonProperty("extractionErrorCode")]
        public string ExtractionErrorCode { get; set; }

        [JsonProperty("typeMatch")]
        public bool TypeMatch { get; set; }
    }
}
