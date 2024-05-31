using OneSpanSign.Sdk.Oauth;

namespace OneSpanSign.Sdk.Builder
{
    public class OAuthTokenConfigBuilder
    {
        private string clientId;
        private string clientSecret;
        private string authenticationServer;
        
        public static OAuthTokenConfigBuilder NewOAuthTokenConfig() {
            return new OAuthTokenConfigBuilder();
        }

        public OAuthTokenConfigBuilder WithClientId( string clientId ) 
        {
            this.clientId = clientId;
            return this;
        }
        
        public OAuthTokenConfigBuilder WithClientSecret( string clientSecret ) 
        {
            this.clientSecret = clientSecret;
            return this;
        }
        
        public OAuthTokenConfigBuilder WithAuthenticationServer( string authenticationServer ) 
        {
            this.authenticationServer = authenticationServer;
            return this;
        }
        
        public OAuthTokenConfig Build()
        {
            OAuthTokenConfig oAuthTokenConfig = new OAuthTokenConfig();
            oAuthTokenConfig.ClientId= clientId;
            oAuthTokenConfig.ClientSecret = clientSecret;
            oAuthTokenConfig.AuthenticationServer = authenticationServer;

            return oAuthTokenConfig;
        }

    }
}