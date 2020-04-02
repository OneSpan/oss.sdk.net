using System;
using System.IO;
using System.Globalization;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class PackageTrashExample : SDKSample
	{
        public static void Main (string[] args)
        {
			new PackageTrashExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
				.DescribedAs( "This package should be trashed" )					                                                           
                    .Build();

            PackageId packageId = ossClient.CreatePackage( superDuperPackage );
            
			Console.WriteLine("packageId = " + packageId);

			ossClient.PackageService.Trash(packageId);

			Console.WriteLine("Package {0} should be trashed", packageId);
        }
	}
}