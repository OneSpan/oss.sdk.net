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
        private Stream fileStream1;
        public readonly string DOCUMENT_NAME = "My Document";

        public DocumentExtractionExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public DocumentExtractionExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document-with-fields.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed("C# DocumentExtractionExample " + DateTime.Now)
                .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("Signer1")
                                .WithCustomId("Signer1")
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .EnableExtraction() )
                    .Build();

            packageId = eslClient.CreatePackage(package);
            eslClient.SendPackage(packageId);

            DocumentPackage sentPackage = eslClient.GetPackage(packageId);
            Console.WriteLine("Document sent = " + sentPackage.Id);
        }
    }
}

