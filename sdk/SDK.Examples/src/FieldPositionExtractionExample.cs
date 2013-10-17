using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class FieldPositionExtractionExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new FieldPositionExtractionExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public FieldPositionExtractionExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public FieldPositionExtractionExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed ("FieldPositionExtractionExample example")
				.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                    .FromStream(fileStream1, DocumentType.PDF)
					              	.EnableExtraction()
					              	.WithSignature(SignatureBuilder.SignatureFor(email1)
					            		.WithName("AGENT_SIG_1")
					               		.EnableExtraction()                                    
					               		.WithField(FieldBuilder.SignatureDate()
					           				.WithName("AGENT_SIG_2")
					           				.WithPositionExtracted())))
					.Build ();

			PackageId id = eslClient.CreatePackage (package);
			eslClient.SendPackage(id);
		}
	}
}