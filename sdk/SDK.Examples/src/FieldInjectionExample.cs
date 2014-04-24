using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class FieldInjectionExample : SDKSample {
        public static void Main (string[] args)
        {
            new FieldInjectionExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public PackageId PackageId
        {
            get;
            set;
        }

        public FieldInjectionExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public FieldInjectionExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document-with-fields.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "FieldInjectionExample " + DateTime.Now )
                .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                            .WithFirstName( "John" )
                            .WithLastName( "Smith" ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) )
                                  .WithInjectedField( FieldBuilder.TextField()
                                       .WithId( "AGENT_SIG_1" )
                                       .WithName( "AGENT_SIG_1" )
                                       .WithValue( "Test Value" ) ) )
                    .Build();

            PackageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( PackageId );
        }
    }
}

