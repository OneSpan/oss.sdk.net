using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class SignerOrderingExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new SignerOrderingExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private Stream fileStream1;
        private Stream fileStream2;

        public SignerOrderingExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email")) {
        }

        public SignerOrderingExample( string apiKey, string apiUrl, string email1, string email2 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
		{
			DocumentPackage package = PackageBuilder.NewPackageNamed ("Signing Order " + DateTime.Now)
					.DescribedAs ("This is a signer workflow example")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .SigningOrder(1))
					.WithSigner(SignerBuilder.NewSignerWithEmail(email2)
					            .WithFirstName("Coco")
					            .WithLastName("Beware")
					            .SigningOrder(2))
					.WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
					              .WithSignature(SignatureBuilder.SignatureFor(email1)
					              		.OnPage(0)
					               		.AtPosition(500, 100))
					              .WithSignature (SignatureBuilder.InitialsFor(email1)
					                	.OnPage (0)
					                	.AtPosition (500, 200))
					              .WithSignature(SignatureBuilder.CaptureFor (email2)
					               		.OnPage (0)
					               		.AtPosition (500, 300)))
					.WithDocument (DocumentBuilder.NewDocumentNamed("First Document")
                                   .FromStream(fileStream2, DocumentType.PDF)
					               .WithSignature(SignatureBuilder.SignatureFor(email1)
					               		.OnPage (0)
					               		.AtPosition (500, 100)))
					.Build ();

			PackageId id = eslClient.CreatePackage (package);
			eslClient.SendPackage(id);
		}
	}
}