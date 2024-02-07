using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SignerVerificationExample : SDKSample
    {
        public readonly string CREATE_VERIFICATION_TYPE_ID = "DIGIPASS";
        public readonly string CREATE_VERIFICATION_PAYLOAD  = "bSxW5aAFG2yTW5NaqaAF";
        public readonly string UPDATE_VERIFICATION_TYPE_ID = "personalCertificateSigning";
        public readonly string UPDATE_VERIFICATION_PAYLOAD  = "";

        public SignerVerification signerVerificationToBeCreated;
        public SignerVerification signerVerificationToBeUpdated;
        public SignerVerification retrievedSignerVerificationAfterCreate;
        public SignerVerification retrievedSignerVerificationAfterUpdate;
        public SignerVerification retrievedSignerVerificationAfterDelete;


        public static void Main(string[] args)
        {
            new SignerVerificationExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John1")
                    .WithLastName("Smith1"))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = ossClient.CreatePackage( superDuperPackage );
            retrievedPackage = ossClient.GetPackage(packageId);
            Signer signer = retrievedPackage.GetSigner(email1);

            // Create
            signerVerificationToBeCreated = SignerVerificationBuilder
                .NewSignerVerification(CREATE_VERIFICATION_TYPE_ID)
                .WithPayload(CREATE_VERIFICATION_PAYLOAD)
                .Build();
            ossClient.CreateSignerVerification(packageId, signer.Id, signerVerificationToBeCreated);
            retrievedSignerVerificationAfterCreate = ossClient.GetSignerVerification(packageId, signer.Id);

            // Update
            signerVerificationToBeUpdated = SignerVerificationBuilder
                .NewSignerVerification(UPDATE_VERIFICATION_TYPE_ID)
                .WithPayload(UPDATE_VERIFICATION_PAYLOAD)
                .Build();

            ossClient.UpdateSignerVerification(packageId, signer.Id, signerVerificationToBeUpdated);
            retrievedSignerVerificationAfterUpdate = ossClient.GetSignerVerification(packageId, signer.Id);

            // Delete
            ossClient.DeleteSignerVerification(packageId, signer.Id);
            retrievedSignerVerificationAfterDelete = ossClient.GetSignerVerification(packageId, signer.Id);
        }
    }
}

