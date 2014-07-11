using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignatureManipulationExample : SDKSample
    {
        private string email1;
        private string email2;
        private Stream fileStream;

        public SignatureManipulationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email"))
        {
        }

        public SignatureManipulationExample(string apiKey, string apiUrl, string email1, string email2, string email3) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("SignatureManipulationExample: " + DateTime.Now)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("Client1")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithTitle("Managing Director")
                                .WithCompany("Acme Inc.")
            )
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName("Patty")
                                .WithLastName("Galant")
            )
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .WithId("documentId")
                                  .FromStream(fileStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .WithId(new SignatureId("signatureId1"))
                                   .WithField(FieldBuilder.CheckBox()
                               .OnPage(0)
                               .AtPosition(400, 200)
                               .WithValue(FieldBuilder.CHECKBOX_CHECKED)
            )
                                   .AtPosition(100, 100)
            )
                                  .WithSignature(SignatureBuilder.SignatureFor(email2)
                                   .WithId(new SignatureId("signatureId2"))
                                   .OnPage(0)
                                   .AtPosition(100, 300)
            )
            )

                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.ApprovalService.DeleteApproval(packageId, "documentId", "signatureId1");
            eslClient.SendPackage(packageId);

        }
    }
}

