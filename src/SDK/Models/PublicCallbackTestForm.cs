using Newtonsoft.Json;

namespace OneSpanSign.API
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class PublicCallbackTestForm : PublicCallbackConfigurationForm
    {
        [JsonProperty("callbackId")]
        public long? CallbackId { get; set; }
    }
}
