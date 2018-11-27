using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DocumentsDeleteExample : SDKSample
    {
        public DocumentPackage RetrievedPackageWithDocuments { get; set; }
        public DocumentPackage RetrievedPackageWithDeletedDocuments { get; set; }

        public const string DocumentName1 = "document name1";
        public const string DocumentName2 = "document name2";

        public static void Main (string [] args)
        {
            new DocumentOperationsExample ().Run ();
        }

        override public void Execute ()
        {
            // 1. Construct a documents

            Document document1 = DocumentBuilder.NewDocumentNamed (DocumentName1)
                .WithId ("id1")
                .WithDescription ("description1")
                .FromStream (fileStream1, DocumentType.PDF)
                .Build ();

            Document document2 = DocumentBuilder.NewDocumentNamed (DocumentName2)
                .WithId ("id2")
                .WithDescription ("description2")
                .FromStream (fileStream2, DocumentType.PDF)
                .Build ();

            // 2. Create a package
            DocumentPackage BuiltPackage = PackageBuilder.NewPackageNamed (PackageName)
                .DescribedAs ("This is a package demonstrating document upload")
                .WithSigner (SignerBuilder.NewSignerWithEmail ("john.smith@email.com")
                          .WithFirstName ("John")
                          .WithLastName ("Smith")
                          .WithTitle ("Managing Director")
                          .WithCompany ("Acme Inc."))
                .WithDocument (document1)
                .WithDocument (document2)
                .Build ();

            PackageId packageId = eslClient.CreatePackage (BuiltPackage);
            Console.WriteLine ("package created, id = " + packageId);

            RetrievedPackageWithDocuments = eslClient.GetPackage (packageId);

            //This is how you would delete a document from a package
            IList<Document> documents = new List<Document> ()
            {
                document1,
                document2
            };
            eslClient.PackageService.DeleteDocuments (packageId, documents);

            RetrievedPackageWithDeletedDocuments = eslClient.GetPackage (packageId);
        }
    }

}
