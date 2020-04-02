using System;
using System.IO;
using System.Globalization;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class PackageLanguageConfigurationExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new PackageLanguageConfigurationExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs( "This is a package created using the eSignLive SDK" )
				.WithLanguage(new CultureInfo("zh-CN"))
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

            packageId = ossClient.CreatePackage( superDuperPackage );
            ossClient.SendPackage( packageId );
            retrievedPackage = ossClient.GetPackage(packageId);
        }
	}
}