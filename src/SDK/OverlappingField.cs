using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class OverlappingField
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("signerId")]
        public string SignerId { get; set; }
    }
}
