using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class TextAnchorExtractionExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new TextAnchorExtractionExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;
        public int FieldWidth = 150;
        public int FieldHeight = 40;
        public DocumentPackage retrievedPackage;

        public TextAnchorExtractionExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public TextAnchorExtractionExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document-for-anchor-extraction.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "TextAnchorExtractionExample: " + DateTime.Now )
                                                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                                        .WithFirstName( "John" )
                                                        .WithLastName( "Smith" ) )
                                                .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                                        .FromStream( fileStream1, DocumentType.PDF )
                                                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                                                .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("Nondisclosure")
                                                                        .AtPosition(TextAnchorPosition.BOTTOMRIGHT)
                                                                        .WithSize(FieldWidth, FieldHeight)
                                                                        .WithOffset(0, 0)
                                                                        .WithCharacter(9)
                                                                        .WithOccurrence(0)))
                                                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                                                .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("Receiving")
                                                                        .AtPosition(TextAnchorPosition.TOPLEFT)
                                                                        .WithSize(FieldWidth, FieldHeight)
                                                                        .WithOffset(0, 0)
                                                                        .WithCharacter(0)
                                                                        .WithOccurrence(0))
                                                                .WithField(FieldBuilder.TextField()
                                                                        .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("Definition")
                                                                                .AtPosition(TextAnchorPosition.TOPLEFT)
                                                                                .WithSize(FieldWidth, FieldHeight)
                                                                                .WithOffset(0, 0)
                                                                                .WithCharacter(0)
                                                                                .WithOccurrence(0)))
                                                                .WithField(FieldBuilder.TextField()
                                                                        .WithPositionAnchor(TextAnchorBuilder.NewTextAnchor("through legitimate means")
                                                                                .AtPosition(TextAnchorPosition.TOPLEFT)
                                                                                .WithSize(FieldWidth, FieldHeight)
                                                                                .WithOffset(100, 100)
                                                                                .WithCharacter(0)
                                                                                .WithOccurrence(1))))
                                                             )
                                                .Build();

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );

            retrievedPackage = eslClient.GetPackage(packageId);
            Console.Out.WriteLine(packageId.Id);
        }
    }
}

