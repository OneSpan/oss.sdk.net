using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class BrandingBarConfigurationExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new BrandingBarConfigurationExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs( "This is a package created using OneSpan Sign SDK" )
                    .WithSettings( DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
                                  .WithoutOptOut()
                                  .WithoutDocumentToolbarDownloadButton()
                                  .WithCeremonyLayoutSettings( CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
                                                .WithoutGlobalNavigation()
                                                .WithoutGlobalDownloadButton() ) )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                .WithCustomId( "Client1" )
                                .WithFirstName( "John" )
                                .WithLastName( "Smith" )
                                .WithTitle( "Managing Director" )
                                .WithCompany( "Acme Inc." ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream1, DocumentType.PDF )
                                  .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) ) )
                    .Build();

            packageId = ossClient.CreatePackage( superDuperPackage );
            ossClient.SendPackage( packageId );
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}

