using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class PublicCallbackConfigurationForm
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("authType")]
        public string AuthType { get; set; }

        [JsonProperty("events")]
        public List<string> Events { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("oauth")]
        public OauthSettingForm Oauth { get; set; }

        [JsonProperty("customHeaders")]
        public List<NamedValue> CustomHeaders { get; set; }

        [JsonProperty("mtlsEnabled")]
        public bool? MtlsEnabled { get; set; }
    }
}
