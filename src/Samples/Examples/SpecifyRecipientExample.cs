using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SpecifyRecipientExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SpecifyRecipientExample().Run();
        }

        public readonly string DOCUMENT_NAME = "Specify Recipient Document";
        public readonly string DOCUMENT_ID = "doc1";

        public readonly string REGULAR_SIGNER_FIRST = "John";
        public readonly string REGULAR_SIGNER_LAST = "Smith";
        public readonly string REGULAR_SIGNER_ID = "regular-signer";

        public readonly string PLACEHOLDER_ID = "placeholder-signer-id";

        public readonly string SPECIFIER_FIRST = "Jane";
        public readonly string SPECIFIER_LAST = "Doe";
        public readonly string SPECIFIER_ID = "specifier-signer";

        override public void Execute()
        {
            PlaceholderSigner placeholder = new PlaceholderSigner(PLACEHOLDER_ID);

            DocumentPackage package = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This package demonstrates the three role types created using the OneSpan Sign SDK")
                // Role 1: regular external signer
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId(REGULAR_SIGNER_ID)
                    .WithFirstName(REGULAR_SIGNER_FIRST)
                    .WithLastName(REGULAR_SIGNER_LAST))
                // Role 2: PLACEHOLDER role type — role contains a signer but signer fields are empty
                .WithSigner(SignerBuilder.NewPlaceholderSigner(placeholder))
                // Role 3: specifier — regular signer with specifier flag set to true
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithCustomId(SPECIFIER_ID)
                    .WithFirstName(SPECIFIER_FIRST)
                    .WithLastName(SPECIFIER_LAST)
                    .WithSpecifier(true))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                    .WithId(DOCUMENT_ID)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100))
                    .WithSignature(SignatureBuilder.SignatureFor(placeholder)
                        .OnPage(0)
                        .AtPosition(100, 200))
                    .WithSignature(SignatureBuilder.SignatureFor(email2)
                        .OnPage(0)
                        .AtPosition(100, 300)))
                .Build();

            packageId = ossClient.CreatePackageOneStep(package);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
