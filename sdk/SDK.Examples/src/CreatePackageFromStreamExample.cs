using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CreatePackageFromStreamExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new CreatePackageFromStreamExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public CreatePackageFromStreamExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email")) {
        }

        public CreatePackageFromStreamExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed("C# CreatePackageFromStream " + DateTime.Now)
                    .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            PackageId id = eslClient.CreatePackage(package);
            eslClient.SendPackage(id);
        }
    }
}