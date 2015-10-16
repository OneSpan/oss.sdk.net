using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignatureImageExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignatureImageExample(Props.GetInstance()).Run();
        }

        private Stream fileStream1;
        public DocumentPackage sentPackage;

        public string email1;
        public string senderUID;
        private readonly string acceptType = "image/jpeg";

        public SignatureImageExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public SignatureImageExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.senderUID = Converter.apiKeyToUID(apiKey);;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            Signer signer1 = SignerBuilder.NewSignerWithEmail(email1)
                .WithCustomId("signer1")
                .WithFirstName("John1")
                .WithLastName("Smith1").Build();

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignatureImageExample: " + DateTime.Now)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(signer1)
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);

            eslClient.SignatureImageService.GetSignatureImageForSender(senderUID, acceptType);
            eslClient.SignatureImageService.GetSignatureImageForPackageRole(packageId, signer1.Id, acceptType);
        }
    }
}

