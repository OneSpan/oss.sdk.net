using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class SignerOrderingExample
	{
		public static string apiToken = "YOUR TOKEN HERE";
		public static string baseUrl = "ENVIRONMENT URL HERE";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("Signing Order " + DateTime.Now)
					.DescribedAs ("This is a signer workflow example")
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .SigningOrder(1))
					.WithSigner(SignerBuilder.NewSignerWithEmail("coco.beware@email.com")
					            .WithFirstName("Coco")
					            .WithLastName("Beware")
					            .SigningOrder(2))
					.WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
					              		.OnPage(0)
					               		.AtPosition(500, 100))
					              .WithSignature (SignatureBuilder.InitialsFor("john.smith@email.com")
					                	.OnPage (0)
					                	.AtPosition (500, 200))
					              .WithSignature(SignatureBuilder.CaptureFor ("coco.beware@email.com")
					               		.OnPage (0)
					               		.AtPosition (500, 300)))
					.WithDocument (DocumentBuilder.NewDocumentNamed("First Document")
					               .FromFile (file.FullName)
					               .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
					               		.OnPage (0)
					               		.AtPosition (500, 100)))
					.Build ();

			PackageId id = client.CreatePackage (package);

			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);
		}
	}
}