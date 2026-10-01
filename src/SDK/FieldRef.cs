using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class FieldRef
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
