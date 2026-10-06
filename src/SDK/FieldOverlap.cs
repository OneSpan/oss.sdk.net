using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class FieldOverlap
    {
        [JsonProperty("field")]
        public OverlappingField Field { get; set; }

        [JsonProperty("conflictsWith")]
        public IList<ConflictingField> ConflictsWith { get; set; } = new List<ConflictingField>();
    }
}
