using System;

namespace SDK.Examples
{
    public class ApplicationVersionExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ApplicationVersionExample(Props.GetInstance()).Run();
        }

        public string applicationVersion;

        public ApplicationVersionExample(Props props) : this(props.Get("api.key"), props.Get("api.url"))
        {
        }

        public ApplicationVersionExample(string apiKey, string apiUrl) : base(apiKey, apiUrl)
        {
        }

        override public void Execute()
        {
            applicationVersion = eslClient.SystemService.GetApplicationVersion();
        }
    }
}

