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


        public static readonly string PACKAGE_DESCRIPTION = "Demonstrates package with signer notification methods creation and update";
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
                        .WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
                        .WithPhoneNumber("+12042345678"))                
                )
                .Build();
            
            packageId = ossClient.CreatePackage(packageToCreate);
            CreatedPackage = ossClient.GetPackage(packageId);
            
            DocumentPackage packageToUpdate = OssClient.GetPackage(packageId);

            var signer1 = packageToUpdate.GetSigner(email1);
            signer1.NotificationMethods.SetPrimaryMethods(NotificationMethod.EMAIL);
            signer1.NotificationMethods.Phone = null;
            
            // Cannot update signer's NM during package update
            ossClient.UpdatePackage(PackageId, packageToUpdate);
            UpdatedPackage = ossClient.GetPackage(packageId);
        }
    }
}

