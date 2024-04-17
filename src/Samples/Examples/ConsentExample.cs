using System;
using OneSpanSign.Sdk;
using System.IO;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class ConsentExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new ConsentExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
                .DescribedAs ("This is a package created using OneSpan Sign SDK")
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

            packageId = ossClient.CreatePackage (package);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);

            Console.WriteLine("Document retrieved = " + retrievedPackage.Id);
        }
    }
}

