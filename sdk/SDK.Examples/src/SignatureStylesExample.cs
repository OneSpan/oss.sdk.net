using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignatureStylesExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SignatureStylesExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public SignatureStylesExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public SignatureStylesExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            String signerId = "myCustomSignerId";

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "SignatureStylesExample: " + DateTime.Now )
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName( "John" )
                            .WithLastName( "Smith" ) )
                .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                              .FromStream( fileStream1, DocumentType.PDF )
                              .WithSignature(SignatureBuilder.SignatureFor(email1)
                               .OnPage( 0 )
                               .AtPosition( 500, 100 ) ) 
                              .WithSignature(SignatureBuilder.InitialsFor(email1)
                               .OnPage( 0 )
                               .AtPosition( 500, 300 ) )
                              .WithSignature( SignatureBuilder.CaptureFor(email1)
                               .OnPage( 0 )
                               .AtPosition( 500, 500 ) ) )
                .Build();

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
        }
    }
}

