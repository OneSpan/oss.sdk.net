using System;
using OneSpanSign.Sdk;
using System.IO;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class DocumentRetrievalExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentRetrievalExample().Run();
        }

        public byte[] pdfDownloadedBytes, originalPdfDownloadedBytes, zippedDownloadedBytes;

        override public void Execute()
        {
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf").FullName);

            string docId = "myDocumentId";
            var superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                            .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName("George")
                            .WithLastName("Faltour").Build())
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                            .WithId(docId)
                            .WithSignature(SignatureBuilder.SignatureFor(email1)
                           .    AtPosition(100, 100).OnPage(0))
                          ).Build();

            var packageId = ossClient.CreatePackageOneStep(superDuperPackage);

            ossClient.SendPackage(packageId);

            pdfDownloadedBytes = ossClient.DownloadDocument(packageId, docId);  
            originalPdfDownloadedBytes = ossClient.DownloadOriginalDocument(packageId, docId);
            zippedDownloadedBytes = ossClient.DownloadZippedDocuments(packageId);

            // To write the byte[] to a file, use:
            // System.IO.File.WriteAllBytes("/path/to/directory/myDocument.pdf", pdfDocumentBytes);
        }
    }
}
