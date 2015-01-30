using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class MixingSignatureAndAcceptanceOnOnedocumentExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new MixingSignatureAndAcceptanceOnOnedocumentExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private Stream fileStream1;

        public readonly string DOCUMENT_NAME = "First Document";

        public MixingSignatureAndAcceptanceOnOnedocumentExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email"))
        {
        }

        public MixingSignatureAndAcceptanceOnOnedocumentExample(string apiKey, string apiUrl, string email1, string email2) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("MixingSignatureAndAcceptanceOnOnedocumentExample: " + DateTime.Now)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName("John2")
                                .WithLastName("Smith2"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.AcceptanceFor(email2)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

