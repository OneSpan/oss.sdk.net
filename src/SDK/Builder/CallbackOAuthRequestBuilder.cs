using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
    /// <summary>
    /// Builds the OAuth 2.0 settings used when <c>AuthType</c> is <c>OAUTH2_AUTH</c>.
    /// </summary>
    public class CallbackOAuthRequestBuilder
    {
        private string grantType;
        private string authMechanism;
        private string authorizationServerUrl;
        private string clientId;
        private string clientSecret;
        private string scope;
        private bool? ignoreSsl;
        private Dictionary<string, string> customParameters;

        private CallbackOAuthRequestBuilder()
        {
        }

        public static CallbackOAuthRequestBuilder NewCallbackOAuthRequest()
        {
            return new CallbackOAuthRequestBuilder();
        }

        /// <summary>
        /// Sets the grant type, for example <c>CLIENT_CREDENTIALS</c>.
        /// </summary>
        public CallbackOAuthRequestBuilder WithGrantType(string grantType)
        {
            this.grantType = grantType;
            return this;
        }

        /// <summary>
        /// Sets how the client ID and secret are sent to the authorization server: <c>HEADER</c> (HTTP Basic) or
        /// <c>FORM_DATA</c> (in the request body).
        /// </summary>
        public CallbackOAuthRequestBuilder WithAuthMechanism(string authMechanism)
        {
            this.authMechanism = authMechanism;
            return this;
        }

        public CallbackOAuthRequestBuilder WithAuthorizationServerUrl(string authorizationServerUrl)
        {
            this.authorizationServerUrl = authorizationServerUrl;
            return this;
        }

        public CallbackOAuthRequestBuilder WithClientId(string clientId)
        {
            this.clientId = clientId;
            return this;
        }

        public CallbackOAuthRequestBuilder WithClientSecret(string clientSecret)
        {
            this.clientSecret = clientSecret;
            return this;
        }

        public CallbackOAuthRequestBuilder WithScope(string scope)
        {
            this.scope = scope;
            return this;
        }

        /// <summary>
        /// Sets <c>IgnoreSsl</c>. On replace, switching it on means the client secret and custom parameter values must
        /// be sent again; see <see cref="Services.CallbackConfigurationService.ReplaceCallbackConfiguration"/>.
        /// </summary>
        public CallbackOAuthRequestBuilder WithIgnoreSsl(bool ignoreSsl)
        {
            this.ignoreSsl = ignoreSsl;
            return this;
        }

        /// <summary>
        /// Adds a custom parameter to the token request. On replace, a <c>null</c> value keeps the saved one and a
        /// parameter left out is removed; see
        /// <see cref="Services.CallbackConfigurationService.ReplaceCallbackConfiguration"/>.
        /// </summary>
        public CallbackOAuthRequestBuilder WithCustomParameter(string name, string value)
        {
            if (customParameters == null)
            {
                customParameters = new Dictionary<string, string>();
            }
            customParameters[name] = value;
            return this;
        }

        public CallbackOAuthRequest Build()
        {
            return new CallbackOAuthRequest
            {
                GrantType = grantType,
                AuthMechanism = authMechanism,
                AuthorizationServerUrl = authorizationServerUrl,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope,
                IgnoreSsl = ignoreSsl,
                CustomParameters = customParameters
            };
        }
    }
}
