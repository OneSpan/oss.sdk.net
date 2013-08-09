using System;
using Silanis.ESL.SDK;
using System.IO;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ConsentExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

        public static void Main (string[] args)
        {
            createPackage();
        }

        public static void createPackage()
        {
            // Create new esl client with api token and base url
            EslClient client = new EslClient (apiToken, baseUrl);
            FileInfo file = new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf");

            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# Package " + DateTime.Now)
                .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithId("SIGNER1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed( "Custom Consent Document" )
                                  .FromFile(Directory.GetCurrentDirectory() + "/src/document.pdf")
                                  .WithSignature(SignatureBuilder.AcceptanceFor( "john.smith@email.com" ) ) )
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromFile(file.FullName)
                                  .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build ();

            PackageId id = client.CreatePackage (package);
            client.SendPackage(id);

            package = client.GetPackage(id);

            Console.WriteLine("Package was created and sent", id.Id);
        }
    }
}

