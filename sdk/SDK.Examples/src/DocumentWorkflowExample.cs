using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class DocumentWorkflowExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new DocumentWorkflowExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;
        private Stream fileStream2;

        public DocumentWorkflowExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email")) {
        }

        public DocumentWorkflowExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute() {
            DocumentPackage package = PackageBuilder.NewPackageNamed ("DocumentWorkflowExample " + DateTime.Now)
					.DescribedAs ("This is a document workflow example")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
					              .AtIndex(2)
					              .WithSignature(SignatureBuilder.SignatureFor(email1)
					              		.OnPage(0)
					               		.AtPosition(100, 100)))
					.WithDocument (DocumentBuilder.NewDocumentNamed("First Document")
                                   .FromStream(fileStream2, DocumentType.PDF)
					               .AtIndex (1)
					               .WithSignature(SignatureBuilder.SignatureFor(email1)
					               		.OnPage (0)
					               		.AtPosition (100, 100)))
					.Build ();

			packageId = eslClient.CreatePackage (package);

			Console.WriteLine("Package create, id = " + packageId);

			DocumentPackage savedPackage = eslClient.GetPackage(packageId);

			savedPackage.Documents["First Document"].Index = 2;
			savedPackage.Documents["Second Document"].Index = 1;

			eslClient.PackageService.OrderDocuments(savedPackage);

			Console.WriteLine("Document order saved");
		}
	}
}