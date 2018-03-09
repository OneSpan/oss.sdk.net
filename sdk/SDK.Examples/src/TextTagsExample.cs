using System;
using Silanis.ESL.SDK;
using System.IO;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class TextTagsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new TextTagsExample().Run();
        }

        public Stream fileStream3;

        public readonly string DOCUMENT1_NAME = "First Document";
        public readonly string DOCUMENT2_NAME = "Second Document";
        public readonly string DOCUMENT3_NAME = "Third Document";

        override public void Execute()
        {
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/Text_Tag_And_Acrobat_Form_Fields.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/Text_Tag_And_Acrobat_Form_Fields.pdf").FullName);
            this.fileStream3 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/Text_Tag_And_Acrobat_Form_Fields.pdf").FullName);

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId("Signer1")
                    .WithFirstName("John1")
                    .WithLastName("Smith1"))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithCustomId("Signer2")
                    .WithFirstName("John2")
                    .WithLastName("Smith2"))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                    .WithCustomId("role3")
                    .WithFirstName("John3")
                    .WithLastName("Smith3"))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT1_NAME)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .EnableExtraction())
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT2_NAME)
                    .FromStream(fileStream2, DocumentType.PDF)
                    .WithExtractionType(ExtractionType.TEXT_TAGS)
                    .EnableExtraction())
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT3_NAME)
                    .FromStream(fileStream3, DocumentType.PDF)
                    .WithExtractionType(ExtractionType.TEXT_TAGS)
                    .WithExtractionType(ExtractionType.ACROFIELDS)
                    .EnableExtraction())
                .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
        }
    }
}

