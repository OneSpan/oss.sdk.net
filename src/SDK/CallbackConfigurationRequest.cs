using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// A callback configuration to create, replace or test, built with
    /// <see cref="Builder.CallbackConfigurationRequestBuilder"/>.
    /// </summary>
    /// <remarks>
    /// A property left <c>null</c> is not sent; see
    /// <see cref="Services.CallbackConfigurationService.ReplaceCallbackConfiguration"/> for what that means on replace.
    /// </remarks>
    public class CallbackConfigurationRequest
    {
        public string Url { get; set; }

        public string AuthType { get; set; }

        public List<string> Events { get; set; }

        public string Key { get; set; }

        public CallbackOAuthRequest OAuth { get; set; }

        public Dictionary<string, string> CustomHeaders { get; set; }

        public bool? MtlsEnabled { get; set; }
    }
}
