using System;
using System.Collections.Generic;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class SupportingDocumentCloneExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SupportingDocumentCloneExample().Run();
        }

        // Constants
        private const string DOCUMENT_NAME = "First Document";
        private const string DOCUMENT_ID = "doc1";
        private const string PACKAGE_DESCRIPTION = "This is a package created using OneSpan Sign SDK";
        private const string PACKAGE_EMAIL_MESSAGE = "This message should be delivered to all signers";
        private const string PACKAGE_EMAIL_MESSAGE2 = "Changed the email message";

        private const string PACKAGE_SIGNER1_FIRST = "John";
        private const string PACKAGE_SIGNER1_LAST = "Smith";
        private const string PACKAGE_SIGNER1_TITLE = "Manager";
        private const string PACKAGE_SIGNER1_COMPANY = "Acme Inc.";
        private const string PACKAGE_SIGNER1_CUSTOM_ID = "Signer1";

        private const string PACKAGE_SIGNER2_FIRST = "Elvis";
        private const string PACKAGE_SIGNER2_LAST = "Presley";
        private const string PACKAGE_SIGNER2_TITLE = "The King";
        private const string PACKAGE_SIGNER2_COMPANY = "Elvis Presley International";
        private const string PACKAGE_SIGNER2_CUSTOM_ID = "Signer2";

        private const string SUPPORTING_DOCUMENT_NAME_1 = "The supporting document number one.pdf";
        private const string SUPPORTING_DOCUMENT_NAME_2 = "The supporting document number two.pdf";
        private const string SUPPORTING_DOCUMENT_NAME_3 = "The supporting document number three.pdf";
        private const string DOCUMENT_RESOURCE = "document.pdf";

        private readonly byte[] _supportingDocumentContent;

        // Results
        public List<SupportingDocumentsInfo> SupportingDocumentBeforeCloning { get; private set; }
        public List<SupportingDocumentsInfo> SupportingDocumentAfterCloning { get; private set; }

        public SupportingDocumentCloneExample()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SampleDocuments", DOCUMENT_RESOURCE);

            if (!File.Exists(filePath))
            {
                throw new OssException($"Document file not found: {filePath}", null);
            }

            _supportingDocumentContent = File.ReadAllBytes(filePath);
        }

        public override void Execute()
        {
            var templateId = CreateTemplate();
            UploadSupportingDocumentsToTemplate(templateId);
            var newPackageId = CreatePackageFromTemplate(templateId);
            RetrieveClonedSupportingDocuments(newPackageId);
        }

        private PackageId CreateTemplate()
        {
            using (var documentStream = new MemoryStream(_supportingDocumentContent))
            {
                var template = PackageBuilder.NewPackageNamed($"Template {PackageName}")
                    .DescribedAs("first message")
                    .WithEmailMessage(PACKAGE_EMAIL_MESSAGE)
                    .WithSigner(CreateSigner1())
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                        .FromStream(documentStream, DocumentType.PDF)
                        .WithId(DOCUMENT_ID)
                        .Build())
                    .Build();

                return ossClient.CreateTemplate(template);
            }
        }

        private void UploadSupportingDocumentsToTemplate(PackageId templateId)
        {
            SupportingDocumentBeforeCloning = OssClient.SupportingDocumentsService
                .UploadSupportingDocuments(templateId.Id, CreateSupportingDocumentsDict());
        }

        private PackageId CreatePackageFromTemplate(PackageId templateId)
        {
            var newPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs(PACKAGE_DESCRIPTION)
                .WithEmailMessage(PACKAGE_EMAIL_MESSAGE2)
                .WithSigner(CreateSigner2())
                .WithSettings(CreatePackageSettings())
                .Build();

            packageId = ossClient.CreatePackageFromTemplate(templateId, newPackage);
            retrievedPackage = ossClient.GetPackage(packageId);

            return packageId;
        }

        private void RetrieveClonedSupportingDocuments(PackageId packageId)
        {
            SupportingDocumentAfterCloning = OssClient.SupportingDocumentsService
                .GetListOfSupportingDocuments(packageId.Id);
        }

        private Signer CreateSigner1()
        {
            return SignerBuilder.NewSignerWithEmail(email1)
                .WithFirstName(PACKAGE_SIGNER1_FIRST)
                .WithLastName(PACKAGE_SIGNER1_LAST)
                .WithTitle(PACKAGE_SIGNER1_TITLE)
                .WithCompany(PACKAGE_SIGNER1_COMPANY)
                .WithCustomId(PACKAGE_SIGNER1_CUSTOM_ID)
                .Build();
        }

        private Signer CreateSigner2()
        {
            return SignerBuilder.NewSignerWithEmail(email2)
                .WithFirstName(PACKAGE_SIGNER2_FIRST)
                .WithLastName(PACKAGE_SIGNER2_LAST)
                .WithTitle(PACKAGE_SIGNER2_TITLE)
                .WithCompany(PACKAGE_SIGNER2_COMPANY)
                .WithCustomId(PACKAGE_SIGNER2_CUSTOM_ID)
                .Build();
        }

        private DocumentPackageSettings CreatePackageSettings()
        {
            return DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
                .WithoutInPerson()
                .WithoutDecline()
                .WithoutOptOut()
                .WithWatermark()
                .Build();
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