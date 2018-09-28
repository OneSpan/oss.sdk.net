using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignDocumentsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignDocumentsExample().Run();
        }

        public DocumentPackage retrievedPackageBeforeSigning, retrievedPackageAfterSigningApproval1, retrievedPackageAfterSigningApproval2;

        private string document1Name = "First Document";
        private string document2Name = "Second Document";
        private string signer1Id = "signer1";

        override public void Execute()
        {
            CapturedSignature capturedSignature = new CapturedSignature ("AQAAAIPGDPtxqL+RsL7/w/7eEX+cAtwAAwADAFAAAAADAAAAnALcACMAAAACq5ZQg105VH9Z/1l+UM9QF3A0v3BEv2BmYYFgSGAYQBZAJkA0QDVAREBmQENAQ0BRUJFQg1CDUFKbQENASUBGQERFUA==");

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId(signer1Id)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(document1Name)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.CaptureFor(senderEmail)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(400, 100)))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(document2Name)
                                  .FromStream(fileStream2, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(senderEmail)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.CaptureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(400, 100)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            retrievedPackageBeforeSigning = eslClient.GetPackage(packageId);

            eslClient.SignDocuments(packageId, capturedSignature);
            retrievedPackageAfterSigningApproval1 = eslClient.GetPackage(packageId);

            eslClient.SignDocuments(packageId, signer1Id, capturedSignature);
            retrievedPackageAfterSigningApproval2 = eslClient.GetPackage(packageId);
        }
    }
}

