using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class GetPackageExample
	{
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("Fields example " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .WithCompany ("Acme Inc")
					            .WithTitle ("Managing Director"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
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