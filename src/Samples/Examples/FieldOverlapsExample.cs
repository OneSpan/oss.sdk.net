using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class FieldOverlapsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new FieldOverlapsExample ().Run ();
        }

        public static readonly string DOCUMENT_NAME = "First Document";
        public static readonly string DOCUMENT_ID = "fieldOverlapsDocumentId";

        public static readonly string FIRST_CHECKBOX_ID = "firstOverlappingCheckboxId";
        public static readonly string SECOND_CHECKBOX_ID = "secondOverlappingCheckboxId";
        public static readonly string SEPARATE_CHECKBOX_ID = "separateCheckboxId";
        public static readonly int CHECKBOX_PAGE = 0;

        public FieldOverlapValidationResult FieldOverlaps { get; private set; }

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
                                        .WithField (FieldBuilder.CheckBox ()
                                            .WithId (FIRST_CHECKBOX_ID)
                                            .OnPage (CHECKBOX_PAGE)
                                            .WithSize (20, 20)
                                            .AtPosition (400, 300))
                                        // Overlaps the first checkbox
                                        .WithField (FieldBuilder.CheckBox ()
                                            .WithId (SECOND_CHECKBOX_ID)
                                            .OnPage (CHECKBOX_PAGE)
                                            .WithSize (20, 20)
                                            .AtPosition (410, 310))
                                        // Apart from the other fields
                                        .WithField (FieldBuilder.CheckBox ()
                                            .WithId (SEPARATE_CHECKBOX_ID)
                                            .OnPage (CHECKBOX_PAGE)
                                            .WithSize (20, 20)
                                            .AtPosition (400, 500))
                                  ))
                    .Build ();

            packageId = ossClient.CreatePackage (superDuperPackage);

            FieldOverlaps = ossClient.PackageService.GetFieldOverlaps (packageId);

            foreach (FieldOverlap overlap in FieldOverlaps.Overlaps)
            {
                Console.WriteLine ("Field " + overlap.Field.Id + " on page " + overlap.Field.Page
                        + " overlaps " + overlap.ConflictsWith.Count + " field(s)");
            }
        }
    }
}
