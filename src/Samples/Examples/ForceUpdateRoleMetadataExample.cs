using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// End-to-end check for <c>ForceUpdateRoleMetadata</c> (PB-130298).
    ///
    /// Prerequisite: the account behind <c>api.key</c> must have the <c>manipulateMetadata</c>
    /// feature enabled. Without it the force call fails with a validation error
    /// (<c>manipulateMetadata.featureDisabled</c>).
    ///
    /// The transaction is created and SENT so it is no longer editable, then the example shows that
    /// <c>ForceUpdateRoleMetadata</c> updates a role's (signer's) custom metadata regardless of the
    /// transaction's status. The role metadata endpoint replaces the role's data map with the one
    /// supplied on the signer.
    ///
    /// Run: fill <c>signers.properties</c> (webpage.url, api.key, 1.email), then run this class's Main.
    /// </summary>
    public class ForceUpdateRoleMetadataExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ForceUpdateRoleMetadataExample().Run();
        }

        override public void Execute()
        {
            // 1. Create a package with one signer, upload a signable document, then SEND it so it is
            //    no longer editable.
            DocumentPackage builtPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("ForceUpdateRoleMetadata smoke test")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John")
                    .WithLastName("Smith"))
                .Build();

            packageId = ossClient.CreatePackage(builtPackage);

            Document document = DocumentBuilder.NewDocumentNamed("Contract")
                .FromStream(fileStream1, DocumentType.PDF)
                .WithSignature(SignatureBuilder.SignatureFor(email1)
                    .OnPage(0)
                    .AtPosition(100, 100))
                .Build();
            ossClient.UploadDocument(document, packageId);

            ossClient.SendPackage(packageId);
            Console.WriteLine("Sent transaction " + packageId.Id + " (status is now SENT, no longer editable).");

            // 2. Reload the sent transaction, grab the role (signer), and force-update its custom metadata.
            DocumentPackage sent = ossClient.GetPackage(packageId);
            Signer signer = sent.GetSigner(email1);
            signer.Data = new Dictionary<string, object>
            {
                { "customerId", "12345" },
                { "region", "EMEA" }
            };

            ossClient.PackageService.ForceUpdateRoleMetadata(sent, signer);
            Console.WriteLine("ForceUpdateRoleMetadata succeeded on a SENT transaction.");

            // 3. Read the metadata back and confirm the force-update was applied.
            DocumentPackage reloaded = ossClient.GetPackage(packageId);
            Console.WriteLine("Role metadata after force-update:");
            foreach (KeyValuePair<string, object> entry in reloaded.GetSigner(email1).Data)
            {
                Console.WriteLine("  " + entry.Key + " = " + entry.Value);
            }
        }
    }
}
