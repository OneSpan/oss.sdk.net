using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SessionCreationExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SessionCreationExample().Run();
        }

        private string signerId = "myCustomSignerId";

        public SessionToken signerSessionToken;

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName( "John" )
                            .WithLastName( "Smith" )
                            .WithCustomId( signerId ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream1, DocumentType.PDF )
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) ) )
                    .Build();

            PackageId packageId = ossClient.CreatePackage( superDuperPackage );
            ossClient.SendPackage( packageId );
			signerSessionToken = ossClient.CreateSignerSessionToken( packageId, email1 );
            Console.WriteLine("{0}/access?sessionToken={1}", webpageUrl, signerSessionToken.Token);
        }
    }
}

