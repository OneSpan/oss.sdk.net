using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class FieldPositionExtractionExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new FieldPositionExtractionExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
				.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                    .FromStream(fileStream1, DocumentType.PDF)
					              	.EnableExtraction()
					              	.WithSignature(SignatureBuilder.SignatureFor(email1)
					            		.WithName("AGENT_SIG_1")
					               		.WithPositionExtracted()                                    
					               		.WithField(FieldBuilder.SignatureDate()
					           				.WithName("AGENT_SIG_2")
					           				.WithPositionExtracted())))
					.Build ();

			PackageId id = ossClient.CreatePackage (package);
			ossClient.SendPackage(id);
		}
	}
}