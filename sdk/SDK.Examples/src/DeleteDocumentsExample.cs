using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DeleteDocumentsExample : SDKSample
    {
        public DocumentPackage RetrievedPackageWithDeletedDocuments { get; set; }

        public const string DOCUMENT1_NAME = "First Document";
        public const string DOCUMENT2_NAME = "Second Document";

        public static void Main (string [] args)
        {
            new DeleteDocumentsExample ().Run ();
        }

        override public void Execute ()
        {

            // Create a package with documents
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed (PackageName)
                .DescribedAs ("This is a package demonstrating document upload")
                .WithSigner (SignerBuilder.NewSignerWithEmail (email1)
                    .WithFirstName ("John")
                    .WithLastName ("Smith")
                    .WithTitle ("Managing Director")
                    .WithCompany ("Acme Inc."))
                .WithDocument (DocumentBuilder.NewDocumentNamed (DOCUMENT1_NAME)
                    .WithId ("id1")
                    .WithDescription ("description1")
                    .FromStream (fileStream1, DocumentType.PDF)
                    .Build ())
                .WithDocument (DocumentBuilder.NewDocumentNamed (DOCUMENT2_NAME)
                    .WithId ("id2")
                    .WithDescription ("description2")
                    .FromStream (fileStream2, DocumentType.PDF)
                    .Build ())
                .Build ();

            packageId = eslClient.CreatePackage (superDuperPackage);

            retrievedPackage = eslClient.GetPackage (packageId);

            //This is how you would delete a document from a package
            eslClient.PackageService.DeleteDocuments (packageId, retrievedPackage.GetDocument(DOCUMENT1_NAME).Id, 
                                                      retrievedPackage.GetDocument (DOCUMENT2_NAME).Id);

            RetrievedPackageWithDeletedDocuments = eslClient.GetPackage (packageId);
        }
    }

}
