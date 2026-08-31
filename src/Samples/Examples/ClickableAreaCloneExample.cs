using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Example demonstrating that a field's clickable area survives being cloned
    /// from a template into a new package via CreatePackageFromTemplate.
    ///
    /// The expandedClickableArea feature must be enabled on the account for this example to run.
    /// </summary>
    public class ClickableAreaCloneExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new ClickableAreaCloneExample ().Run ();
        }

        public static readonly string DOCUMENT_NAME = "First Document";
        public static readonly string PACKAGE_DESCRIPTION = "This is a package created using OneSpan Sign SDK";

        public static readonly string TEMPLATE_SIGNER_FIRST = "John";
        public static readonly string TEMPLATE_SIGNER_LAST = "Smith";

        public static readonly string PACKAGE_SIGNER_FIRST = "Elvis";
        public static readonly string PACKAGE_SIGNER_LAST = "Presley";

        public static readonly string CHECKBOX_ID = "clonedCheckboxWithClickableAreaId";
        public static readonly int CHECKBOX_PAGE = 0;
        public static readonly double CHECKBOX_WIDTH = 20;
        public static readonly double CHECKBOX_HEIGHT = 20;
        public static readonly int CHECKBOX_POSITION_X = 400;
        public static readonly int CHECKBOX_POSITION_Y = 300;
        public static readonly double CLICKABLE_AREA_WIDTH = 40;
        public static readonly double CLICKABLE_AREA_HEIGHT = 40;

        override public void Execute ()
        {
            DocumentPackage template = PackageBuilder.NewPackageNamed ("Template " + PackageName)
                    .DescribedAs (PACKAGE_DESCRIPTION)
                    .WithSigner (SignerBuilder.NewSignerWithEmail (email1)
                                .WithFirstName (TEMPLATE_SIGNER_FIRST)
                                .WithLastName (TEMPLATE_SIGNER_LAST))
                    .WithDocument (DocumentBuilder.NewDocumentNamed (DOCUMENT_NAME)
                                  .FromStream (fileStream1, DocumentType.PDF)
                                  .WithSignature (SignatureBuilder.SignatureFor (email1)
                                        .OnPage (0)
                                        .AtPosition (400, 100)
                                        .WithField (FieldBuilder.CheckBox ()
                                            .WithId (CHECKBOX_ID)
                                            .OnPage (CHECKBOX_PAGE)
                                            .WithSize (CHECKBOX_WIDTH, CHECKBOX_HEIGHT)
                                            .AtPosition (CHECKBOX_POSITION_X, CHECKBOX_POSITION_Y)
                                            .WithClickableArea (CLICKABLE_AREA_WIDTH, CLICKABLE_AREA_HEIGHT))
                                  ))
                    .Build ();

            PackageId templateId = ossClient.CreateTemplate (template);
            template.Id = templateId;

            DocumentPackage newPackage = PackageBuilder.NewPackageNamed (PackageName)
                    .DescribedAs (PACKAGE_DESCRIPTION)
                    .WithSigner (SignerBuilder.NewSignerWithEmail (email2)
                                .WithFirstName (PACKAGE_SIGNER_FIRST)
                                .WithLastName (PACKAGE_SIGNER_LAST))
                    .Build ();

            packageId = ossClient.CreatePackageFromTemplate (templateId, newPackage);
            retrievedPackage = ossClient.GetPackage (packageId);
        }
    }
}
