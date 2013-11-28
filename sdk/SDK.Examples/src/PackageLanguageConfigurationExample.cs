using System;
using System.IO;
using System.Globalization;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class PackageLanguageConfigurationExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new PackageLanguageConfigurationExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public PackageLanguageConfigurationExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public PackageLanguageConfigurationExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "PackageLanguageConfigurationExample: " + DateTime.Now )
                .DescribedAs( "This is a package created using the e-SignLive SDK" )
				.WithLanguage(new CultureInfo("fr"))
                    .ExpiresOn( DateTime.Now.AddMonths(1) )
                    .WithEmailMessage( "This message should be delivered to all signers" )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                .WithFirstName( "John" )
                                .WithLastName( "Smith" ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream1, DocumentType.PDF )
                                  .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 )
                                   .WithField( FieldBuilder.TextField()
                               .OnPage( 0 )
                               .AtPosition( 400, 100 )
                               .WithSize( 200, 50 ) ) ) )
                    .Build();

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
            eslClient.PackageService.NotifySigner( packageId, email1, "HELLO SIGNER" );
        }
	}
}