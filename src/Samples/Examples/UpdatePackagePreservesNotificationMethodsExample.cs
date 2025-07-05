using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class UpdatePackagePreservesNotificationMethodsExample : SDKSample
    {
        public DocumentPackage CreatedPackage, UpdatedPackage;
        
        public static void Main(string[] args)
        {
            new UpdatePackagePreservesNotificationMethodsExample().Run();
        }


        public static readonly string PACKAGE_DESCRIPTION = "This example is created to demonstrate Notification methods";
        public static readonly string PACKAGE_SIGNER1_FIRST = "John";
        public static readonly string PACKAGE_SIGNER1_LAST = "Smith";
        
        override public void Execute()
        {
            DocumentPackage packageToCreate = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs(PACKAGE_DESCRIPTION)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName(PACKAGE_SIGNER1_FIRST)
                    .WithLastName(PACKAGE_SIGNER1_LAST)
                    .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                        .WithPrimaryMethods(NotificationMethod.EMAIL)
)                )
                .Build();
            
            packageId = ossClient.CreatePackage(packageToCreate);
            CreatedPackage = ossClient.GetPackage(packageId);
            
            DocumentPackage packageToUpdate = PackageBuilder.NewPackageNamed(PackageName).Build();
            
            // Cannot update signer's NM during package update
            ossClient.UpdatePackage(PackageId, packageToUpdate);
            UpdatedPackage = ossClient.GetPackage(packageId);
        }
    }
}

