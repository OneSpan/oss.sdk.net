using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class GetSigningStatusExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new GetSigningStatusExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public GetSigningStatusExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public GetSigningStatusExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# GetSigningStatusExample " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
					              .WithSignature(SignatureBuilder.SignatureFor(email1)
					              		.OnPage(0)
					               		.AtPosition(500, 100)))
					.Build ();

            PackageId id = eslClient.CreatePackage (package);

            SigningStatus status = eslClient.GetSigningStatus( id, null, null );

            eslClient.SendPackage(id);

            status = eslClient.GetSigningStatus( id, null, null );
            Console.WriteLine("Signing status = " + status);
		}
	}
}