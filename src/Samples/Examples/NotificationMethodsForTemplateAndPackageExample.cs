using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class NotificationMethodsForTemplateAndPackageExample : SDKSample
    {
        public DocumentPackage TemplatePackage{
            get; set; 
        }
        public DocumentPackage CreatedPackage{
            get; set; 
        }
        public DocumentPackage SignerUpdatedPackage{
            get; set; 
        }
        
        private PackageId templateId;

        
        public static void Main(string[] args)
        {
            new NotificationMethodsForTemplateAndPackageExample().Run();
        }


        public static readonly string PACKAGE_DESCRIPTION = "This example is created to demonstrate that package update doesn't affect signer's notification methods";
        public static readonly string PACKAGE_SIGNER1_FIRST = "John";
        public static readonly string PACKAGE_SIGNER1_LAST = "Smith";
        public static readonly string PACKAGE_SIGNER1_CUSTOM_ID = "Signer1";
        public static readonly string PACKAGE_SIGNER1_PHONE = "+12042345678";
        
        override public void Execute()
        {
            DocumentPackage template = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs(PACKAGE_DESCRIPTION)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId(PACKAGE_SIGNER1_CUSTOM_ID)
                    .WithFirstName(PACKAGE_SIGNER1_FIRST)
                    .WithLastName(PACKAGE_SIGNER1_LAST)
                    .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                        .WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
                        .WithPhoneNumber(PACKAGE_SIGNER1_PHONE))
                )
                .Build();
            
            template.Id = ossClient.CreateTemplate(template);
            TemplatePackage = ossClient.GetPackage(template.Id);
            
            DocumentPackage newPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs(PACKAGE_DESCRIPTION)
                .Build();

            // Verify signer's NM when create package from template
            packageId = ossClient.CreatePackageFromTemplate(template.Id, newPackage);
            CreatedPackage = ossClient.GetPackage(packageId);
            
            // Able to update signer's NM during signer update
            var signer1 = ossClient.PackageService.GetSigner(packageId, PACKAGE_SIGNER1_CUSTOM_ID);
            signer1.NotificationMethods.SetPrimaryMethods(NotificationMethod.EMAIL);
            signer1.NotificationMethods.Phone = "+15147623743";
            
            // Signer's change of NM should be reflected on the new Signer updated package.
            ossClient.PackageService.UpdateSigner(packageId, signer1);
            SignerUpdatedPackage = ossClient.GetPackage(packageId);
        }
    }
}

