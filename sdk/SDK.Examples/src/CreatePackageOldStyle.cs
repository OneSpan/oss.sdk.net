using System;
using System.IO;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
	class CreatePackageOldStyle
	{
		public static string apiToken = "NzRtVWtJT3dkUVhGOnNlY3JldA==";
		public static string baseUrl = "http://localhost:8080";

		public static void Main (string[] args)
		{
			byte[] file = File.ReadAllBytes("/Users/ehardy/Documents/test-docs/one_page_document_standard_size.pdf");

			// Create new esl client with api token and base url
			EslClient eslClient = new EslClient (apiToken, baseUrl);

			Package package = new Package ();

			package.Name = "Testing package from C#";
			package.Autocomplete = true;

			Signer signer = new Signer ();

			signer.Email = "etienne_hardy@silanis.com";
			signer.FirstName = "Etienne";
			signer.LastName = "Silanis";

			Role role = new Role();
			role.Id = "signer1";
			role.AddSigner (signer);
			package.AddRole(role);

			// Create a new package
			PackageId packageId = eslClient.PackageService.CreatePackage(package);

			Console.WriteLine ("Package {0} was created", packageId.Id);

			Field signature = new Field ();

			signature.Type = FieldType.SIGNATURE;
			signature.Subtype = FieldSubtype.FULLNAME;
			signature.Left = 500;
			signature.Top = 100;
			signature.Width = 200;
			signature.Height = 50;
			signature.Name = "Sign Here";

			Approval approval = new Approval ();
			approval.AddField (signature);
			approval.Role = "signer1";

			Document document = new Document ();
			document.AddApproval(approval);
			document.Name = "Document to sign";

			// Upload document to package
			eslClient.PackageService.UploadDocument(packageId, "one_page_document_standard_size.pdf", file, document);

			Console.WriteLine ("Document was uploaded to package {0}", packageId.Id);

			// Send the package
			eslClient.PackageService.SendPackage(packageId);

			Console.WriteLine ("Package {0} was sent", packageId.Id);
		}
	}
}
