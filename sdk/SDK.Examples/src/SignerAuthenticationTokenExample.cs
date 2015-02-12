using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class SignerAuthenticationTokenExample : SDKSample
    {
        /** 
        Will not be supported until later release.
        **/
        public static void Main (string[] args)
        {
            new SignerAuthenticationTokenExample(Props.GetInstance()).Run();
        }
        public string SignerSessionId { get; private set; }

        private AuthenticationClient AuthenticationClient;
        private Stream FileStream;
        private string SignerEmail;

        public SignerAuthenticationTokenExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("webpage.url"), props.Get("1.email"))
        {
        }

        public SignerAuthenticationTokenExample( string apiKey, string apiUrl, string webpageUrl, string signerEmail) : base( apiKey, apiUrl )
        {
            this.AuthenticationClient = new AuthenticationClient(webpageUrl);
            this.SignerEmail = signerEmail;
            this.FileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            string signerId = System.Guid.NewGuid().ToString();
            DocumentPackage package = PackageBuilder.NewPackageNamed ("SignerAuthenticationTokenExample " + DateTime.Now)
                    .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(SignerEmail)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithCompany ("Acme Inc")
                                .WithTitle ("Managing Director")
                                .WithCustomId(signerId))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(FileStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(SignerEmail)
                                        .OnPage(0)
                                        .AtPosition(500, 100)))
                    .Build ();

            PackageId packageId = eslClient.CreatePackage (package);
            eslClient.SendPackage(packageId);


            string signerAuthenticationToken = eslClient.AuthenticationTokenService.CreateSignerAuthenticationToken(packageId, signerId);


            //This session id can be set in a cookie header
            SignerSessionId = AuthenticationClient.GetSessionIdForSignerAuthenticationToken(signerAuthenticationToken);
        }

    }
}

