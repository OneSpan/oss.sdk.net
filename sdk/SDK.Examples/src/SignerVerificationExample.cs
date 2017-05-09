using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignerVerificationExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerVerificationExample().Run();
        }

        public DocumentPackage createdPackage, updatedPackage;
        public string firstVerificationType, deletedVerificationType;

        public readonly string CERTIFICATE = "personalCertificateSigning";

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using the eSignLive SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John1")
                    .WithLastName("Smith1")
                    .WithSignerVerification(CERTIFICATE))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = eslClient.CreatePackage( superDuperPackage );
            createdPackage = eslClient.GetPackage( packageId );

            Signer signer = createdPackage.GetSigner(email1);
            firstVerificationType = signer.VerificationType;

            signer.VerificationType = "";
            eslClient.UpdatePackage(packageId, createdPackage);
            updatedPackage = eslClient.GetPackage( packageId );

            signer = updatedPackage.GetSigner(email1);
            deletedVerificationType = signer.VerificationType;
        }
    }
}

