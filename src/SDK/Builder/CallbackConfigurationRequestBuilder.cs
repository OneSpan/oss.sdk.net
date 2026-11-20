using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
    /// <summary>
    /// Builds a <see cref="CallbackConfigurationRequest"/>.
    /// </summary>
    /// <remarks>
    /// Create and replace need <c>Url</c>, <c>AuthType</c> and at least one event. A property left out is not sent;
    /// see <see cref="Services.CallbackConfigurationService.ReplaceCallbackConfiguration"/> for what that means on
    /// replace.
    /// </remarks>
    public class CallbackConfigurationRequestBuilder
    {
        private string url;
        private string authType;
        private List<string> events;
        private string key;
        private CallbackOAuthRequest oauth;
        private Dictionary<string, string> customHeaders;
        private bool? mtlsEnabled;

        private CallbackConfigurationRequestBuilder()
        {
        }

        public static CallbackConfigurationRequestBuilder NewCallbackConfigurationRequest()
        {
            return new CallbackConfigurationRequestBuilder();
        }

        public CallbackConfigurationRequestBuilder WithUrl(string url)
        {
            this.url = url;
            return this;
        }

        /// <summary>
        /// Sets the authentication type: <c>NO_AUTH</c>, <c>BASIC_AUTH</c> (also set <see cref="WithKey"/>) or
        /// <c>OAUTH2_AUTH</c> (also set <see cref="WithOAuth(CallbackOAuthRequest)"/>).
        /// </summary>
        public CallbackConfigurationRequestBuilder WithAuthType(string authType)
        {
            this.authType = authType;
            return this;
        }

        /// <summary>
        /// Sets the events by their API name, for example for an event this SDK version does not know yet. Replaces
        /// any events set before.
        /// </summary>
        public CallbackConfigurationRequestBuilder WithEvents(params string[] events)
        {
            this.events = new List<string>(events);
            return this;
        }

        public CallbackConfigurationRequestBuilder WithEvents(params NotificationEvent[] events)
        {
            this.events = new List<string>();
            foreach (NotificationEvent notificationEvent in events)
            {
                // The name is not always the API value: PACKAGE_READY_FOR_COMPLETION is sent as PACKAGE_READY_FOR_COMPLETE.
                this.events.Add(new EventNotificationConverter(notificationEvent).ToAPICallbackEvent());
            }
            return this;
        }

        /// <summary>
        /// Sets the key sent as <c>Authorization: Basic &lt;key&gt;</c> on each callback, for <c>BASIC_AUTH</c>.
        /// </summary>
        public CallbackConfigurationRequestBuilder WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public CallbackConfigurationRequestBuilder WithOAuth(CallbackOAuthRequest oauth)
        {
            this.oauth = oauth;
            return this;
        }

        public CallbackConfigurationRequestBuilder WithOAuth(CallbackOAuthRequestBuilder builder)
        {
            return WithOAuth(builder.Build());
        }

        /// <summary>
        /// Adds a custom header sent on each callback. On replace, a <c>null</c> value keeps the saved one and a header
        /// left out is removed; see <see cref="Services.CallbackConfigurationService.ReplaceCallbackConfiguration"/>.
        /// </summary>
        public CallbackConfigurationRequestBuilder WithCustomHeader(string name, string value)
        {
            if (customHeaders == null)
            {
                customHeaders = new Dictionary<string, string>();
            }
            customHeaders[name] = value;
            return this;
        }

        /// <summary>
        /// Sets whether OneSpan presents its client certificate on each callback. Your endpoint must then trust
        /// <see cref="Services.CallbackConfigurationService.GetClientCertificateIntermediate"/>.
        /// </summary>
        public CallbackConfigurationRequestBuilder WithMtlsEnabled(bool mtlsEnabled)
        {
            this.mtlsEnabled = mtlsEnabled;
            return this;
        }

        public CallbackConfigurationRequest Build()
        {
            return new CallbackConfigurationRequest
            {
                Url = url,
                AuthType = authType,
                Events = events,
                Key = key,
                OAuth = oauth,
                CustomHeaders = customHeaders,
                MtlsEnabled = mtlsEnabled
            };
        }
    }
}
