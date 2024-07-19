using System.Collections.Concurrent;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Oauth;

namespace OneSpanSign.Sdk.OSSProvider
{
    public sealed class OSSAuthClientProvider
    {
        private static OSSAuthClientProvider instance = null;
        private static readonly object padlock = new object();
        private ConcurrentDictionary<string, OssClient> clients;

        OSSAuthClientProvider()
        {
            clients = new ConcurrentDictionary<string, OssClient>();
        }

        public static OSSAuthClientProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new OSSAuthClientProvider();
                        }
                    }
                }

                return instance;
            }
        }
        
        public OssClient GetOssClient(OSSAuthClientConfig config)
        {
            OssClient newClient = CreateNewClient(config);
            
            clients.AddOrUpdate(config.ClientId, newClient, (key, existingClient) =>
            {
                if (existingClient == null ||
                    !existingClient.oauth2TokenConfig.ClientSecret.Equals(config.ClientSecret))
                {
                    return newClient;
                }
                return existingClient;
            });

            OssClient result;
            clients.TryGetValue(config.ClientId, out result);
            return result;
        }

        private OssClient CreateNewClient(OSSAuthClientConfig config)
        {
            OAuthTokenConfig oAuthTokenConfig = OAuthTokenConfigBuilder.NewOAuthTokenConfig()
                .WithClientId(config.ClientId)
                .WithClientSecret(config.ClientSecret)
                .WithAuthenticationServer(config.AuthenticationServer)  
                .Build();
            
            return new OssClient(oAuthTokenConfig, config.ApiUrl, config.AllowAllSSLCertificatesFlag, config.ProxyConfig, config.Headers);

        }
    }
}