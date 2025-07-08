using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class NotificationMethodsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new NotificationMethodsExample().Run();
        }
        
        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed(PackageName)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("David")
                    .WithLastName("Miller"))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                        .WithPrimaryMethods(NotificationMethod.EMAIL)
                    ))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                    .WithFirstName("Jane")
                    .WithLastName("Cooked")
                    .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                        .WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
                        .WithPhoneNumber("+12042345678")
                    ))
                .Build();

            packageId = ossClient.CreatePackage(package); 
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}