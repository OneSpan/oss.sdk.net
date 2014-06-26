using System;
using Silanis.ESL.SDK;
using System.IO;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class DocumentRetrievalExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentRetrievalExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;
        private byte[] pdfDocumentBytes, originalPdfDocumentBytes;

        public DocumentRetrievalExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public DocumentRetrievalExample(String apiKey, String apiUrl, String email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        public byte[] PdfDocumentBytes
        {
            get
            {
                return pdfDocumentBytes;
            }
        }

        public byte[] OriginalPdfDocumentBytes
        {
            get
            {
                return originalPdfDocumentBytes;
            }
        }

        override public void Execute()
        {
            string documentId = "myDocumentId";
            DocumentPackage package = PackageBuilder.NewPackageNamed("DocumentRetrievalExample " + DateTime.Now)
                .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithId(documentId)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            PackageId packageId = eslClient.CreatePackage(package);
            eslClient.SendPackage(packageId);

            pdfDocumentBytes = eslClient.DownloadDocument(packageId, documentId);
            originalPdfDocumentBytes = eslClient.DownloadOriginalDocument(packageId, documentId);

            // To write the byte[] to a file, use:
            // System.IO.File.WriteAllBytes("/path/to/directory/myDocument.pdf", pdfDocumentBytes);
        }
    }
}
