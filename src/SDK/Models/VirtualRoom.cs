using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
    internal class VirtualRoom
    {
        [JsonProperty("video")]
        public bool Video
        {
            get; set;
        }

        [JsonProperty("videoRecording")]
        public bool VideoRecording
        {
            get; set;
        }

        [JsonProperty("startDatetime")]
        public DateTime StartDatetime
        {
            get; set;
        }

        [JsonProperty("hostUid")]
        public string HostUid
        {
            get; set;
        }
    }
}
