using System;
using Silanis.ESL.SDK;
using System.IO;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ConsentExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new ConsentExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private Stream fileStream1;
        private Stream fileStream2;

        public ConsentExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email")) {
        }

        public ConsentExample( String apiKey, String apiUrl, String email1, String email2 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# ConsentExample " + DateTime.Now)
                .DescribedAs ("This is a package created using the e-SignLive SDK")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName("John2")
                                .WithLastName("Smith2"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed( "Custom Consent Document" )
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.AcceptanceFor( email1 ) ) )
                    .WithDocument(DocumentBuilder.NewDocumentNamed( "Regular Document" )
                                  .FromStream(fileStream2, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build ();

            packageId = eslClient.CreatePackage (package);
            eslClient.SendPackage(packageId);

            DocumentPackage retrievedPackage = eslClient.GetPackage(packageId);
            Console.WriteLine("Document retrieved = " + retrievedPackage.Id);
        }
    }
}

