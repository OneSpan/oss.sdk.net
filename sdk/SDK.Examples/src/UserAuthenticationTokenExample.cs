using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class UserAuthenticationTokenExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new UserAuthenticationTokenExample(Props.GetInstance()).Run();
        }

        public string UserSessionId{ get; private set; }

        private AuthenticationClient AuthenticationClient;

        public UserAuthenticationTokenExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("webpage.url")) {
        }

        public UserAuthenticationTokenExample( string apiKey, string apiUrl, string webpageUrl) : base( apiKey, apiUrl ) {
            this.AuthenticationClient = new AuthenticationClient(webpageUrl);
        }

        override public void Execute()
        {            
            string userAuthenticationToken = eslClient.GetAuthenticationTokenService().CreateUserAuthenticationToken();

            UserSessionId = AuthenticationClient.GetSessionIdForUserAuthenticationToken(userAuthenticationToken);
        }
    }
}

