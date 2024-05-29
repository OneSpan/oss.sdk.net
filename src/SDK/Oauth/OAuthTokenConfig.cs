using System;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Oauth 
{
    
    public class OAuthTokenConfig
    {
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
