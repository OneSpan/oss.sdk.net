using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class AttachmentVerificationResult
    {
        [JsonProperty("attachmentUuid")]
        public string AttachmentUuid { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("classificationResult")]
        public AttachmentClassificationResult ClassificationResult { get; set; }

        [JsonProperty("typeMatch")]
        public bool TypeMatch { get; set; }
    }
}
