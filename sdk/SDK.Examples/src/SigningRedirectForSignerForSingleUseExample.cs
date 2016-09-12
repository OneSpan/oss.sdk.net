using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SigningRedirectForSignerForSingleUseExample: SDKSample
    {
        public static void Main (string[] args)
        {
            new SigningRedirectForSignerForSingleUseExample().Run();
        }

        public string GeneratedLinkToSigningForSigner{ get; private set; }

        private AuthenticationClient authenticationClient;

        public SigningRedirectForSignerForSingleUseExample()
        {
            this.authenticationClient = new AuthenticationClient(webpageUrl);
        }

        override public void Execute()
        {
            string signerId = System.Guid.NewGuid().ToString();
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
                .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithCompany ("Acme Inc")
                                .WithTitle ("Managing Director")
                                .WithCustomId(signerId))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(500, 100)))
                    .Build ();

            PackageId packageId = eslClient.CreatePackage (package);
            eslClient.SendPackage(packageId);


            string signerAuthenticationToken = eslClient.AuthenticationTokenService.CreateSignerAuthenticationTokenForSingleUse(packageId, signerId, null);


            GeneratedLinkToSigningForSigner = authenticationClient.BuildRedirectToSigningForSigner(signerAuthenticationToken, packageId);

            System.Console.WriteLine("Signing redirect url: " + GeneratedLinkToSigningForSigner);
        }
    }
}

