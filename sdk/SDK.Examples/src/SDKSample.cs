using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public abstract class SDKSample
    {
		protected EslClient eslClient;
        protected PackageId packageId;
        protected DocumentPackage retrievedPackage;

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

        public DocumentPackage RetrievedPackage
        {
            get { return retrievedPackage; }
        }

        public SDKSample( string apiKey, string apiUrl )
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

