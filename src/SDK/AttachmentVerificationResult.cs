using Newtonsoft.Json;

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

        [JsonProperty("extractionFailed")]
        public bool ExtractionFailed { get; set; }

        [JsonProperty("extractionErrorCode")]
        public string ExtractionErrorCode { get; set; }

        [JsonProperty("typeMatch")]
        public bool TypeMatch { get; set; }
    }
}
