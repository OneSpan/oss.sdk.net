using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class ReferencedFieldConditions
    {
        [JsonProperty("referencedInCondition")]
        public List<FieldCondition> ReferencedInCondition 
        {
            get; set;
        }
        [JsonProperty("referencedInAction")]
        public List<FieldCondition> ReferencedInAction
        {
            get; set;
        }
    }
}
