namespace SDK.Examples
{
    public abstract class OAuth2SDKSample : BaseSDKSample
    {
        public string clientId { get; private set; }
        public string clientSecret { get; private set; }
        public string oAuthServerUrl { get; private set; }
        public string apiUrl { get; private set; }
        public string senderId { get; private set; }
        public string delegatorId { get; private set; }


        private void SetOAuth2Props()
        {
            clientId = props.Get("api.oauth.clientID");
            clientSecret = props.Get("api.oauth.clientSecret");
            oAuthServerUrl = props.Get("api.oauth.server.url");
            apiUrl = props.Get("api.url");
            senderId = props.Get("api.oauth.senderId");
            delegatorId = props.Get("api.oauth.delegatorId");
        }

        public OAuth2SDKSample()
        {
            SetProperties();
            SetOAuth2Props();
        }
    }
}