using System;
using Silanis.ESL.SDK;
using System.IO;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class DocumentRetrievalExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

        private static EslClient eslClient;
        private static PackageId packageId;
        private static string documentId = "myDocumentId";


        public static void Main (string[] args)
        {
            // Create new esl client with api token and base url
            eslClient = new EslClient (apiToken, baseUrl);
            FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");


            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# Package " + DateTime.Now)
                .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromFile(file.FullName)
                                  .WithId(documentId)
                                  .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
                                   .OnPage(0)
                                   .AtPosition (500, 300)))
                    .Build ();

            packageId = eslClient.CreatePackage (package);

            eslClient.SendPackage(packageId);

            downloadDocumentById();
        }

        public static void downloadDocumentById() {
            byte[] documentBinary = eslClient.DownloadDocument( packageId, documentId );
            System.IO.File.WriteAllBytes("myDocument.pdf", documentBinary);
        }

        public static void downloadDocumentsAsZip() {
            byte[] documentsZipBinary = eslClient.DownloadZippedDocuments( packageId );
            System.IO.File.WriteAllBytes("myDocumentArchive.zip", documentsZipBinary);
        }
    }
}
