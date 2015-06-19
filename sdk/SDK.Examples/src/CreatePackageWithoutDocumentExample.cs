using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CreatePackageWithoutDocumentExample : SDKSample
    {
        public readonly string PACKAGE_NAME = "CreatePackageWithoutDocumentExample: " + DateTime.Now;

        public static void Main(string[] args)
        {
            new CreatePackageWithoutDocumentExample(Props.GetInstance()).Run();
        }

        public CreatePackageWithoutDocumentExample(Props props) : this(props.Get("api.key"), props.Get("api.url"))
        {
        }

        public CreatePackageWithoutDocumentExample(string apiKey, string apiUrl) : base( apiKey, apiUrl )
        {
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PACKAGE_NAME)
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

