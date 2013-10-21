using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class AuthenticationMethodsExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new AuthenticationMethodsExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private string email3;
        private string sms3;
        private Stream fileStream1;

        public AuthenticationMethodsExample( Props props ) 
            : this(props.Get("api.url"), 
                  props.Get("api.key"), 
                  props.Get("1.email"), 
                  props.Get("2.email"), 
                  props.Get("3.email"), 
                  props.Get("3.sms")) {
        }

        public AuthenticationMethodsExample( string apiKey, string apiUrl, string email1, string email2, string email3, string sms3 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.sms3 = sms3;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# AuthenticationMethodsExample " + DateTime.Now)
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                                .WithFirstName("John2")
                                .WithLastName("Smith2")
                                .ChallengedWithQuestions( ChallengeBuilder.FirstQuestion( "What's 1+1?" )
                                             .Answer( "2" )
                                             .SecondQuestion( "What color's the sky?" )
                                             .Answer( "blue" ) ) )
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                                .WithFirstName("John3")
                                .WithLastName("Smith3")
                                .WithSMSSentTo(sms3))
                    .WithDocument(DocumentBuilder.NewDocumentNamed( "Custom Consent Document" )
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.AcceptanceFor( email1 )
                                  .OnPage(0)
                                  .AtPosition(100,100)))
                    .Build();

            PackageId id = eslClient.CreatePackage (package);
            eslClient.SendPackage(id);

            DocumentPackage retrievedPackage = eslClient.GetPackage(id);
        }
    }
}

