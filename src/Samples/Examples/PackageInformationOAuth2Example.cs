using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class PackageInformationOAuth2Example : OAuth2SDKSample
    {
        public PackageInformationOAuth2Example() : base()
        {
            
        }
        
        public SupportConfiguration supportConfiguration;

        public readonly string DOCUMENT_NAME = "First Document";

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);

            supportConfiguration = ossClient.PackageService.GetConfig(packageId);
        }
    }
}

