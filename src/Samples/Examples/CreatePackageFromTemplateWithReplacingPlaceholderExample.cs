using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class CreatePackageFromTemplateWithReplacingPlaceholderExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new CreatePackageFromTemplateWithReplacingPlaceholderExample().Run();
        }

        public PackageId templateId;

        public readonly string DOCUMENT_NAME = "First Document";
        public readonly string DOCUMENT_ID = "doc1";
        public readonly string TEMPLATE_NAME = "CreatePackageFromTemplateWithReplacingPlaceholderExample Template: " + DateTime.Now;
        public readonly string TEMPLATE_DESCRIPTION = "This is a template created using OneSpan Sign SDK";
        public readonly string TEMPLATE_EMAIL_MESSAGE = "This message should be delivered to all signers";
        public readonly string TEMPLATE_SIGNER_FIRST = "John";
        public readonly string TEMPLATE_SIGNER_LAST = "Smith";

        public readonly string PACKAGE_DESCRIPTION = "This is a package created using OneSpan Sign SDK";
        public readonly string PACKAGE_EMAIL_MESSAGE = "This message should be delivered to all signers";
        public readonly string PACKAGE_SIGNER_FIRST = "Patty";
        public readonly string PACKAGE_SIGNER_LAST = "Galant";

        public readonly string PLACEHOLDER_ID1 = "PlaceholderId1";
        public readonly string PLACEHOLDER_ID2 = "PlaceholderId2";

        override public void Execute()
        {
            DocumentPackage template = PackageBuilder.NewPackageNamed(TEMPLATE_NAME)
                .DescribedAs(TEMPLATE_DESCRIPTION)
                .WithEmailMessage(TEMPLATE_EMAIL_MESSAGE)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName(TEMPLATE_SIGNER_FIRST)
                                .WithLastName(TEMPLATE_SIGNER_LAST)
                    .SigningOrder(3))
                .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder(PLACEHOLDER_ID1))
                    .SigningOrder(1))
                .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder(PLACEHOLDER_ID2))
                    .SigningOrder(2))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                    .WithId(DOCUMENT_ID)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100))
                    .WithSignature(SignatureBuilder.SignatureFor(new Placeholder(PLACEHOLDER_ID1))
                        .OnPage(0)
                        .AtPosition(400, 100))
                    .WithSignature(SignatureBuilder.SignatureFor(new Placeholder(PLACEHOLDER_ID2))
                        .OnPage(0)
                        .AtPosition(100, 300)))
                .Build();

            templateId = ossClient.CreateTemplate(template);

            DocumentPackage newPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs(PACKAGE_DESCRIPTION)
                .WithEmailMessage(PACKAGE_EMAIL_MESSAGE)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithFirstName(PACKAGE_SIGNER_FIRST)
                    .WithLastName(PACKAGE_SIGNER_LAST).Replacing(new Placeholder(PLACEHOLDER_ID1)))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                    .WithFirstName(PACKAGE_SIGNER_FIRST)
                    .WithLastName(PACKAGE_SIGNER_LAST).Replacing(new Placeholder(PLACEHOLDER_ID2)))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email4)
                    .WithFirstName(PACKAGE_SIGNER_FIRST)
                    .WithLastName(PACKAGE_SIGNER_LAST))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email5)
                    .WithFirstName(PACKAGE_SIGNER_FIRST)
                    .WithLastName(PACKAGE_SIGNER_LAST))
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                .Build();

            packageId = ossClient.CreatePackageFromTemplate(templateId, newPackage);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}

