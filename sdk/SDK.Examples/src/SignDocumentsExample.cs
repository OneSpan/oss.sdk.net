using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignDocumentsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignDocumentsExample(Props.GetInstance()).Run();
        }

        private string senderEmail, email1;
        private Stream fileStream1, fileStream2;
        public DocumentPackage retrievedPackageBeforeSigning, retrievedPackageAfterSigningApproval1, retrievedPackageAfterSigningApproval2;

        private string document1Name = "First Document";
        private string document2Name = "Second Document";
        private string signer1Id = "signer1";

        public SignDocumentsExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("sender.email"), props.Get("1.email"))
        {
        }

        public SignDocumentsExample(string apiKey, string apiUrl, string senderEmail, string email1) : base(apiKey, apiUrl)
        {
            this.senderEmail = senderEmail;
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignDocumentsExample: " + DateTime.Now)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("signer1")
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(document1Name)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(senderEmail)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(300, 100)))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(document2Name)
                                  .FromStream(fileStream2, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(senderEmail)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(300, 100)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            retrievedPackageBeforeSigning = eslClient.GetPackage(packageId);

//            eslClient.SignDocument(packageId, document1Name);

            eslClient.SignDocuments(packageId);
            retrievedPackageAfterSigningApproval1 = eslClient.GetPackage(packageId);

            eslClient.SignDocuments(packageId, signer1Id);
            retrievedPackageAfterSigningApproval2 = eslClient.GetPackage(packageId);
        }
    }
}

