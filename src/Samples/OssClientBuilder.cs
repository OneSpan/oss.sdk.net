using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Oauth;

namespace SDK.Examples
{
    public class OssClientBuilder
    {
        private IDictionary<string, string> additionalHeaders;

        private string authType;
        private Props props;
        private ProxyConfiguration proxyConfiguration;

        public IDictionary<string, string> GetAdditionalHeaders()
        {
            return additionalHeaders;
        }

        public ProxyConfiguration GetProxyConfiguration()
        {
            return proxyConfiguration;
        }

        public string GetAuthType()
        {
            return authType;
        }

        public static OssClientBuilder NewOssClientBuilder()
        {
            return new OssClientBuilder();
        }

        public OssClientBuilder WithAuthenticationType(string authType)
        {
            this.authType = authType;
            return this;
        }

        public OssClientBuilder WithProxyConfiguration(ProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration;
            return this;
        }

        public OssClientBuilder WithAdditionalHeaders(IDictionary<string, string> additionalHeaders)
        {
            this.additionalHeaders = additionalHeaders;
            return this;
        }

        public OssClientBuilder WithProperties(Props props)
        {
            this.props = props;
            return this;
        }

        public OssClient Build()
        {
            OssClient client;
            string authType = props.Get("api.auth.type");
            if ("APIKEY".Equals(authType, StringComparison.OrdinalIgnoreCase) ||
                props.Get("api.clientId") == null && props.Get("api.oauth.clientID") == null)
                client = BuildOssClientForAPIKEY(additionalHeaders, proxyConfiguration);
            else if ("APITOKEN".Equals(authType, StringComparison.OrdinalIgnoreCase) ||
                     props.Get("api.oauth.clientID") == null)
                client = BuildOssClientForAPITOKEN(additionalHeaders, proxyConfiguration);
            else if ("OAUTH".Equals(authType, StringComparison.OrdinalIgnoreCase) ||
                     props.Get("api.oauth.clientID") != null)
                client = BuildOssClientForOAuth(additionalHeaders, proxyConfiguration);
            else
                throw new Exception("Unknown Authentication Method.");
            return client;
        }

        private OssClient BuildOssClientForOAuth(IDictionary<string, string> additionalHeaders,
            ProxyConfiguration proxyConfiguration)
        {
            OAuthTokenConfig oAuthTokenConfig = OAuthTokenConfigBuilder.NewOAuthTokenConfig()
                .WithClientId(props.Get("api.oauth.clientID"))
                .WithClientSecret(props.Get("api.oauth.clientSecret"))
                .WithAuthenticationServer(props.Get("api.oauth.server.url"))
                .Build();
            return new OssClient(oAuthTokenConfig, props.Get("api.url"), true, proxyConfiguration, additionalHeaders);
        }

        private OssClient BuildOssClientForAPITOKEN(IDictionary<string, string> additionalHeaders,
            ProxyConfiguration proxyConfiguration)
        {
            ApiTokenConfig apiTokenConfig = ApiTokenConfigBuilder.NewApiTokenConfig()
                .WithClientAppId(props.Get("api.clientId"))
                .WithClientAppSecret(props.Get("api.secret"))
                .WithTokenType(ApiTokenType.OWNER)
                .WithBaseUrl(props.Get("webpage.url"))
                .Build();
            return new OssClient(apiTokenConfig, props.Get("api.url"), true, proxyConfiguration, additionalHeaders);
        }

        private OssClient BuildOssClientForAPIKEY(IDictionary<string, string> additionalHeaders,
            ProxyConfiguration proxyConfiguration)
        {
            return new OssClient(props.Get("api.key"), props.Get("api.url"),
                true, proxyConfiguration);
        }
    }
}