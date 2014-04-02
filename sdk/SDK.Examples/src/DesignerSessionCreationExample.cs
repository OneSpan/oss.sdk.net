using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class DesignerSessionCreationExample : SDKSample
    {
        public static void Main (string[] args)
        {
			new DesignerSessionCreationExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream;

		public DesignerSessionCreationExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

		public DesignerSessionCreationExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {            
			DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "DesignerSessionCreationExample: " + DateTime.Now )
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName( "John" )
                            .WithLastName( "Smith" ) )
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream, DocumentType.PDF )
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage( 0 )
                                   .AtPosition( 100, 100 ) ) )
                    .Build();

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
			AuthenticationToken token = eslClient.CreateAuthenticationToken();

			Console.WriteLine("packageId = " + packageId.Id);
			Console.WriteLine("authentication token = " + token.Token);
        }
    }
}