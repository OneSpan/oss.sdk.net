using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class ConflictingField
    {
        [JsonProperty("field")]
        public FieldRef Field { get; set; }

        [JsonProperty("overlapType")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public OverlapType OverlapType { get; set; }
    }
}
