using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Example demonstrating how to set a field's clickable area, which expands the
    /// region a signer can click to interact with a checkbox or radio button beyond
    /// its visible size, optionally with the mark aligned within that area. Also
    /// demonstrates modifying a field's clickable area and adding a new field with
    /// a clickable area to an already-created package.
    /// </summary>
    public class ClickableAreaExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new ClickableAreaExample ().Run ();
        }

        public static readonly string DOCUMENT_NAME = "First Document";
        public static readonly string DOCUMENT_ID = "clickableAreaDocumentId";
        public static readonly SignatureId SIGNATURE_ID = new SignatureId ("clickableAreaSignatureId");

        public static readonly string CHECKBOX_ID = "checkboxWithClickableAreaId";
        public static readonly int CHECKBOX_PAGE = 0;
        public static readonly double CHECKBOX_WIDTH = 20;
        public static readonly double CHECKBOX_HEIGHT = 20;
        public static readonly int CHECKBOX_POSITION_X = 400;
        public static readonly int CHECKBOX_POSITION_Y = 300;
        public static readonly double CLICKABLE_AREA_WIDTH = 40;
        public static readonly double CLICKABLE_AREA_HEIGHT = 40;

        public static readonly string ALIGNED_CHECKBOX_ID = "checkboxWithAlignedClickableAreaId";
        public static readonly int ALIGNED_CHECKBOX_PAGE = 0;
        public static readonly double ALIGNED_CHECKBOX_WIDTH = 20;
        public static readonly double ALIGNED_CHECKBOX_HEIGHT = 20;
        public static readonly int ALIGNED_CHECKBOX_POSITION_X = 400;
        public static readonly int ALIGNED_CHECKBOX_POSITION_Y = 350;
        public static readonly double ALIGNED_CLICKABLE_AREA_WIDTH = 40;
        public static readonly double ALIGNED_CLICKABLE_AREA_HEIGHT = 40;
        public static readonly ClickableAreaAlignment ALIGNED_CLICKABLE_AREA_ALIGNMENT = ClickableAreaAlignment.TOP_LEFT;

        public static readonly string RADIO_ID = "radioWithClickableAreaId";
        public static readonly string RADIO_GROUP = "radioWithClickableAreaGroup";
        public static readonly int RADIO_PAGE = 0;
        public static readonly double RADIO_WIDTH = 20;
        public static readonly double RADIO_HEIGHT = 20;
        public static readonly int RADIO_POSITION_X = 400;
        public static readonly int RADIO_POSITION_Y = 400;
        public static readonly double RADIO_CLICKABLE_AREA_WIDTH = 40;
        public static readonly double RADIO_CLICKABLE_AREA_HEIGHT = 40;

        public static readonly double MODIFIED_CLICKABLE_AREA_WIDTH = 60;
        public static readonly double MODIFIED_CLICKABLE_AREA_HEIGHT = 60;

        public static readonly string ADDED_CHECKBOX_ID = "addedCheckboxWithClickableAreaId";
        public static readonly int ADDED_CHECKBOX_PAGE = 0;
        public static readonly double ADDED_CHECKBOX_WIDTH = 20;
        public static readonly double ADDED_CHECKBOX_HEIGHT = 20;
        public static readonly int ADDED_CHECKBOX_POSITION_X = 400;
        public static readonly int ADDED_CHECKBOX_POSITION_Y = 450;
        public static readonly double ADDED_CLICKABLE_AREA_WIDTH = 40;
        public static readonly double ADDED_CLICKABLE_AREA_HEIGHT = 40;

        override public void Execute ()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed (PackageName)
                    .DescribedAs ("This is a package created using OneSpan Sign SDK")
                    .WithSigner (SignerBuilder.NewSignerWithEmail (email1)
                                .WithFirstName ("John")
                                .WithLastName ("Smith"))
                    .WithDocument (DocumentBuilder.NewDocumentNamed (DOCUMENT_NAME)
                                  .FromStream (fileStream1, DocumentType.PDF)
                                  .WithId (DOCUMENT_ID)
                                  .WithSignature (SignatureBuilder.SignatureFor (email1)
                                        .OnPage (0)
                                        .AtPosition (400, 100)
                                        .WithId (SIGNATURE_ID)
                                        .WithField (FieldBuilder.CheckBox ()
                                            .WithId (CHECKBOX_ID)
                                            .OnPage (CHECKBOX_PAGE)
                                            .WithSize (CHECKBOX_WIDTH, CHECKBOX_HEIGHT)
                                            .AtPosition (CHECKBOX_POSITION_X, CHECKBOX_POSITION_Y)
                                            .WithClickableArea (CLICKABLE_AREA_WIDTH, CLICKABLE_AREA_HEIGHT))
                                        .WithField (FieldBuilder.CheckBox ()
                                            .WithId (ALIGNED_CHECKBOX_ID)
                                            .OnPage (ALIGNED_CHECKBOX_PAGE)
                                            .WithSize (ALIGNED_CHECKBOX_WIDTH, ALIGNED_CHECKBOX_HEIGHT)
                                            .AtPosition (ALIGNED_CHECKBOX_POSITION_X, ALIGNED_CHECKBOX_POSITION_Y)
                                            .WithClickableArea (ClickableAreaBuilder.NewClickableArea ()
                                                .WithSize (ALIGNED_CLICKABLE_AREA_WIDTH, ALIGNED_CLICKABLE_AREA_HEIGHT)
                                                .WithAlignment (ALIGNED_CLICKABLE_AREA_ALIGNMENT)))
                                        .WithField (FieldBuilder.RadioButton (RADIO_GROUP)
                                            .WithId (RADIO_ID)
                                            .OnPage (RADIO_PAGE)
                                            .WithSize (RADIO_WIDTH, RADIO_HEIGHT)
                                            .AtPosition (RADIO_POSITION_X, RADIO_POSITION_Y)
                                            .WithClickableArea (RADIO_CLICKABLE_AREA_WIDTH, RADIO_CLICKABLE_AREA_HEIGHT))
                                  ))
                    .Build ();

            packageId = ossClient.CreatePackage (superDuperPackage);

            // Modify: update the plain checkbox's clickable area to a new size
            Field modifiedCheckbox = FieldBuilder.CheckBox ()
                    .WithId (CHECKBOX_ID)
                    .OnPage (CHECKBOX_PAGE)
                    .WithSize (CHECKBOX_WIDTH, CHECKBOX_HEIGHT)
                    .AtPosition (CHECKBOX_POSITION_X, CHECKBOX_POSITION_Y)
                    .WithClickableArea (MODIFIED_CLICKABLE_AREA_WIDTH, MODIFIED_CLICKABLE_AREA_HEIGHT)
                    .Build ();
            ossClient.ApprovalService.ModifyField (packageId, DOCUMENT_ID, SIGNATURE_ID, modifiedCheckbox);

            // Add field: add a brand new checkbox with its own clickable area
            Field addedCheckbox = FieldBuilder.CheckBox ()
                    .WithId (ADDED_CHECKBOX_ID)
                    .OnPage (ADDED_CHECKBOX_PAGE)
                    .WithSize (ADDED_CHECKBOX_WIDTH, ADDED_CHECKBOX_HEIGHT)
                    .AtPosition (ADDED_CHECKBOX_POSITION_X, ADDED_CHECKBOX_POSITION_Y)
                    .WithClickableArea (ADDED_CLICKABLE_AREA_WIDTH, ADDED_CLICKABLE_AREA_HEIGHT)
                    .Build ();
            ossClient.ApprovalService.AddField (packageId, DOCUMENT_ID, SIGNATURE_ID, addedCheckbox);
        }
    }
}
