using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class UpdateTemplateWithPlaceholderExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new UpdateTemplateWithPlaceholderExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1, fileStream2;

        public PackageId templateId;

        public readonly string DOCUMENT_NAME = "First Document";
        public readonly string DOCUMENT_ID = "doc1";
        public readonly string TEMPLATE1_NAME = "UpdateTemplateWithPlaceholderExample Template1: "  + DateTime.Now;
        public readonly string TEMPLATE2_NAME = "UpdateTemplateWithPlaceholderExample Template2: "  + DateTime.Now;
        public readonly string TEMPLATE_DESCRIPTION = "This is a template created using the e-SignLive SDK";
        public readonly string TEMPLATE_EMAIL_MESSAGE = "This message should be delivered to all signers";
        public readonly string TEMPLATE_SIGNER_FIRST = "John";
        public readonly string TEMPLATE_SIGNER_LAST = "Smith";

        public readonly string PLACEHOLDER_ID = "PlaceholderId1";
        public readonly string PLACEHOLDER2_ID = "PlaceholderId2";

        public DocumentPackage retrievedTemplate, updatedTemplate;

        public UpdateTemplateWithPlaceholderExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public UpdateTemplateWithPlaceholderExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage template1 = PackageBuilder.NewPackageNamed(TEMPLATE1_NAME)
                .DescribedAs(TEMPLATE_DESCRIPTION)
                    .WithEmailMessage(TEMPLATE_EMAIL_MESSAGE)
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName(TEMPLATE_SIGNER_FIRST)
                                .WithLastName(TEMPLATE_SIGNER_LAST))
                    .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder(PLACEHOLDER_ID)))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .WithId(DOCUMENT_ID)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(new Placeholder(PLACEHOLDER_ID))
                                   .OnPage(0)
                                   .AtPosition(400, 100)))
                    .Build();

            DocumentPackage template2 = PackageBuilder.NewPackageNamed(TEMPLATE2_NAME)
                .DescribedAs(TEMPLATE_DESCRIPTION)
                    .WithEmailMessage(TEMPLATE_EMAIL_MESSAGE)
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName(TEMPLATE_SIGNER_FIRST)
                                .WithLastName(TEMPLATE_SIGNER_LAST))
                    .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder(PLACEHOLDER_ID)))
                    .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder(PLACEHOLDER2_ID)))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .WithId(DOCUMENT_ID)
                                  .FromStream(fileStream2, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(new Placeholder(PLACEHOLDER_ID))
                                   .OnPage(0)
                                   .AtPosition(400, 100))
                                  .WithSignature(SignatureBuilder.SignatureFor(new Placeholder(PLACEHOLDER2_ID))
                                   .OnPage(0)
                                   .AtPosition(500, 100)))
                    .Build();

            templateId = eslClient.CreateTemplate(template1);

            retrievedTemplate = eslClient.GetPackage(templateId);
            template2.Id = templateId;

            eslClient.TemplateService.Update(template2);

            updatedTemplate = eslClient.GetPackage(templateId);
        }
    }
}

