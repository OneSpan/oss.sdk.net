using System;
using System.IO;
using System.Globalization;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class PackageEditExample : SDKSample
	{
        public static void Main (string[] args)
        {
			new PackageEditExample(Props.GetInstance()).Run();
        }

		public PackageEditExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) 
		{
        }

		public PackageEditExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) 
		{        
        }

        override public void Execute()
        {
			Stream fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

			DocumentPackage superDuperPackage =
				PackageBuilder.NewPackageNamed("PackageEditExample: " + DateTime.Now)
					.DescribedAs("This is a package created using the e-SignLive SDK")
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@acme.com")
						.WithCustomId("Client1")
						.WithFirstName("John")
						.WithLastName("Smith")
					)
					.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
						.FromStream(fileStream, DocumentType.PDF)
						.WithSignature(SignatureBuilder.SignatureFor("john.smith@acme.com")
							.OnPage(0)
							.AtPosition(100, 100)
						)
					)
					.Build();

			PackageId packageId = eslClient.CreateAndSendPackage(superDuperPackage);

			Console.WriteLine("Package sent, id = " + packageId);

			eslClient.PackageService.Edit(packageId);

			Console.WriteLine("Package {0} back in DRAFT state", packageId);
        }
	}
}