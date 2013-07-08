using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class FieldExtractionExample
	{
		public static string apiToken = "YOUR TOKEN HERE";
		public static string baseUrl = "ENVIRONMENT URL HERE";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document-with-fields.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("Field extraction example")
				.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
					              	.FromFile(file.FullName)
					              	.EnableExtraction()
					              	.WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
					            		.WithName("AGENT_SIG_1")
					               		.EnableExtraction()
					               		.WithField(FieldBuilder.SignatureDate()
					           				.WithName("AGENT_SIG_2")
					           				.WithExtraction())))
					.Build ();

			PackageId id = client.CreatePackage (package);

			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);
		}
	}
}