using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class BasicPackageCreationExample : SDKSample
    {
        public readonly string DOCUMENT1_NAME = "First Document";
        public readonly string DOCUMENT2_NAME = "Second Document";

        public readonly Nullable<Int32> SIGNATURE_FONT_SIZE = 10;
        public readonly Nullable<Int32> AUTO_FIELD_FONT_SIZE = 9;

        public static void Main(string[] args)
        {
            new BasicPackageCreationExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .ExpiresOn(DateTime.Now.AddMonths(100))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithTimezoneId("Canada/Mountain")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithCustomId("Client1")
                            .WithFirstName("John")
                            .WithLastName("Smith")
                            .WithTitle("Managing Director")
                            .WithCompany("Acme Inc.")
                           )
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                            .WithFirstName("Patty")
                            .WithLastName("Galant")
                           )
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT1_NAME)
                              .FromStream(fileStream1, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(email1)
                                             .WithFontSize (SIGNATURE_FONT_SIZE)
                                             .OnPage(0)
                                             .WithField(FieldBuilder.CheckBox()
                                                     .OnPage(0)
                                                     .AtPosition(400, 200)
                                                     .WithValue(FieldBuilder.CHECKBOX_CHECKED)
                                                       )
                                             .WithField (FieldBuilder.SignerName ()
                                                         .OnPage (0)
                                                         .AtPosition (100, 200)
                                                         .WithFontSize (AUTO_FIELD_FONT_SIZE))
                                             .AtPosition(100, 100)
                                            )
                             )
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT2_NAME)
                              .FromStream(fileStream2, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(email2)
                                             .OnPage(0)
                                             .AtPosition(100, 200)
                                             .WithField(FieldBuilder.RadioButton("group")
                                                     .WithName("firstField")
                                                     .WithValue(false)
                                                     .WithSize(20, 20)
                                                     .OnPage(0)
                                                     .AtPosition(400, 200))
                                             .WithField(FieldBuilder.RadioButton("group")
                                                     .WithName("secondField")
                                                     .WithValue(true)
                                                     .WithSize(20, 20)
                                                     .OnPage(0)
                                                     .AtPosition(400, 250))
                                             .WithField(FieldBuilder.RadioButton("group")
                                                     .WithName("thirdField")
                                                     .WithValue(false)
                                                     .WithSize(20, 20)
                                                     .OnPage(0)
                                                     .AtPosition(400, 300))
                                            )
                             )
                .Build();

            packageId = ossClient.CreatePackageOneStep(superDuperPackage);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
