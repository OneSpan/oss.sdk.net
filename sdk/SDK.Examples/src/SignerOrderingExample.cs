using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class SignerOrderingExample
	{
		public static string apiToken = "Q2xubnp5Y2dIQ3lROnNlY3JldA==";
		public static string baseUrl = "http://localhost:8080";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("Signing Order " + DateTime.Now)
					.DescribedAs ("This is a signer workflow example")
					.WithSigner(SignerBuilder.NewSignerWithEmail("etienne_hardy@silanis.com")
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .SigningOrder(1))
					.WithSigner(SignerBuilder.NewSignerWithEmail("etienne.hardy@gmail.com")
					            .WithFirstName("Coco")
					            .WithLastName("Beware")
					            .SigningOrder(2))
					.WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("etienne_hardy@silanis.com")
					              		.OnPage(0)
					               		.AtPosition(500, 100))
					              .WithSignature (SignatureBuilder.InitialsFor("etienne_hardy@silanis.com")
					                	.OnPage (0)
					                	.AtPosition (500, 200))
					              .WithSignature(SignatureBuilder.CaptureFor ("etienne_hardy@silanis.com")
					               		.OnPage (0)
					               		.AtPosition (500, 300)))
					.WithDocument (DocumentBuilder.NewDocumentNamed("First Document")
					               .FromFile (file.FullName)
					               .WithSignature(SignatureBuilder.SignatureFor("etienne_hardy@silanis.com")
					               		.OnPage (0)
					               		.AtPosition (500, 100)))
					.Build ();

			PackageId id = client.CreatePackage (package);

			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);
		}
	}
}