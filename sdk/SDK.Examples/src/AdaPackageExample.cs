using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class AdaPackageExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AdaPackageExample().Run();
        }

        public readonly string DOCUMENT_NAME = "First Document";

        override public void Execute()
        {
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/taggedDocument.pdf").FullName);

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithAda())
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John1")
                    .WithLastName("Smith1"))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .EnableExtraction()
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .WithId(new SignatureId("CustomerSignature"))
                        .WithName("CustomerSignature")
                        .WithStyle(SignatureStyle.FULL_NAME)
                        .WithPositionExtracted()
                        .WithField(FieldBuilder.TextField()
                            .WithId("APR")
                            .WithName("APR")
                            .WithPositionExtracted()))
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .WithId(new SignatureId("Agent"))
                        .WithName("Agent")
                        .WithPositionExtracted()))
                .Build();

            packageId = eslClient.CreatePackageOneStep(superDuperPackage);
            eslClient.SendPackage(packageId);
        }
    }
}

