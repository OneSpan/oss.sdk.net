using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class DocumentOperationsExample : SDKSample
	{
		public DocumentOperationsExample(Props props) : base(props.Get("api.url"), props.Get("api.key"))
		{
		}

		override public void Execute()
		{
			FileInfo file = new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf");

			// 1. Create a package
			DocumentPackage package = PackageBuilder.NewPackageNamed("Policy " + DateTime.Now)
				.DescribedAs("This is a package demonstrating document upload")
			    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
      					.WithFirstName("John")
      					.WithLastName("Smith")
      					.WithTitle("Managing Director")
      					.WithCompany("Acme Inc."))
  				.Build();

			package.Id = eslClient.CreatePackage(package);
			Console.WriteLine("package created, id = " + package.Id);

			// 2. Construct a document
			Document document = DocumentBuilder.NewDocumentNamed("First Document")
				.FromFile(file.FullName)
				.WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
						.OnPage(0))                                
				.Build();

			// 3. Attach the document to the created package by uploading the document.
			document = eslClient.UploadDocument(document, package);
			Console.WriteLine("Document was uploaded");

			//This is how you would update a document's metadata (name, description)
			document.Name = "New document name";
			document.Description = "New description";

			eslClient.PackageService.UpdateDocumentMetadata(package, document);
			Console.WriteLine("Document was updated");

			//This is how you would delete a document from a package
			eslClient.PackageService.DeleteDocument(package.Id, document.Id);
		}

		public static void Main(string[] args)
		{
			new DocumentOperationsExample(Props.GetInstance()).Run();
		}
	}
}