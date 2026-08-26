using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class AttachmentVerificationCheckResult
    {
        [JsonProperty("ruleName")]
        public string RuleName { get; set; }

        [JsonProperty("fields")]
        public IList<string> Fields { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AttachmentVerificationStatus Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
