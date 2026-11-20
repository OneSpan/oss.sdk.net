using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class OauthSettingForm
    {
        [JsonProperty("grantType")]
        public string GrantType { get; set; }

        [JsonProperty("authMechanism")]
        public string AuthMechanism { get; set; }

        [JsonProperty("authorizationServerUrl")]
        public string AuthorizationServerUrl { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("ignoreSsl")]
        public bool? IgnoreSsl { get; set; }

        [JsonProperty("customParameters")]
        public List<NamedValue> CustomParameters { get; set; }
    }
}
