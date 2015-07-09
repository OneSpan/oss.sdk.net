using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class SignerOrderingExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new SignerOrderingExample(Props.GetInstance()).Run();
        }

        public string email1;
        public string email2;

        public DocumentPackage savedPackage, afterReorder;

        public SignerOrderingExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email")) {
        }

        public SignerOrderingExample( string apiKey, string apiUrl, string email1, string email2 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.email2 = email2;            
        }

        override public void Execute()
		{
			DocumentPackage package = PackageBuilder.NewPackageNamed ("Signing Order " + DateTime.Now)
					.DescribedAs ("This is a signer workflow example")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email2)
					            .WithFirstName("Coco")
					            .WithLastName("Beware")
								.SigningOrder(2))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .SigningOrder(1))			
					.Build ();

            packageId = eslClient.CreatePackage (package);
            
			Console.WriteLine("Package created, id = " + packageId);

			savedPackage = EslClient.GetPackage(packageId);

            // Reorder signers
            afterReorder = eslClient.GetPackage(packageId);
            afterReorder.GetSigner(email2).SigningOrder = 1;
            afterReorder.GetSigner(email1).SigningOrder = 2;
            eslClient.PackageService.OrderSigners(afterReorder);

            afterReorder = eslClient.GetPackage(packageId);

			Console.WriteLine("Signer order changed");
		}
	}
}