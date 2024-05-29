using OneSpanSign.Sdk.Oauth;

namespace OneSpanSign.Sdk.Builder
{
    public class ApiTokenConfigBuilder
    {
        private string baseUrl;
        private string clientAppId;
        private string clientAppSecret;
        private ApiTokenType tokenType;
        private string senderEmail;
        
        public static ApiTokenConfigBuilder NewApiTokenConfig() {
            return new ApiTokenConfigBuilder();
        }

        public ApiTokenConfigBuilder WithBaseUrl( string baseUrl ) 
        {
            this.baseUrl = baseUrl;
            return this;
        }
        
        public ApiTokenConfigBuilder WithClientAppId( string clientAppId ) 
        {
            this.clientAppId = clientAppId;
            return this;
        }
        
        public ApiTokenConfigBuilder WithClientAppSecret( string clientAppSecret ) 
        {
            this.clientAppSecret = clientAppSecret;
            return this;
        }
        
        public ApiTokenConfigBuilder WithTokenType( ApiTokenType tokenType  ) 
        {
            this.tokenType = tokenType;
            return this;
        }
        
        public ApiTokenConfigBuilder WithSenderEmail( string senderEmail ) 
        {
            this.senderEmail = senderEmail;
            return this;
        }
        
        public ApiTokenConfig Build()
        {
            ApiTokenConfig apiTokenConfig = new ApiTokenConfig();
            apiTokenConfig.BaseUrl= baseUrl;
            apiTokenConfig.ClientAppId = clientAppId;
            apiTokenConfig.ClientAppSecret = clientAppSecret;
            apiTokenConfig.TokenType = tokenType;
            apiTokenConfig.SenderEmail = senderEmail;

            return apiTokenConfig;
        }

    }
}