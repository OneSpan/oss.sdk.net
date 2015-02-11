using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class SigningRedirectForSignerExample: SDKSample
    {
        /** 
        Will not be supported until later release.
        **/

        public static void Main (string[] args)
        {
            new SigningRedirectForSignerExample(Props.GetInstance()).Run();
        }

        public string GeneratedLinkToSigningForSigner{ get; private set; }

        private AuthenticationClient authenticationClient;
        private Stream fileStream;
        private string signerEmail;

        public SigningRedirectForSignerExample( Props props ) : this(props.Get("api.key"),
                props.Get("api.url"),
                props.Get("webpage.url"),
                props.Get("1.email"))
        {
        }

        public SigningRedirectForSignerExample( string apiKey, string apiUrl, string webpageUrl, string signerEmail) : base( apiKey, apiUrl )
        {
            this.signerEmail = signerEmail;
            this.authenticationClient = new AuthenticationClient(webpageUrl);
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            string signerId = System.Guid.NewGuid().ToString();
            DocumentPackage package = PackageBuilder.NewPackageNamed ("SignerAuthenticationTokenExample " + DateTime.Now)
                    .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(signerEmail)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithCompany ("Acme Inc")
                                .WithTitle ("Managing Director")
                                .WithCustomId(signerId))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(signerEmail)
                                        .OnPage(0)
                                        .AtPosition(500, 100)))
                    .Build ();

            PackageId packageId = eslClient.CreatePackage (package);
            eslClient.SendPackage(packageId);


            string signerAuthenticationToken = eslClient.AuthenticationTokenService.CreateSignerAuthenticationToken(packageId, signerId);


            GeneratedLinkToSigningForSigner = authenticationClient.BuildRedirectToSigningForSigner(signerAuthenticationToken, packageId);

            //This is an example url that can be used in an iFrame or to open a browser window with a signing session (created from the signer authentication token) and a redirect to the signing page.
            System.Console.WriteLine("Signing redirect url: " + GeneratedLinkToSigningForSigner);
        }

    }
}