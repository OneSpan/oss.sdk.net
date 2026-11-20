using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// A callback of the account.
    /// </summary>
    /// <remarks>
    /// <para><c>Type</c> is <c>ACCOUNT</c> for the account-level callback and <c>CONNECTOR</c> for a connector's.</para>
    /// <para>Secrets are never returned: the key, the OAuth client secret, and the values of custom headers and OAuth
    /// parameters. Header and parameter names are returned.</para>
    /// <para><c>Type</c>, <c>AuthType</c> and the OAuth settings are plain strings, and an event this SDK version does
    /// not know is returned as an unrecognized <see cref="NotificationEvent"/>, so new server values do not break older
    /// SDKs.</para>
    /// </remarks>
    public class CallbackConfiguration
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string AuthType { get; set; }

        public List<NotificationEvent> Events { get; set; }

        public List<string> CustomHeaderNames { get; set; }

        public CallbackOAuthSettings OAuth { get; set; }

        public bool MtlsEnabled { get; set; }

        public bool Enabled { get; set; }
    }
}
