using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class PublicCallbackConfigurationView
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("authType")]
        public string AuthType { get; set; }

        [JsonProperty("events")]
        public List<string> Events { get; set; }

        [JsonProperty("customHeaderNames")]
        public List<string> CustomHeaderNames { get; set; }

        [JsonProperty("oauth")]
        public OauthSettingView Oauth { get; set; }

        [JsonProperty("mtlsEnabled")]
        public bool? MtlsEnabled { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
    }
}
