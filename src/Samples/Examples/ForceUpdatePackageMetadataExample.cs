using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// End-to-end check for <c>ForceUpdatePackageMetadata</c> (PB-130297).
    ///
    /// Prerequisite: the account behind <c>api.key</c> must have the <c>manipulateMetadata</c>
    /// feature enabled. Without it the force call fails with a validation error
    /// (<c>manipulateMetadata.featureDisabled</c>).
    ///
    /// The transaction is created and SENT so it is no longer editable, then the example shows that
    /// <c>ForceUpdatePackageMetadata</c> updates the transaction's own custom metadata (its
    /// attributes) regardless of the transaction's status.
    ///
    /// Run: fill <c>signers.properties</c> (webpage.url, api.key, 1.email), then run this class's Main.
    /// </summary>
    public class ForceUpdatePackageMetadataExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ForceUpdatePackageMetadataExample().Run();
        }

        override public void Execute()
        {
            // 1. Create a package with one signer, upload a signable document, then SEND it so it is
            //    no longer editable.
            DocumentPackage builtPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("ForceUpdatePackageMetadata smoke test")
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

            // 2. Reload the sent transaction and force-update its custom metadata (transaction attributes).
            DocumentPackage sent = ossClient.GetPackage(packageId);
            sent.Attributes = new DocumentPackageAttributes(new Dictionary<string, object>
            {
                { "customerId", "12345" },
                { "region", "EMEA" }
            });

            ossClient.PackageService.ForceUpdatePackageMetadata(sent);
            Console.WriteLine("ForceUpdatePackageMetadata succeeded on a SENT transaction.");

            // 3. Read the metadata back and confirm the force-update was applied.
            DocumentPackage reloaded = ossClient.GetPackage(packageId);
            Console.WriteLine("Transaction metadata after force-update:");
            foreach (KeyValuePair<string, object> entry in reloaded.Attributes.Contents)
            {
                Console.WriteLine("  " + entry.Key + " = " + entry.Value);
            }
        }
    }
}
