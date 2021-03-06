using Newtonsoft.Json;

namespace OneSpanSign.API
{
    public class IdvWorkflowConfiguration
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("tenant")] public string Tenant { get; set; }

        [JsonProperty("desc")] public string Desc { get; set; }

        [JsonProperty("skipWhenAccessingSignedDocuments")]
        public bool SkipWhenAccessingSignedDocuments { get; set; }
    }
}