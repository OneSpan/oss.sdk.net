using System;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Oauth
{
    public class OAuthAccessToken
    {
        [JsonProperty ("access_token")]
        public String AccessToken 
        {
            get; set;
        }
        
        [JsonProperty ("token_type")]
        public String TokenType 
        {
            get; set;
        }
        
        [JsonProperty ("scope")]
        public String Scope 
        {
            get; set;
        }
        
        [JsonProperty ("expires_in")]
        public long ExpiresAt 
        {
            get; set;
        }
        
    }
}