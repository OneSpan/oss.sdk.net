using System;
using System.IO;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    /// <summary>
    /// Example of how to configure the SMS authentication method for a signer
    /// </summary>
    public class SignerSMSAuthenticationExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerSMSAuthenticationExample(Props.GetInstance()).Run();
        }

        public string email1;
        public string sms1;
        private Stream fileStream1;

        public SignerSMSAuthenticationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("1.sms"))
        {
        }

        public SignerSMSAuthenticationExample(string apiKey, string apiUrl, string email1, string sms1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.sms1 = sms1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignerSMSAuthenticationExample: " + DateTime.Now)
                .DescribedAs("This is a SMS authentication example")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithSMSSentTo(sms1))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(199, 100)))
                .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);

            eslClient.SendPackage(packageId);
        }
    }
}

