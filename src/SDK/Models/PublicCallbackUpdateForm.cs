using Newtonsoft.Json;

namespace OneSpanSign.API
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class PublicCallbackUpdateForm
    {
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
    }
}
