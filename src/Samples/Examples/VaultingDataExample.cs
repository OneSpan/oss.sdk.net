using System;
using System.IO;
using System.Globalization;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class VaultingDataExample : SDKSample
	{
        public static void Main (string[] args)
        {
			new VaultingDataExample().Run();
        }
        
        override public void Execute()
        {
			DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
					.DescribedAs("This is a package created using OneSpan Sign SDK")
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@acme.com")
						.WithCustomId("Client1")
						.WithFirstName("John")
						.WithLastName("Smith")
					)
					.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
						.FromStream(fileStream1, DocumentType.PDF)
						.WithSignature(SignatureBuilder.SignatureFor("john.smith@acme.com")
							.OnPage(0)
							.AtPosition(100, 100)
						)
					)
					.WithAttributes(DocumentPackageAttributesBuilder.WithAttribute("vaulting_data", "base64EncodedOldVaultingData")
					.Build();

			PackageId packageId = ossClient.CreateAndSendPackage(superDuperPackage);
			
			DocumentPackage packageToUpdate = PackageBuilder.NewPackageNamed(PackageName)
				.WithAttributes(DocumentPackageAttributesBuilder..WithAttribute("vaulting_data", "base64EncodedNewVaultingData"))
				.Build();
			
			ossClient.EOriginalService.UpdateVaultingData(packageId,packageToUpdate);
			ossClient.EOriginalService.GetVaultingData(packageId);
			ossClient.EOriginalService.Revault(packageId);
        }
	}
}