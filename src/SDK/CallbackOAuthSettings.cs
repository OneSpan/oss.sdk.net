using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// The OAuth settings of a <see cref="CallbackConfiguration"/>, without the client secret or parameter values.
    /// </summary>
    public class CallbackOAuthSettings
    {
        public string GrantType { get; set; }

        public string AuthMechanism { get; set; }

        public string AuthorizationServerUrl { get; set; }

        public string ClientId { get; set; }

        public string Scope { get; set; }

        public bool IgnoreSsl { get; set; }

        public List<string> CustomParameterNames { get; set; }
    }
}
