using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class DocumentOperationsExample : SDKSample
	{
        public DocumentPackage BuiltPackage{ get; set; }
        public DocumentPackage RetrievedPackageWithNewDocument{ get; set; }
        public Document RetrievedUpdatedDocument{ get; set; }
        public DocumentPackage RetrievedPackageWithUpdatedDocument{ get; set; }
        public DocumentPackage RetrievedPackageWithDeletedDocument{ get; set; }
        
        public const string OriginalDocumentName = "Original Document Name";
        public const string UpdatedDocumentName = "Updated Document Name";
        
        public const string OriginalDocumentDescription = "Original Document Description";
        public const string UpdatedDocumentDescription = "Updated Document Description";
    
        public static void Main(string[] args)
        {
            new DocumentOperationsExample().Run();
        }

		override public void Execute()
		{
			// 1. Create a package
            BuiltPackage = PackageBuilder.NewPackageNamed(PackageName)
				.DescribedAs("This is a package demonstrating document upload")
			    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
      					.WithFirstName("John")
      					.WithLastName("Smith")
      					.WithTitle("Managing Director")
      					.WithCompany("Acme Inc."))
  				.Build();

            PackageId packageId = ossClient.CreatePackage(BuiltPackage);
			Console.WriteLine("package created, id = " + packageId);
            
            retrievedPackage = ossClient.GetPackage(packageId);

			// 2. Construct a document
            Signature signature = SignatureBuilder.SignatureFor("john.smith@email.com")
                        .OnPage(0)
                        .AtPosition(100,100)
                        .Build();
			Document document = DocumentBuilder.NewDocumentNamed( OriginalDocumentName )
                .WithDescription( OriginalDocumentDescription )
                .FromStream( fileStream1, DocumentType.PDF )
				.WithSignature(signature)                                
				.Build();

			// 3. Attach the document to the created package by uploading the document.
            document = ossClient.UploadDocument(document, packageId);
			Console.WriteLine("Document was uploaded");
            
            RetrievedPackageWithNewDocument = ossClient.GetPackage(packageId);

            //This is how you would update and get a document's metadata (name, description, approvals, fields)
			document.Name = UpdatedDocumentName;
			document.Description = UpdatedDocumentDescription;
            document.Signatures.Add(SignatureBuilder.SignatureFor("john.smith@email.com")
                        .OnPage(0)
                        .AtPosition(200,200)
                        .Build());

			ossClient.PackageService.UpdateDocumentMetadata(RetrievedPackage, document);
			Console.WriteLine("Document was updated");

            RetrievedUpdatedDocument = ossClient.PackageService.GetDocumentMetadata(RetrievedPackage, document.Id);
            RetrievedPackageWithUpdatedDocument = ossClient.GetPackage(packageId);

			//This is how you would delete a document from a package
			ossClient.PackageService.DeleteDocument(packageId, document.Id);

            RetrievedPackageWithDeletedDocument = ossClient.GetPackage(packageId);
		}
	}
}