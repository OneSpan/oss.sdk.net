using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ChangePackageStatusExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ChangePackageStatusExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;
        public DocumentPackage sentPackage;

        public readonly string DOCUMENT_NAME = "First Document";

        public ChangePackageStatusExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public ChangePackageStatusExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("ChangePackageStatusExample: " + DateTime.Now)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            sentPackage = eslClient.GetPackage(packageId);
            eslClient.ChangePackageStatusToDraft(packageId);
            retrievedPackage = eslClient.GetPackage( packageId );
        }
    }
}

