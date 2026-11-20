using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class CallbackTestResponseView
    {
        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }
    }
}
