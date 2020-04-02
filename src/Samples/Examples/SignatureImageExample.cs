using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SignatureImageExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignatureImageExample().Run();
        }

        public DocumentPackage sentPackage;

        override public void Execute()
        {
            Signer signer1 = SignerBuilder.NewSignerWithEmail(email1)
                .WithCustomId("signer1")
                .WithFirstName("John1")
                .WithLastName("Smith1").Build();

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(signer1)
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);

            ossClient.SignatureImageService.GetSignatureImageForSender(senderUID, SignatureImageFormat.GIF);
            ossClient.SignatureImageService.GetSignatureImageForPackageRole(packageId, signer1.Id, SignatureImageFormat.JPG);
        }
    }
}

