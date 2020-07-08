using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder.Internal;

namespace SDK.Examples
{
    public class AttachmentFileExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AttachmentFileExample ().Run();
        }

        private Stream attachmentInputStream;

        private Signer signer;

        public readonly string NAME = "Driver's license";
        public readonly string DESCRIPTION = "Please upload a scanned copy of your driver's license.";
        public readonly string SIGNER_ID = "signerId";
        public IList<AttachmentRequirement> signerAttachments;
        private AttachmentRequirement signerAtt;
        public readonly string ATTACHMENT_FILE_NAME = "The attachment for signer.pdf";
        public IList<AttachmentFile> filesAfterUpload;
        public IList<AttachmentFile> filesAfterDelete;

        public FileInfo downloadedAttachmentFile;
        public long signerAttachmentFileSize;

        public AttachmentFileExample ()
        {
            this.attachmentInputStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document-for-anchor-extraction.pdf").FullName);
     
        }

        override public void Execute()
        {
            // Signer with 1 attachment requirement
            signer = SignerBuilder.NewSignerWithEmail (email1)
                .WithFirstName ("John")
                    .WithLastName ("Smith")
                    .WithCustomId (SIGNER_ID)
                    .WithAttachmentRequirement (AttachmentRequirementBuilder.NewAttachmentRequirementWithName (NAME)
                                               .WithDescription (DESCRIPTION)
                                               .IsRequiredAttachment ()
                                               .Build ()).Build ();
        
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using the eSignLive SDK")
                    .WithSigner(signer)
                    .WithDocument(DocumentBuilder.NewDocumentNamed("test document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                  .Build())
                                  .Build())
                    .Build();

            packageId = OssClient.CreateAndSendPackage(superDuperPackage);

            retrievedPackage = OssClient.GetPackage (packageId);
            signerAtt = retrievedPackage.GetSigner (email1).GetAttachmentRequirement (NAME);

            byte [] attachmentForSignerFileContent = new StreamDocumentSource (attachmentInputStream).Content ();
            signerAttachmentFileSize = attachmentForSignerFileContent.Length;
            OssClient.UploadAttachment (packageId, signerAtt.Id, ATTACHMENT_FILE_NAME,
                attachmentForSignerFileContent, SIGNER_ID);

            retrievedPackage = OssClient.GetPackage (packageId);
            signerAtt = retrievedPackage.GetSigner (email1).GetAttachmentRequirement (NAME);

            filesAfterUpload = signerAtt.Files;

            AttachmentFile attachmentFile = filesAfterUpload[0];

            // Download signer attachment
            DownloadedFile downloadedAttachment = ossClient.AttachmentRequirementService.DownloadAttachmentFile(packageId, signerAtt.Id, attachmentFile.Id);
            System.IO.File.WriteAllBytes(downloadedAttachment.Filename, downloadedAttachment.Contents);

            OssClient.DeleteAttachmentFile (packageId, signerAtt.Id, attachmentFile.Id, SIGNER_ID);

            retrievedPackage = OssClient.GetPackage (packageId);
            signerAtt = retrievedPackage.GetSigner (email1).GetAttachmentRequirement (NAME);

            filesAfterDelete = signerAtt.Files;

            downloadedAttachmentFile = new FileInfo(downloadedAttachment.Filename);
        }

    }
}
