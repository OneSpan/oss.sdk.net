using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class OauthSettingView
    {
        [JsonProperty("grantType")]
        public string GrantType { get; set; }

        [JsonProperty("authMechanism")]
        public string AuthMechanism { get; set; }

        [JsonProperty("authorizationServerUrl")]
        public string AuthorizationServerUrl { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("ignoreSsl")]
        public bool? IgnoreSsl { get; set; }

        [JsonProperty("customParameterNames")]
        public List<string> CustomParameterNames { get; set; }
    }
}
