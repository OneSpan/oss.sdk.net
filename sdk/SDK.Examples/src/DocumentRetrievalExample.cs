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
        private byte[] pdfDocumentBytes, originalPdfDocumentBytes, zippedDocumentsBytes;

        public DocumentRetrievalExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public DocumentRetrievalExample(String apiKey, String apiUrl, String email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/prêt.pdf").FullName);
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

        public byte[] ZippedDocumentsBytes
        {
            get
            {
                return zippedDocumentsBytes;
            }
        }

        override public void Execute()
        {
            String docId = "myDocumentId";
            PackageId package = eslClient.CreatePackageOneStep(PackageBuilder.NewPackageNamed("DocumentRetrievalExample " + DateTime.Now)
                        .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName("George")
                            .WithLastName("Faltour").Build())
                            .WithDocument(DocumentBuilder.NewDocumentNamed("prêt.pdf")
                            .FromStream(fileStream1, DocumentType.PDF)
                            .WithId(docId)
                            .WithSignature(SignatureBuilder.SignatureFor(email1)
                           .    AtPosition(100, 100).OnPage(0))
                          ).Build());

            eslClient.SendPackage(package);

            pdfDocumentBytes = eslClient.DownloadDocument(package, docId);  
            originalPdfDocumentBytes = eslClient.DownloadOriginalDocument(package, docId);
            zippedDocumentsBytes = eslClient.DownloadZippedDocuments(package);

            // To write the byte[] to a file, use:
            // System.IO.File.WriteAllBytes("/path/to/directory/myDocument.pdf", pdfDocumentBytes);
        }
    }
}
