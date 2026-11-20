using Newtonsoft.Json;

namespace OneSpanSign.API
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class NamedValue
    {
        public NamedValue()
        {
        }

        public NamedValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
