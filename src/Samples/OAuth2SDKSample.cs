namespace SDK.Examples
{
    public abstract class OAuth2SDKSample : BaseSDKSample
    {
        public string clientId { get; private set; }
        public string clientSecret { get; private set; }
        public string oAuthServerUrl { get; private set; }
        public string apiUrl { get; private set; }


        private void SetOAuth2Props()
        {
            clientId = props.Get("api.oauth.clientID");
            clientSecret = props.Get("api.oauth.clientSecret");
            oAuthServerUrl = props.Get("api.oauth.server.url");
            apiUrl = props.Get("api.url");
        }

        public OAuth2SDKSample()
        {
            SetOAuth2Props();
        }
    }
}