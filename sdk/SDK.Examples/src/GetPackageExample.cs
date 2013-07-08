using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class GetPackageExample
	{
		public static string apiToken = "Q2xubnp5Y2dIQ3lROnNlY3JldA==";
		public static string baseUrl = "http://localhost:8080";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("Fields example " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail("etienne_hardy@silanis.com")
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .WithCompany ("Acme Inc")
					            .WithTitle ("Managing Director"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("etienne_hardy@silanis.com")
					              		.OnPage(0)
					               		.AtPosition(500, 100)
					               		.WithField(FieldBuilder.SignatureDate()
					           				.OnPage(0)
					           				.AtPosition(500, 200))
					               		.WithField(FieldBuilder.SignerName ()
					           				.OnPage(0)
					           				.AtPosition(500, 300))
							         	.WithField(FieldBuilder.SignerTitle()
							           		.OnPage(0)
							           		.AtPosition(500, 400))
					               		.WithField (FieldBuilder.SignerCompany()
					            			.OnPage (0)
					            			.AtPosition (500, 500))))
					.Build ();

			PackageId id = client.CreatePackage (package);
			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);

			DocumentPackage retrievedPackage = client.GetPackage (id);

			Console.WriteLine ("id = " + retrievedPackage.Id);
			Console.WriteLine ("status = " + retrievedPackage.Status);
		}
	}
}