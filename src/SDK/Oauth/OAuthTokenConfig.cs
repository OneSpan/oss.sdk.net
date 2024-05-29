using System;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Oauth 
{
    
    public class OAuthTokenConfig
    {
        private string grantType = "client_credentials";

        public string ClientId
        {
            get; set;
        }
        
        public string ClientSecret
        {
            get; set;
        }
        
        public string AuthenticationServer
        {
            get; set;
        }
    }
}
