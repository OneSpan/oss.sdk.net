using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ChangeSignerExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ChangeSignerExample(Props.GetInstance()).Run();
        }

        public string email1;
        private Stream fileStream1;

        public ChangeSignerExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public ChangeSignerExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "ChangeSignerExample " + DateTime.Now )
                .DescribedAs( "This is a package created using the e-SignLive SDK" )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                .WithFirstName( "John" )
                                .WithLastName( "Smith" )
                                .CanChangeSigner())
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream1, DocumentType.PDF )
                                  .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                   .OnPage( 0 )
                                   .AtPosition( 500, 100 ) ) )
                    .Build();

            packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

