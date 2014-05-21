using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public abstract class SDKSample
    {
		protected EslClient eslClient;
		protected PackageId packageId;

		public EslClient EslClient
		{
			get
			{
				return eslClient;
			}
		}

		public PackageId PackageId
		{
			get
			{
				return packageId;
			}
		}

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
        
        protected string GetRandomEmail() {
            return System.Guid.NewGuid().ToString().Replace("-","") + "@e-signlive.com";
        }
    }
}

