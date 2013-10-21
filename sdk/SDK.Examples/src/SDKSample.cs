using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public abstract class SDKSample
    {
        protected EslClient eslClient;

        public SDKSample( string apiUrl, string apiKey )
        {
            eslClient = new EslClient(apiKey, apiUrl);
        }

        public abstract void Execute();

        public void Run() {
            Execute();
        }
    }
}

