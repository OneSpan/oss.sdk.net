using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class TemplateExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new TemplateExample(Props.GetInstance()).Run();
        }

        public string email1;
        public string email2;

        public PackageId templateId;
        public PackageId instantiatedTemplateId;

        public readonly string UPDATED_TEMPLATE_NAME = "Modified name";
        public readonly string UPDATED_TEMPLATE_DESCRIPTION = "Modified description";

        public readonly string SIGNER1_FIRST_NAME = "John1";
        public readonly string SIGNER1_LAST_NAME = "Smith1";
        public readonly string SIGNER1_TITLE = "Managing Director1";
        public readonly string SIGNER1_COMPANY = "Acme Inc.1";

        public readonly string SIGNER2_FIRST_NAME = "John2";
        public readonly string SIGNER2_LAST_NAME = "Smith2";
        public readonly string SIGNER2_TITLE = "Managing Director2";
        public readonly string SIGNER2_COMPANY = "Acme Inc.2";

        public readonly string PACKAGE_NAME = "Package From Template";

        public TemplateExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email") )
        {
        }

        public TemplateExample(string apiKey, string apiUrl, string email1, string email2) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
        }

        override public void Execute()
        {
			Stream file = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
			Document document = DocumentBuilder.NewDocumentNamed("First Document")
				.WithId("doc1")
				.FromStream(file, DocumentType.PDF)
				.Build();

            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("CreateTemplateFromPackageExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                .WithFirstName(SIGNER1_FIRST_NAME)
                                .WithLastName(SIGNER1_LAST_NAME)
                                .WithTitle(SIGNER1_TITLE)
                                .WithCompany(SIGNER1_COMPANY)
                                )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email2 )
                                .WithFirstName(SIGNER2_FIRST_NAME)
                                .WithLastName(SIGNER2_LAST_NAME)
                                .WithTitle(SIGNER2_TITLE)
                                .WithCompany(SIGNER2_COMPANY)
                                )
				.WithDocument(document)
                .Build();

			templateId = eslClient.CreateTemplate(superDuperPackage);

			superDuperPackage.Id = templateId;

            superDuperPackage.Name = UPDATED_TEMPLATE_NAME;
            superDuperPackage.Description = UPDATED_TEMPLATE_DESCRIPTION;
			superDuperPackage.Autocomplete = false;

			eslClient.TemplateService.Update(superDuperPackage);

			document.Description = "Updated description";
			eslClient.TemplateService.UpdateDocumentMetadata(superDuperPackage, document);

			eslClient.TemplateService.DeleteDocument(templateId, "doc1");

			Console.WriteLine("Template {0} updated", templateId);

            instantiatedTemplateId = eslClient.CreatePackageFromTemplate(templateId,
                                        PackageBuilder.NewPackageNamed(PACKAGE_NAME)
                                             .Build() );
                                             
			Console.Out.WriteLine("Package from template = " + instantiatedTemplateId.Id);
        }
    }
}

