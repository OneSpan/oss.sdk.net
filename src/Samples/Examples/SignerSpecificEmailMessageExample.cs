using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class SignerSpecificEmailMessageExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new SignerSpecificEmailMessageExample().Run();
        }

        public readonly string EMAIL_MESSAGE = "Hi John, could you sign this asap please?";

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith")
                                .WithEmailMessage(EMAIL_MESSAGE))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
					              		.OnPage(0)
					               		.AtPosition(500, 100))
                                  .WithSignature (SignatureBuilder.InitialsFor(email1)
					                	.OnPage (0)
					                	.AtPosition (500, 200))
                                  .WithSignature(SignatureBuilder.CaptureFor (email1)
					               		.OnPage (0)
					               		.AtPosition (500, 300)))
					.Build ();

            packageId = ossClient.CreatePackage (package);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
		}
	}
}