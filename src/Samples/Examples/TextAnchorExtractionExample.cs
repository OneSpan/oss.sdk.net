using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class TextAnchorExtractionExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new TextAnchorExtractionExample().Run();
        }

        public readonly string DOCUMENT_NAME = "Document With Anchors";
        public readonly int FIELD_WIDTH = 151;
        public readonly int FIELD_HEIGHT = 40;

        override public void Execute()
        {
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document-for-anchor-extraction.pdf").FullName);

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                                                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                                        .WithFirstName( "John" )
                                                        .WithLastName( "Smith" ) )
                                                .WithDocument( DocumentBuilder.NewDocumentNamed( DOCUMENT_NAME )
                                                        .FromStream( fileStream1, DocumentType.PDF )
                                                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                                                .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("Nondisclosure")
                                                                        .AtPosition(TextAnchorPosition.BOTTOMRIGHT)
                                                                        .WithSize(FIELD_WIDTH, FIELD_HEIGHT)
                                                                        .WithOffset(0, 0)
                                                                        .WithCharacter(9)
                                                                        .WithOccurrence(0)))
                                                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                                                .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("Receiving")
                                                                        .AtPosition(TextAnchorPosition.TOPLEFT)
                                                                        .WithSize(FIELD_WIDTH, FIELD_HEIGHT)
                                                                        .WithOffset(0, 0)
                                                                        .WithCharacter(0)
                                                                        .WithOccurrence(0))
                                                                .WithField(FieldBuilder.TextField()
                                                                        .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("Definition")
                                                                                .AtPosition(TextAnchorPosition.TOPLEFT)
                                                                                .WithSize(FIELD_WIDTH, FIELD_HEIGHT)
                                                                                .WithOffset(0, 0)
                                                                                .WithCharacter(0)
                                                                                .WithOccurrence(0)))
                                                                .WithField(FieldBuilder.TextField()
                                                                        .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("through legitimate means")
                                                                                .AtPosition(TextAnchorPosition.TOPLEFT)
                                                                                .WithSize(FIELD_WIDTH, FIELD_HEIGHT)
                                                                                .WithOffset(100, 100)
                                                                                .WithCharacter(0)
                                                                                .WithOccurrence(1))))
                                                             )
                                                .Build();

            PackageId packageId = ossClient.CreatePackage( superDuperPackage );
            ossClient.SendPackage( packageId );

            retrievedPackage = ossClient.GetPackage(packageId);
            Console.Out.WriteLine(packageId.Id);
        }
    }
}

