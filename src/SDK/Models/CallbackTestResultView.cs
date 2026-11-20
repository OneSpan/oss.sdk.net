using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class CallbackTestResultView
    {
        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("sentPayload")]
        public string SentPayload { get; set; }

        [JsonProperty("requestHeaders")]
        public Dictionary<string, string> RequestHeaders { get; set; }

        [JsonProperty("durationMs")]
        public long? DurationMs { get; set; }

        [JsonProperty("response")]
        public CallbackTestResponseView Response { get; set; }

        [JsonProperty("failureDetail")]
        public string FailureDetail { get; set; }
    }
}
