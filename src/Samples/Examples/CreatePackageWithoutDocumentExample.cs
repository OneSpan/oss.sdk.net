using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class CreatePackageWithoutDocumentExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new CreatePackageWithoutDocumentExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}

