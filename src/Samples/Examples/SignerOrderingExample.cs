using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class SignerOrderingExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new SignerOrderingExample().Run();
        }

        public DocumentPackage savedPackage, afterReorder;

        override public void Execute()
		{
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
					.DescribedAs ("This is a signer workflow example")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("Coco")
					            .WithLastName("Beware")
								.SigningOrder(1))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .SigningOrder(2))			
					.Build ();

            packageId = ossClient.CreatePackage (package);
            
			Console.WriteLine("Package created, id = " + packageId);

			savedPackage = ossClient.GetPackage(packageId);

            // Reorder signers
            afterReorder = ossClient.GetPackage(packageId);
            afterReorder.GetSigner(email2).SigningOrder = 1;
            afterReorder.GetSigner(email1).SigningOrder = 2;
            ossClient.PackageService.OrderSigners(afterReorder);

            afterReorder = ossClient.GetPackage(packageId);

			Console.WriteLine("Signer order changed");
		}
	}
}