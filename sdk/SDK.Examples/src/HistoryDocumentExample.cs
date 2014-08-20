using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    /// <summary>
    /// The HistoryDocumentExample class provides an example to help adding a document from an external provider 
    /// which is the history list of documents uploaded. However, most external providers require pre-development
    /// configurations.
    /// Please contact us for more information
    /// </summary>

    public class HistoryDocumentExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new HistoryDocumentExample(Props.GetInstance()).Run();
        }

        public string email1;
        public string email2;
        public string externalDocumentName;
        private Stream fileStream1;
        private Stream fileStream2;

        public HistoryDocumentExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"))
        {
        }

        public HistoryDocumentExample(string apiKey, string apiUrl, string email1, string email2) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            externalDocumentName = "External Document " + DateTime.Now;

            DocumentPackage superDuperPackage =
                    PackageBuilder.NewPackageNamed("ExternalPackage: " + DateTime.Now)
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
                        .WithDocument(DocumentBuilder.NewDocumentNamed(externalDocumentName)
                                      .FromStream(fileStream1, DocumentType.PDF)
                                      .WithSignature(SignatureBuilder.SignatureFor(email1)
                                       .OnPage(0)
                                       .AtPosition(100, 100)
            )
            )
                        .Build();

            packageId = eslClient.CreatePackageOneStep(superDuperPackage);
            eslClient.SendPackage(packageId);

            DocumentPackage documentWithExternalContent =
                PackageBuilder.NewPackageNamed("HistoryDocumentExample: " + DateTime.Now)
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
                                  .FromStream(fileStream2, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email2)
                                   .OnPage(0)
                                   .AtPosition(100, 100)
                                   )
                                  )
                    .Build();

            packageId = eslClient.CreatePackageOneStep(superDuperPackage);

            IList<Silanis.ESL.SDK.Document> documentsHistory = eslClient.PackageService.GetDocuments();
            IList<Silanis.ESL.SDK.Document> externalDocuments = new List<Silanis.ESL.SDK.Document>();

            foreach (Silanis.ESL.SDK.Document document in documentsHistory)
            {
                if (document.Name == externalDocumentName)
                {
                    externalDocuments.Add(document);
                }
            }

            eslClient.PackageService.AddDocumentWithExternalContent(packageId.Id, externalDocuments);
        }
    }
}

