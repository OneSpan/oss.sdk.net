using System;

namespace SDK.Examples
{
    public class UserAuthenticationTokenExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new UserAuthenticationTokenExample(Props.GetInstance()).Run();
        }

        public string UserAuthenticationToken{ get; private set; }

        public UserAuthenticationTokenExample( Props props ) : this(props.Get("api.url"), props.Get("api.key")) {
        }

        public UserAuthenticationTokenExample( string apiKey, string apiUrl) : base( apiKey, apiUrl ) {
        }

        override public void Execute()
        {            
            UserAuthenticationToken = eslClient.CreateUserAuthenticationToken();
            Console.WriteLine("User Authentication Token = " + UserAuthenticationToken);
        }


    }
}

