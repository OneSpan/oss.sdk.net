using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using SDK.Examples;

namespace SDK.Examples
{
    public class FromFileCaptureSignatureExample : SDKSample
    {

        public static void Main(string[] args)
        {
            new FieldValidationExample().Run();
        }


        override public void Execute()
        {

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                    .DescribedAs("This is a package created using the eSignLive SDK")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(senderEmail)
                            .WithFirstName("John1")
                            .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                            .FromStream(fileStream1, DocumentType.PDF)
                            .WithSignature(SignatureBuilder.CaptureFor(senderEmail)
                                    .OnPage(0)
                                    .AtPosition(100, 100)
                                    .SetFromFile(true)))
                    .Build();

            packageId = ossClient.CreatePackageOneStep(superDuperPackage);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}