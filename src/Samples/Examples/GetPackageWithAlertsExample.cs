using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class GetPackageWithAlertsExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new GetPackageWithAlertsExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .WithCompany ("Acme Inc")
					            .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
						            .WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
						            .WithPhoneNumber("+12042345678")))
					.Build ();

			PackageId id = ossClient.CreatePackage (package);
			var extensions = new HashSet<DocumentPackageRequestExtension>();
			extensions.Add (DocumentPackageRequestExtension.ALERTS);
	        retrievedPackage = ossClient.GetPackageWithExtensions (id, extensions);
            Console.WriteLine("Document retrieved = " + retrievedPackage.Id);
		}
	}
}