using System;
using System.IO;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignableSignaturesExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignableSignaturesExample(Props.GetInstance()).Run();
        }

        private Stream fileStream1;
        public DocumentPackage sentPackage;

        private string signer1Id = "signer1Id";
        private string signer2Id = "signer2Id";
        private string documentId = "documentId";
        private string DOCUMENT_NAME = "First Document";

        public string email1;
        public string email2;
        public IList<Signature> signer1SignableSignatures, signer2SignableSignatures;

        public SignableSignaturesExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email"))
        {
        }

        public SignableSignaturesExample(string apiKey, string apiUrl, string email1, string email2) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignableSignaturesExample: " + DateTime.Now)
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

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            sentPackage = eslClient.GetPackage(packageId);

            signer1SignableSignatures = eslClient.ApprovalService.GetAllSignableSignatures(sentPackage, documentId, signer1Id);
            signer2SignableSignatures = eslClient.ApprovalService.GetAllSignableSignatures(sentPackage, documentId, signer2Id);
        }
    }
}

