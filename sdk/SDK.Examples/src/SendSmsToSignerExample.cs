using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SendSmsToSignerExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SendSmsToSignerExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private string sms1;
        private string sms2;
        private Stream fileStream1;

        public readonly string SIGNER1_FIRST = "John";
        public readonly string SIGNER1_LAST = "Smith";
        public readonly string SIGNER2_FIRST = "Patty";
        public readonly string SIGNER2_LAST = "Galant";
        public readonly string DOCUMENT_NAME = "First Document";

        public SendSmsToSignerExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email"), props.Get("1.sms"), props.Get("2.sms"))
        {
        }

        public SendSmsToSignerExample(string apiKey, string apiUrl, string email1, string email2, string sms1, string sms2) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.email2 = email2;
            this.sms1 = sms1;
            this.sms2 = sms2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SendSmsToSignerExample: " + DateTime.Now)
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName(SIGNER1_FIRST)
                                .WithLastName(SIGNER1_LAST)
                                .WithSMSSentTo(sms1))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName(SIGNER2_FIRST)
                                .WithLastName(SIGNER2_LAST)
                                .WithSMSSentTo(sms2))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(400, 100)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            retrievedPackage = eslClient.GetPackage(packageId);

            eslClient.PackageService.SendSmsToSigner(packageId, retrievedPackage.GetSigner(email1));
            eslClient.PackageService.SendSmsToSigner(packageId, retrievedPackage.GetSigner(email2));

            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

