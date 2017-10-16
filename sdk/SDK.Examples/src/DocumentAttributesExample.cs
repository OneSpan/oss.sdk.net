using System;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class DocumentAttributesExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentAttributesExample().Run();
        }

        public readonly string DOCUMENT_NAME = "First Document";

        public readonly string ATTRIBUTE_KEY_1 = "Key 1";
        public readonly string ATTRIBUTE_KEY_2 = "Key 2";
        public readonly string ATTRIBUTE_KEY_3 = "Key 3";
        public readonly string ATTRIBUTE_1 = "Attribute 1";
        public readonly string ATTRIBUTE_2 = "Attribute 2";
        public readonly string ATTRIBUTE_3 = "Attribute 3";

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs( "This is a package created using the eSignLive SDK" )
                .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                    .WithCustomId( "Client1" )
                    .WithFirstName( "John" )
                    .WithLastName( "Smith" )
                    .WithTitle( "Managing Director" )
                    .WithCompany( "Acme Inc." ) )
                .WithDocument( DocumentBuilder.NewDocumentNamed( DOCUMENT_NAME )
                    .FromStream( fileStream1, DocumentType.PDF )
                    .WithSignature( SignatureBuilder.SignatureFor( email1 )
                        .OnPage( 0 )
                        .AtPosition( 100, 100 ) )
                    .WithData(DocumentAttributesBuilder.NewDocumentAttributes()
                        .AddAttribute( ATTRIBUTE_KEY_1, ATTRIBUTE_1 )
                        .AddAttribute( ATTRIBUTE_KEY_2, ATTRIBUTE_2 ))
                    .WithData(DocumentAttributesBuilder.NewDocumentAttributes()
                        .AddAttribute( ATTRIBUTE_KEY_3, ATTRIBUTE_3 ))
                )
                .Build();

            packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
        }
    }
}
