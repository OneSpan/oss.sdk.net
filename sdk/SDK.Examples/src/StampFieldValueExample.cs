using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class StampFieldValueExample
	{
		public static string apiToken = "Q2xubnp5Y2dIQ3lROnNlY3JldA==";
		public static string baseUrl = "http://localhost:8080";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document-with-fields.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("Field extraction example")
				.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail("etienne_hardy@silanis.com")
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
					              	.FromFile(file.FullName)
					              	.EnableExtraction()
					              	.WithSignature(SignatureBuilder.SignatureFor("etienne_hardy@silanis.com")
					            		.WithName("AGENT_SIG_1")
					               		.EnableExtraction())
					              	.WithField(FieldBuilder.Label()
					           			.WithName ("AGENT_SIG_2")
					           			.WithValue("Value to be stamped")))
					.Build ();

			PackageId id = client.CreatePackage (package);

			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);
		}
	}
}