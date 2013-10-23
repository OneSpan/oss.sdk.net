using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public abstract class SDKSample
    {
        protected EslClient eslClient;

        public SDKSample( string apiUrl, string apiKey )
        {
            Console.Out.WriteLine("apiUrl: " + apiUrl + ", apiKey: " + apiKey);
            eslClient = new EslClient(apiKey, apiUrl);
            Console.Out.WriteLine("eslClient: " + eslClient);
        }

        public abstract void Execute();

        public void Run() {
            Execute();
        }
    }
}

