using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Example class demonstrating a transaction with a carbon copy recipient.
    ///
    /// A carbon copy recipient receives a copy of the completed documents but never participates in
    /// the signing ceremony. They are excluded from the signing order and are only notified once the
    /// transaction is complete, so no signature is placed for them.
    ///
    /// The carbonCopyRecipient feature must be enabled on the account for this example to run.
    /// </summary>
    public class CarbonCopyRecipientExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new CarbonCopyRecipientExample().Run();
        }

        public readonly string DOCUMENT_NAME = "Carbon Copy Recipient Document";
        public readonly string DOCUMENT_ID = "doc1";

        public readonly string SIGNER_FIRST = "John";
        public readonly string SIGNER_LAST = "Smith";
        public readonly string SIGNER_ID = "regular-signer";

        public readonly string CARBON_COPY_FIRST = "Jane";
        public readonly string CARBON_COPY_LAST = "Doe";
        public readonly string CARBON_COPY_ID = "carbon-copy-recipient";

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This transaction demonstrates a carbon copy recipient, created using the OneSpan Sign SDK")
                // Role 1: regular external signer
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId(SIGNER_ID)
                    .WithFirstName(SIGNER_FIRST)
                    .WithLastName(SIGNER_LAST))
                // Role 2: carbon copy recipient — receives the completed documents, never signs
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithCustomId(CARBON_COPY_ID)
                    .WithFirstName(CARBON_COPY_FIRST)
                    .WithLastName(CARBON_COPY_LAST)
                    .AsCarbonCopyRecipient())
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                    .WithId(DOCUMENT_ID)
                    .FromStream(fileStream1, DocumentType.PDF)
                    // Only the signer gets a signature; a carbon copy recipient cannot have
                    // signatures or fields.
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = ossClient.CreatePackageOneStep(package);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
