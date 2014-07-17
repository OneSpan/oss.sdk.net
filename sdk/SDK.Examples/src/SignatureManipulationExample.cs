using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SignatureManipulationExample : SDKSample
    {
        public string email1;
        public string email2;
        public string email3;
        private Stream fileStream;

        private string documentId = "documentId";

        public Signature signature1;
        public Signature signature2;
        public Signature signature3;
        public Signature updatedSignature;

        public List<Signature> addedSignatures;
        public List<Signature> deletedSignatures;
        public List<Signature> updatedSignatures;


        public DocumentPackage createdPackage;

        public SignatureManipulationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email"))
        {
        }

        public SignatureManipulationExample(string apiKey, string apiUrl, string email1, string email2, string email3) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("SignatureManipulationExample: " + DateTime.Now)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("signatureId1")
                                .WithFirstName("firstName1")
                                .WithLastName("lastName1")
            )
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithCustomId("signatureId2")
                                .WithFirstName("firstName2")
                                .WithLastName("lastName2")
            )
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                                .WithCustomId("signatureId3")
                                .WithFirstName("firstName3")
                                .WithLastName("lastName3")
            )
                    .WithDocument(DocumentBuilder.NewDocumentNamed("SignatureManipulationExample")
                                  .WithId("documentId")
                                  .FromStream(fileStream, DocumentType.PDF)
            )

                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);

            signature1 = SignatureBuilder.SignatureFor(email1)
                .OnPage(0)
                    .WithId(new SignatureId("signatureId1"))
                    .AtPosition(100, 100)
                    .Build();

            signature2 = SignatureBuilder.SignatureFor(email2)
                .OnPage(0)
                    .WithId(new SignatureId("signatureId2"))
                    .AtPosition(100, 200)
                    .Build();

            signature3 = SignatureBuilder.SignatureFor(email3)
                .OnPage(0)
                    .WithId(new SignatureId("signatureId3"))
                    .AtPosition(100, 300)
                    .Build();

            updatedSignature = SignatureBuilder.SignatureFor(email1)
                .OnPage(0)
                    .WithId(new SignatureId("updatedId"))
                    .AtPosition(200, 400)
                    .Build();

            // Adding the signatures
            createdPackage = eslClient.GetPackage(packageId);
            eslClient.ApprovalService.AddApproval(createdPackage, documentId, signature1);
            eslClient.ApprovalService.AddApproval(createdPackage, documentId, signature2);
            eslClient.ApprovalService.AddApproval(createdPackage, documentId, signature3);
            addedSignatures = eslClient.GetPackage(packageId).Documents["SignatureManipulationExample"].Signatures;

            // Deleting signature for signer 1
            eslClient.ApprovalService.DeleteApproval(packageId, "documentId", "signatureId1");
            deletedSignatures = eslClient.GetPackage(packageId).Documents["SignatureManipulationExample"].Signatures;

            // Updating the information for the third signature
            createdPackage = eslClient.GetPackage(packageId);
            eslClient.ApprovalService.ModifyApproval(createdPackage, "documentId", "signatureId3", updatedSignature);
            updatedSignatures = eslClient.GetPackage(packageId).Documents["SignatureManipulationExample"].Signatures;
        }
    }
}

