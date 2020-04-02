using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class ReferencedDocument
    {
        [JsonProperty("documentId")]
        public String DocumentId
        {
            get; set;
        }
        [JsonProperty("fields")]
        public List<ReferencedField> Fields
        {
            get; set;
        }
    }
}
