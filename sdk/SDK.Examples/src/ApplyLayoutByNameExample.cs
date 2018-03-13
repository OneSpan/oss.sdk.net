using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ApplyLayoutByNameExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ApplyLayoutByNameExample().Run();
        }

        public string layoutId;

        public readonly string LAYOUT_PACKAGE_NAME = "Layout " + DateTime.Now;
        public readonly string LAYOUT_PACKAGE_DESCRIPTION = "This is a document layout.";
        public readonly string LAYOUT_DOCUMENT_NAME = "First Document";
        public readonly string FIELD_1_NAME = "field title";
        public readonly string FIELD_2_NAME = "field company";
        public readonly string APPLY_LAYOUT_DOCUMENT_NAME = "Apply Layout Document";
        public readonly string APPLY_LAYOUT_DOCUMENT_ID = "docId";
        public readonly string APPLY_LAYOUT_DOCUMENT_DESCRIPTION = "Document with applied layout description.";

        override public void Execute()
        {
            // Create a package with one document and one signature with two fields
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(LAYOUT_PACKAGE_NAME)
                .DescribedAs(LAYOUT_PACKAGE_DESCRIPTION)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId("Client1")
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithTitle("Managing Director")
                    .WithCompany("Acme Inc."))
                .WithDocument(DocumentBuilder.NewDocumentNamed(LAYOUT_DOCUMENT_NAME)
                    .WithId("documentId")
                    .WithDescription("Layout document description")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(120, 100)
                        .WithField(FieldBuilder.SignerTitle()
                            .WithName(FIELD_1_NAME)
                            .OnPage(0)
                            .AtPosition(120, 200))
                        .WithField(FieldBuilder.SignerCompany()
                            .WithName(FIELD_2_NAME)
                            .OnPage(0)
                            .AtPosition(120, 300))))
                .Build();

            PackageId packageId1 = eslClient.CreatePackage(superDuperPackage);
            superDuperPackage.Id = packageId1;

            // Create layout from package
            layoutId = eslClient.LayoutService.CreateLayout(superDuperPackage);

            // Create a new package to apply document layout to
            DocumentPackage packageFromLayout = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using the eSignLive SDK")
                .WithEmailMessage("This message should be delivered to all signers")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId("Client1")
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithTitle("Managing Director")
                    .WithCompany("Acme Inc."))
                .WithDocument(DocumentBuilder.NewDocumentNamed(APPLY_LAYOUT_DOCUMENT_NAME)
                    .WithId(APPLY_LAYOUT_DOCUMENT_ID)
                    .WithDescription(APPLY_LAYOUT_DOCUMENT_DESCRIPTION)
                    .FromStream(fileStream2, DocumentType.PDF))
                .Build();

            packageId = eslClient.CreatePackage(packageFromLayout);

            // Apply the layout to document in package
            eslClient.LayoutService.ApplyLayoutByName(packageId, APPLY_LAYOUT_DOCUMENT_ID, LAYOUT_PACKAGE_NAME);
        }
    }
}

