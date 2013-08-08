using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class FieldInjectionExample {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

        public static void Main (string[] args)
        {
            EslClient eslClient = new EslClient( apiToken, baseUrl );

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "Sample Insurance policy" )
                .WithSigner( SignerBuilder.NewSignerWithEmail( "dlawson@silanis.com" )
                            .WithFirstName( "John" )
                            .WithLastName( "Smith" ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromFile( "src/document-with-fields.pdf" )
                                  .WithSignature( SignatureBuilder.SignatureFor( "dlawson@silanis.com" )
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) )
                                  .WithInjectedField( FieldBuilder.TextField()
                                       .WithId( "AGENT_SIG_1" )
                                       .WithPositionExtracted()
                                       .WithName( "AGENT_SIG_1" )
                                       .WithValue( "Test Value" ) ) )
                    .Build();

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
        }
    }
}

