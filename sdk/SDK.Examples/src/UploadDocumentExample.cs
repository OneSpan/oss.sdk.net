using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    class UploadDocumentExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

        public static void Main(string[] args)
        {
            EslClient client = new EslClient(apiToken, baseUrl);
            FileInfo file = new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf");

            // 1. Create a package
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("Policy " + DateTime.Now)
                .DescribedAs("This is a package demonstrating document upload")
                    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithTitle("Managing Director")
                                .WithCompany("Acme Inc."))
                    .Build();

            superDuperPackage.Id = client.CreatePackage(superDuperPackage);

            // 2. Construct a document
            Document document = DocumentBuilder.NewDocumentNamed("First Document")
                                .FromFile(file.FullName)
                                .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
                                    .OnPage(0))                                
                                .Build();

            // 3. Attach the document to the created package by uploading the document.
            client.UploadDocument(document.FileName, document.Content, document, superDuperPackage);

            Console.WriteLine("Document was uploaded");
        }
    }
}