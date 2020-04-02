using System;
using System.IO;
using OneSpanSign.Sdk;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SignableSignaturesExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignableSignaturesExample().Run();
        }

        public DocumentPackage sentPackage;

        private string signer1Id = "signer1Id";
        private string signer2Id = "signer2Id";
        private string documentId = "documentId";
        private string DOCUMENT_NAME = "First Document";

        public IList<Signature> signer1SignableSignatures, signer2SignableSignatures;

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithFirstName("John1")
                        .WithLastName("Smith1")
                        .WithCustomId(signer1Id))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                        .WithFirstName("John2")
                        .WithLastName("Smith2")
                        .WithCustomId(signer2Id))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                        .FromStream(fileStream1, DocumentType.PDF)
                        .WithId(documentId)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                              .OnPage(0)
                              .AtPosition(100, 100))
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                              .OnPage(0)
                              .AtPosition(300, 100))
                        .WithSignature(SignatureBuilder.SignatureFor(email2)
                              .OnPage(0)
                              .AtPosition(500, 100)))
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);
            sentPackage = ossClient.GetPackage(packageId);

            signer1SignableSignatures = ossClient.ApprovalService.GetAllSignableSignatures(sentPackage, documentId, signer1Id);
            signer2SignableSignatures = ossClient.ApprovalService.GetAllSignableSignatures(sentPackage, documentId, signer2Id);
        }
    }
}

