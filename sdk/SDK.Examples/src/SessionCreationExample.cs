using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SessionCreationExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SessionCreationExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;
        private string webpageUrl;
        private string signerId = "myCustomSignerId";

        public SessionToken signerSessionToken;

        public SessionCreationExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("webpage.url"), props.Get("1.email")) {
        }

        public SessionCreationExample( string apiKey, string apiUrl, string webpageUrl, string email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.webpageUrl = webpageUrl;
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "SessionCreationExample: " + DateTime.Now )
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

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
			signerSessionToken = eslClient.CreateSignerSessionToken( packageId, email1 );
            Console.WriteLine("{0}/access?sessionToken={1}", webpageUrl, signerSessionToken.Token);
        }
    }
}

