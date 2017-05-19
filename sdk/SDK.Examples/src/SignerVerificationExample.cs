using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignerVerificationExample : SDKSample
    {
        public readonly string VERIFICATION_TYPE = "DIGIPASS";
        public readonly string VERIFICATION_PAYLOAD  = "bSxW5aAFG2yTW5NaqaAF";

        public SignerVerification RetrievedSignerVerification;

        public static void Main(string[] args)
        {
            new SignerVerificationExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using the eSignLive SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John1")
                    .WithLastName("Smith1"))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = eslClient.CreatePackage( superDuperPackage );
            retrievedPackage = eslClient.GetPackage(packageId);

            Signer signer = retrievedPackage.GetSigner(email1);
            SignerVerification signerVerification = SignerVerificationBuilder.SignerVerificationFor(VERIFICATION_TYPE)
                .WithPayload(VERIFICATION_PAYLOAD)
                .Build();

            eslClient.CreateSignerVerification(packageId, signer.Id, signerVerification);

            RetrievedSignerVerification = eslClient.GetSignerVerification(packageId, signer.Id);
        }
    }
}

