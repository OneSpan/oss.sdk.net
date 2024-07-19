using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.OSSProvider
{
    public class OSSAuthClientConfig
    {
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string AuthenticationServer { get; private set; }
        public string ApiUrl { get; private set; }
        public bool AllowAllSSLCertificatesFlag { get; private set; }
        public ProxyConfiguration ProxyConfig { get; private set; }
        public bool UseSystemProperties { get; private set; }
        public IDictionary<string, string> Headers { get; private set; }
        
        private OSSAuthClientConfig(string clientId, string clientSecret, string authenticationServer, string apiUrl,
            bool allowAllSSLCertificatesFlag, ProxyConfiguration proxyConfig, bool useSystemProperties, IDictionary<string, string> headers)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            AuthenticationServer = authenticationServer;
            ApiUrl = apiUrl;
            AllowAllSSLCertificatesFlag = allowAllSSLCertificatesFlag;
            ProxyConfig = proxyConfig;
            UseSystemProperties = useSystemProperties;
            Headers = headers;
        }
        
        public override string ToString()
        {
            return $"OneSpanClient{{ 'authenticationServerUrl':'{AuthenticationServer}', 'clientId': '{ClientId}', 'apiUrl': '{ApiUrl}', 'allowAllSSLCertificatesFlag': '{AllowAllSSLCertificatesFlag}', 'useSystemProperties': '{UseSystemProperties}'}}";
        }
        
        public class Builder
        {
            private string clientId;
            private string clientSecret;
            private string authenticationServer;
            private string apiUrl;
            private bool allowAllSSLCertificatesFlag;
            private ProxyConfiguration proxyConfig;
            private bool useSystemProperties;
            private Dictionary<string, string> headers = new Dictionary<string, string>();

            public Builder WithClientId(string clientId)
            {
                this.clientId = clientId;
                return this;
            }

            public Builder WithClientSecret(string clientSecret)
            {
                this.clientSecret = clientSecret;
                return this;
            }

            public Builder WithAuthenticationServer(string authenticationServer)
            {
                this.authenticationServer = authenticationServer;
                return this;
            }

            public Builder WithApiUrl(string apiUrl)
            {
                this.apiUrl = apiUrl;
                return this;
            }

            public Builder WithAllowAllSSLCertificatesFlag(bool allowAllSSLCertificatesFlag)
            {
                this.allowAllSSLCertificatesFlag = allowAllSSLCertificatesFlag;
                return this;
            }

            public Builder WithProxyConfiguration(ProxyConfiguration proxyConfig)
            {
                this.proxyConfig = proxyConfig;
                return this;
            }

            public Builder WithUseSystemProperties(bool useSystemProperties)
            {
                this.useSystemProperties = useSystemProperties;
                return this;
            }

            public Builder WithHeaders(Dictionary<string, string> headers)
            {
                this.headers = headers;
                return this;
            }

            public OSSAuthClientConfig Build()
            {
                Asserts.NotEmptyOrNull(clientId, "clientId");
                Asserts.NotEmptyOrNull(clientSecret, "clientSecret");
                Asserts.NotEmptyOrNull(authenticationServer, "authenticationServer");
                Asserts.NotEmptyOrNull(apiUrl, "apiUrl");
                return new OSSAuthClientConfig(clientId, clientSecret, authenticationServer, apiUrl, allowAllSSLCertificatesFlag, proxyConfig, useSystemProperties, headers);
            }
        }
    }
}