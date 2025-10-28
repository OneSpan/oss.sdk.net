using System.Collections.Generic;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class SupportingDocumentExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SupportingDocumentExample().Run();
        }

        // Results
        public List<SupportingDocumentDocumentInfo> supportingDocumentAfterUpload;
        public DocumentMetadata documentMetadata;
        public SupportingDocumentDocumentInfo supportingDocumentAfterRename;
        public List<SupportingDocumentDocumentInfo> supportingDocumentAfterDelete;
        public FileInfo downloadedAllSupportingDocumentsForPackage;

        private readonly byte[] supportingDocumentContent;

        private Signer signer;

        public readonly string SIGNER_ID = "signerId";
        public readonly string SUPPORTING_DOCUMENT_NAME_1 = "The supporting document number one.pdf";
        public readonly string SUPPORTING_DOCUMENT_NAME_2 = "The supporting document number two.pdf";
        public readonly string SUPPORTING_DOCUMENT_NAME_3 = "The supporting document number three.pdf";
        public readonly string DOCUMENT_RESOURCE = "document.pdf";
        private static readonly Dictionary<string, string> RENAME_PAYLOAD = new() { ["fileName"] = "renamed" };
        public readonly string DOWNLOADED_ALL_SUPPORTING_DOCUMENTS_FOR_PACKAGE = "downloadedAllSupportingDocumentsForPackage";

        public SupportingDocumentExample()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SampleDocuments", DOCUMENT_RESOURCE);
            this.supportingDocumentContent = File.ReadAllBytes(filePath);
        }

        override public void Execute()
        {
            signer = SignerBuilder.NewSignerWithEmail(email1)
                .WithFirstName("John")
                .WithLastName("Smith")
                .WithCustomId(SIGNER_ID)
                .Build();

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .WithSigner(signer)
                .WithDocument(DocumentBuilder.NewDocumentNamed("test document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .Build())
                    .Build())
                .Build();

            PackageId packageId = OssClient.CreatePackage(superDuperPackage);

            supportingDocumentAfterUpload = OssClient.SupportingDocumentsService.UploadSupportingDocuments(packageId.Id,
                CreateSupportingDocumentsMap());
            
            documentMetadata = OssClient.SupportingDocumentsService.DownloadSupportingDocument(
                packageId.Id,
                supportingDocumentAfterUpload[0].Id);
            
            supportingDocumentAfterRename = OssClient.SupportingDocumentsService.RenameSupportingDocument(
                packageId.Id,
                supportingDocumentAfterUpload[1].Id, RENAME_PAYLOAD);
            
            OssClient.SupportingDocumentsService.DeleteSupportingDocument(
                packageId.Id,
                supportingDocumentAfterUpload[2].Id);
            
            supportingDocumentAfterDelete = OssClient.SupportingDocumentsService.GetListOfSupportingDocuments(packageId.Id);
            
            DownloadedFile downloadedFiles = OssClient.SupportingDocumentsService.DownloadAllSupportingDocuments(packageId.Id);
            
            System.IO.File.WriteAllBytes(DOWNLOADED_ALL_SUPPORTING_DOCUMENTS_FOR_PACKAGE, downloadedFiles.Contents);
            
            downloadedAllSupportingDocumentsForPackage = new FileInfo(DOWNLOADED_ALL_SUPPORTING_DOCUMENTS_FOR_PACKAGE);
        }

        private Dictionary<string, byte[]> CreateSupportingDocumentsMap()
        {
            var documents = new Dictionary<string, byte[]>
            {
                { SUPPORTING_DOCUMENT_NAME_1, supportingDocumentContent },
                { SUPPORTING_DOCUMENT_NAME_2, supportingDocumentContent },
                { SUPPORTING_DOCUMENT_NAME_3, supportingDocumentContent }
            };
            return documents;
        }
    }
}