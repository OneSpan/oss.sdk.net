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
        public List<SupportingDocumentsInfo> SupportingDocumentAfterUpload { get; private set; }
        public DocumentMetadata DocumentMetadata { get; private set; }
        public SupportingDocumentsInfo SupportingDocumentAfterRename { get; private set; }
        public List<SupportingDocumentsInfo> SupportingDocumentAfterDelete { get; private set; }
        public FileInfo DownloadedAllSupportingDocumentsForPackage { get; private set; }

        private readonly byte[] _supportingDocumentContent;
        private readonly byte[] _mainDocumentContent;

        // Constants
        private const string SIGNER_ID = "signerId";
        private const string SUPPORTING_DOCUMENT_NAME_1 = "The supporting document number one.pdf";
        private const string SUPPORTING_DOCUMENT_NAME_2 = "The supporting document number two.pdf";
        private const string SUPPORTING_DOCUMENT_NAME_3 = "The supporting document number three.pdf";
        private const string DOCUMENT_RESOURCE = "document.pdf";
        private const string RENAMED_DOCUMENT_NAME = "renamed";
        private const string DOWNLOADED_FILE_NAME = "downloadedAllSupportingDocumentsForPackage.zip";

        private static readonly Dictionary<string, string> RenamePayload = new Dictionary<string, string>
        {
            ["fileName"] = RENAMED_DOCUMENT_NAME
        };

        public SupportingDocumentExample()
        {
            var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "SampleDocuments");
            var documentFilePath = Path.Combine(documentsPath, DOCUMENT_RESOURCE);

            if (!File.Exists(documentFilePath))
            {
                throw new OssException($"Document file not found: {documentFilePath}", null);
            }

            _supportingDocumentContent = File.ReadAllBytes(documentFilePath);
            _mainDocumentContent = File.ReadAllBytes(documentFilePath);
        }

        public override void Execute()
        {
            var signer = CreateSigner();
            var packageId = CreatePackageWithDocument(signer);

            // Upload supporting documents
            SupportingDocumentAfterUpload = OssClient.SupportingDocumentsService
                .UploadSupportingDocuments(packageId.Id, CreateSupportingDocumentsDict());

            // Download first supporting document metadata
            DocumentMetadata = OssClient.SupportingDocumentsService
                .DownloadSupportingDocument(packageId.Id, SupportingDocumentAfterUpload[0].Id ?? 0);

            // Rename second supporting document
            SupportingDocumentAfterRename = OssClient.SupportingDocumentsService
                .RenameSupportingDocument(packageId.Id, SupportingDocumentAfterUpload[0].Id ?? 0, RenamePayload);

            // Delete third supporting document
            OssClient.SupportingDocumentsService
                .DeleteSupportingDocument(packageId.Id, SupportingDocumentAfterUpload[0].Id ?? 0);

            // Get remaining supporting documents
            SupportingDocumentAfterDelete = OssClient.SupportingDocumentsService
                .GetListOfSupportingDocuments(packageId.Id);

            // Download all supporting documents
            DownloadAllSupportingDocuments(packageId);
        }

        private Signer CreateSigner()
        {
            return SignerBuilder.NewSignerWithEmail(email1)
                .WithFirstName("John")
                .WithLastName("Smith")
                .WithCustomId(SIGNER_ID)
                .Build();
        }

        private PackageId CreatePackageWithDocument(Signer signer)
        {
            using var documentStream = new MemoryStream(_mainDocumentContent);

            var package = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .WithSigner(signer)
                .WithDocument(DocumentBuilder.NewDocumentNamed("test document")
                    .FromStream(documentStream, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1).Build())
                    .Build())
                .Build();

            return OssClient.CreatePackage(package);
        }

        private void DownloadAllSupportingDocuments(PackageId packageId)
        {
            var downloadedFiles = OssClient.SupportingDocumentsService
                .DownloadAllSupportingDocuments(packageId.Id);

            var tempPath = Path.GetTempPath();
            var downloadPath = Path.Combine(tempPath, DOWNLOADED_FILE_NAME);

            File.WriteAllBytes(downloadPath, downloadedFiles.Contents);
            DownloadedAllSupportingDocumentsForPackage = new FileInfo(downloadPath);
        }

        private Dictionary<string, byte[]> CreateSupportingDocumentsDict()
        {
            return new Dictionary<string, byte[]>
            {
                { SUPPORTING_DOCUMENT_NAME_1, _supportingDocumentContent },
                { SUPPORTING_DOCUMENT_NAME_2, _supportingDocumentContent },
                { SUPPORTING_DOCUMENT_NAME_3, _supportingDocumentContent }
            };
        }
    }
}