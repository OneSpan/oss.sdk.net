using System;
using System.IO;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Builder.Internal;

namespace SDK.Examples
{
    public class AttachmentExtractionExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AttachmentExtractionExample().Run();
        }

        public readonly string ATTACHMENT_NAME = "Driver's license";
        public readonly string ATTACHMENT_DESCRIPTION = "Please upload a scanned copy of your driver's license.";
        public readonly AttachmentType ATTACHMENT_TYPE = AttachmentType.DRIVERS_LICENSE;
        public readonly string SIGNER1_ID = "signer1Id";
        public readonly string ATTACHMENT_FILE_NAME = "attachment-drivers-license.pdf";

        public AttachmentRequirement retrievedAttachmentRequirement;
        public IList<AttachmentVerificationResult> verificationResults;

        private Stream attachmentInputStream;

        public AttachmentExtractionExample()
        {
            this.attachmentInputStream = File.OpenRead(
                new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document-for-anchor-extraction.pdf").FullName);
        }

        override public void Execute()
        {
            Signer signer = SignerBuilder.NewSignerWithEmail(email1)
                .WithFirstName("John")
                .WithLastName("Smith")
                .WithCustomId(SIGNER1_ID)
                .WithAttachmentRequirement(AttachmentRequirementBuilder.NewAttachmentRequirementWithName(ATTACHMENT_NAME)
                    .WithDescription(ATTACHMENT_DESCRIPTION)
                    .IsRequiredAttachment()
                    .WithAttachmentType(ATTACHMENT_TYPE)
                    .WithExtractionEnabled(true)
                    .Build())
                .Build();

            DocumentPackage pkg = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("Extraction example - demonstrates extractionEnabled and ExtractionResult")
                .WithSigner(signer)
                .WithDocument(DocumentBuilder.NewDocumentNamed("test document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .Build())
                    .Build())
                .Build();

            packageId = ossClient.CreateAndSendPackage(pkg);

            retrievedPackage = ossClient.GetPackage(packageId);
            retrievedAttachmentRequirement = retrievedPackage.GetSigner(email1)
                .GetAttachmentRequirement(ATTACHMENT_NAME);

            byte[] fileContent = new StreamDocumentSource(attachmentInputStream).Content();
            ossClient.UploadAttachment(packageId, retrievedAttachmentRequirement.Id, ATTACHMENT_FILE_NAME, fileContent, SIGNER1_ID);

            verificationResults = ossClient.AttachmentRequirementService
                .GetAttachmentVerificationResults(packageId);
        }
    }
}
