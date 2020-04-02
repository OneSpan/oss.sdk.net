using System;
using OneSpanSign.Sdk;
using System.IO;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class DocumentDeliveryByEmailExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";


        public static void Main (string[] args)
        {
            // Create new oss client with api token and base url
            OssClient client = new OssClient (apiToken, baseUrl);
            FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf");

            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# DocumentDeliveryByEmailExample Package " + DateTime.Now)
                .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
                                .DeliverSignedDocumentsByEmail()
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromFile(file.FullName)
                                  .WithSignature(SignatureBuilder.CaptureFor ("john.smith@email.com")
                                   .OnPage (0)
                                   .AtPosition (500, 300)))
                    .Build ();

            PackageId id = client.CreatePackage (package);

            client.SendPackage(id);
        }
    }
}

