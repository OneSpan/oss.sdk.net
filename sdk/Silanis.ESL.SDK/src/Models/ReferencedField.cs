using System;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class ReferencedField
    {
        [JsonProperty("fieldId")]
        public String FieldId
        {
            get; set;
        }
        [JsonProperty("conditions")]
        public ReferencedFieldConditions Conditions
        {
            get; set;
        }
    }
}
