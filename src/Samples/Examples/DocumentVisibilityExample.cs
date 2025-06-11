using System;
using System.IO;
using OneSpanSign.Sdk;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class DocumentVisibilityExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentVisibilityExample(Props.GetInstance()).Run();
        }

        private Stream fileStream3;

        public readonly string DOC1_ID = "doc1Id";
        public readonly string DOC2_ID = "doc2Id";
        public readonly string DOC3_ID = "doc3Id";

        public readonly string DOC1_NAME = "First Document";
        public readonly string DOC2_NAME = "Second Document";
        public readonly string DOC3_NAME = "Third Document";

        public readonly string SIGNER1_ID = "signer1Id";
        public readonly string SIGNER2_ID = "signer2Id";
        public readonly string SIGNER3_ID = "signer3Id";

        public DocumentVisibility retrievedVisibility;
        public IList<Document> documentsForSigner1, documentsForSigner2, documentsForSigner3;
        public IList<Signer> signersForDocument1, signersForDocument2, signersForDocument3;

        public DocumentVisibilityExample(Props props) : this(props.Get("api.key"), 
                                                             props.Get("api.url"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email"))
        {
        }

        public DocumentVisibilityExample(string apiKey, string apiUrl, string email1, string email2, string email3)
        {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf").FullName);
            this.fileStream3 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("DocumentVisibilityExample: " + DateTime.Now)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId(SIGNER1_ID)
                    .WithFirstName("John1")
                    .WithLastName("Smith1"))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithCustomId(SIGNER2_ID)
                    .WithFirstName("John2")
                    .WithLastName("Smith2"))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                    .WithCustomId(SIGNER3_ID)
                    .WithFirstName("John3")
                    .WithLastName("Smith3"))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOC1_NAME)
                    .WithId(DOC1_ID)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOC2_NAME)
                    .WithId(DOC2_ID)
                    .FromStream(fileStream2, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email2)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOC3_NAME)
                    .WithId(DOC3_ID)
                    .FromStream(fileStream3, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email3)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);



            DocumentVisibility visibility = DocumentVisibilityBuilder.NewDocumentVisibility()
                .AddConfiguration(DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration(DOC1_ID)
                    .WithSignerIds(new List<string>{ SIGNER1_ID, SIGNER3_ID }))
                .AddConfiguration(DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration(DOC2_ID)
                    .WithSignerIds(new List<string>{ SIGNER2_ID, SIGNER3_ID }))
                .AddConfiguration(DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration(DOC3_ID)
                    .WithSignerIds(new List<string>{ SIGNER3_ID, SIGNER2_ID }))
                .Build();

            //      You can also set up a document visibility based on signer.
            /*
            DocumentVisibility visibility = DocumentVisibilityBasedOnSignerBuilder.NewDocumentVisibilityBasedOnSigner()
                .AddConfiguration(DocumentVisibilityConfigurationBasedOnSignerBuilder.NewDocumentVisibilityConfigurationBasedOnSigner(SIGNER1_ID)
                    .WithDocumentIds(new List<string>{ DOC1_ID }))
                .AddConfiguration(DocumentVisibilityConfigurationBasedOnSignerBuilder.NewDocumentVisibilityConfigurationBasedOnSigner(SIGNER2_ID)
                    .WithDocumentIds(new List<string>{ DOC2_ID, DOC3_ID }))
                .AddConfiguration(DocumentVisibilityConfigurationBasedOnSignerBuilder.NewDocumentVisibilityConfigurationBasedOnSigner(SIGNER3_ID)
                    .WithDocumentIds(new List<string>{ DOC1_ID, DOC2_ID, DOC3_ID }))
            .Build();
            */

            ossClient.ConfigureDocumentVisibility(packageId, visibility);

            retrievedVisibility = ossClient.getDocumentVisibility(packageId);

            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);

            documentsForSigner1 = ossClient.GetDocuments(packageId, SIGNER1_ID);
            documentsForSigner2 = ossClient.GetDocuments(packageId, SIGNER2_ID);
            documentsForSigner3 = ossClient.GetDocuments(packageId, SIGNER3_ID);

            signersForDocument1 = ossClient.GetSigners(packageId, DOC1_ID);
            signersForDocument2 = ossClient.GetSigners(packageId, DOC2_ID);
            signersForDocument3 = ossClient.GetSigners(packageId, DOC3_ID);
        }
    }
}

