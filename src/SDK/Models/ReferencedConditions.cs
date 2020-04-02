using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class ReferencedConditions
    {
        [JsonProperty("packageId")]
        public String PackageId
        {
            get; set;
        }
        [JsonProperty("documents")]
        public List<ReferencedDocument> Documents
        {
            get; set;
        }
    }
}
