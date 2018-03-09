using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DocumentUploadExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentUploadExample().Run();
        }

        public readonly string DOCUMENT1_NAME = "First Document";
        public readonly string DOCUMENT2_NAME = "Second Document";

        public Document document1, document2;
        public IList<Document> uploadedDocuments;

        override public void Execute()
        {

            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/taggedDocument.pdf").FullName);

            // 1. Create a package
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using the eSignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(1))
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("Client1")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithTitle("Managing Director")
                                .WithCompany("Acme Inc."))
                    .Build();
            packageId = eslClient.CreatePackage(superDuperPackage);
            superDuperPackage.Id = packageId;

            // 2. Construct documents
            document1 = DocumentBuilder.NewDocumentNamed(DOCUMENT1_NAME)
                                .FromStream(fileStream1, DocumentType.PDF)
                                .WithSignature(SignatureBuilder.SignatureFor(email1)
                                .OnPage(0)
                                .WithField(FieldBuilder.CheckBox()
                                .OnPage(0)
                                .AtPosition(400, 200)
                                .WithValue(FieldBuilder.CHECKBOX_CHECKED))
                                .AtPosition(100, 100))
                    .Build();

            document2 = DocumentBuilder.NewDocumentNamed(DOCUMENT2_NAME)
                .FromStream(fileStream2, DocumentType.PDF)
                .WithSignature(SignatureBuilder.SignatureFor(email1)
                    .OnPage(0)
                    .WithField(FieldBuilder.CheckBox()
                        .OnPage(0)
                        .AtPosition(400, 200)
                        .WithValue(FieldBuilder.CHECKBOX_CHECKED))
                    .AtPosition(100, 100))
                .Build();

            // 3. Upload the documents to the created package by uploading the document.
            uploadedDocuments = eslClient.UploadDocuments(packageId, document1, document2);

            eslClient.SendPackage(packageId);
            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

