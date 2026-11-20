using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// The OAuth settings of a <see cref="CallbackConfigurationRequest"/>, built with
    /// <see cref="Builder.CallbackOAuthRequestBuilder"/>.
    /// </summary>
    public class CallbackOAuthRequest
    {
        public string GrantType { get; set; }

        public string AuthMechanism { get; set; }

        public string AuthorizationServerUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }

        public bool? IgnoreSsl { get; set; }

        public Dictionary<string, string> CustomParameters { get; set; }
    }
}
