using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class DocumentExtractionExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new DocumentExtractionExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private string email3;
        private Stream fileStream1;
        public readonly string DOCUMENT_NAME = "My Document";

        public DocumentExtractionExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email")) {
        }

        public DocumentExtractionExample( string apiKey, string apiUrl, string email1, string email2, string email3 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/extract_document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed("C# DocumentExtractionExample " + DateTime.Now)
                .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName("John2")
                                .WithLastName("Smith2"))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                                .WithFirstName("John3")
                                .WithLastName("Smith3"))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .EnableExtraction() )
                    .Build();

            packageId = eslClient.CreatePackage(package);
            eslClient.SendPackage(packageId);

            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

