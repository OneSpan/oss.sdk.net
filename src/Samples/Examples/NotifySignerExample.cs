using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class NotifySignerExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new NotifySignerExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs( "This is a package created using OneSpan Sign SDK" )
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

            PackageId packageId = ossClient.CreatePackage( superDuperPackage );
            ossClient.SendPackage( packageId );
            ossClient.PackageService.NotifySigner( packageId, email1, "HELLO SIGNER" );
        }

    }
}

