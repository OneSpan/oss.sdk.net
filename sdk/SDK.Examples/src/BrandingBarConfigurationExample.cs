using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class BrandingBarConfigurationExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new BrandingBarConfigurationExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public BrandingBarConfigurationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public BrandingBarConfigurationExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "BrandingBarConfigurationExample " + DateTime.Now )
                .DescribedAs( "This is a package created using the e-SignLive SDK" )
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

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );

            SessionToken sessionToken = eslClient.CreateSessionToken( packageId, "Client1" );
        }
    }
}

