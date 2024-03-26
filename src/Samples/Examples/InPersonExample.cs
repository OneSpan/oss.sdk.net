using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class InPersonExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new InPersonExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs( "This is a package created using OneSpan Sign SDK" )
                    .ExpiresOn( DateTime.Now.AddMonths(1) )
                    .WithEmailMessage( "This message should be delivered to all signers" )
                    .WithSettings( DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
                                  .WithInPerson() )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                .WithFirstName( "John" )
                                .WithLastName( "Smith" )
                                .WithTitle( "Managing Director" )
                                .WithCompany( "Acme Inc." ) )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email2 )
                                .WithFirstName( "Patty" )
                                .WithLastName( "Galant" ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream1, DocumentType.PDF )
                                  .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "Second Document" )
                                  .FromStream( fileStream2, DocumentType.PDF )
                                  .WithSignature( SignatureBuilder.SignatureFor( email2 )
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) ) )
                    .Build();

            PackageId packageId = ossClient.CreatePackage( superDuperPackage );

            ossClient.SendPackage( packageId );
        }
    }
}

