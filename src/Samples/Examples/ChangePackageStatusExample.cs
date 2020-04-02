using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class ChangePackageStatusExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ChangePackageStatusExample().Run();
        }

        public DocumentPackage sentPackage, trashedPackage, restoredPackage;

        public readonly string DOCUMENT_NAME = "First Document";

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);
            sentPackage = ossClient.GetPackage(packageId);
            ossClient.ChangePackageStatusToDraft(packageId);
            retrievedPackage = ossClient.GetPackage( packageId );
            ossClient.PackageService.Trash(packageId);
            trashedPackage = ossClient.GetPackage(packageId);
            ossClient.PackageService.Restore(packageId);
            restoredPackage = ossClient.GetPackage(packageId);
        }
    }
}

