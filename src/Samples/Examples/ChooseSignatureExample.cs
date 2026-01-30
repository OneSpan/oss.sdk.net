using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class ChooseSignatureExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignatureStylesExample().Run();
        }

        public readonly string DOCUMENT_NAME = "First Document";
        public readonly int CHOOSE_SIGNATURE_PAGE = 0;
        public readonly int CHOOSE_SIGNATURE_POSITION_X = 500;
        public readonly int CHOOSE_SIGNATURE_POSITION_Y = 100;
        public readonly int CHOOSE_INITIALS_PAGE = 0;
        public readonly int CHOOSE_INITIALS_POSITION_X = 500;
        public readonly int CHOOSE_INITIALS_POSITION_Y = 300;

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName("John")
                            .WithLastName("Smith"))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                .FromStream(fileStream1, DocumentType.PDF)
                                .WithSignature(SignatureBuilder.signature(SignatureStyle.CHOOSE_SIGNATURE, email1)
                                    .OnPage(CHOOSE_SIGNATURE_PAGE)
                                    .AtPosition(CHOOSE_SIGNATURE_POSITION_X, CHOOSE_SIGNATURE_POSITION_Y)) 
                                .WithSignature(SignatureBuilder.signature(SignatureStyle.CHOOSE_INITIALS, email1)
                                    .OnPage(CHOOSE_INITIALS_PAGE)
                                    .AtPosition(CHOOSE_INITIALS_POSITION_X, CHOOSE_INITIALS_POSITION_Y))
                                )
                .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}

