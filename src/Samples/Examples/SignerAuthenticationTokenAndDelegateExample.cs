using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SignerAuthenticationTokenAndDelegateExample : OAuth2SDKSample
    {
        /** 
        Will not be supported until later release.
        **/
        public static void Main (string[] args)
        {
            new SignerAuthenticationTokenAndDelegateExample().Run();
        }
        public string SignerSessionIdForMultiUse { get; private set; }
        public string SignerSessionIdForSingleUse { get; private set; }

        private AuthenticationClient AuthenticationClient;
        private string signerSessionFieldKey = "SDK SignerAuthenticationTokenExample Signer";

        public SignerAuthenticationTokenAndDelegateExample()
        {
            this.AuthenticationClient = new AuthenticationClient(webpageUrl);
        }

        override public void Execute()
        {
            string signerId = System.Guid.NewGuid().ToString();
            string signerEmail = props.Get("delegator.email");
            string delegateeEmail = props.Get("delegatee.email");
            DocumentPackage package = PackageBuilder.NewPackageNamed (PackageName)
                    .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(signerEmail)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithCompany ("Acme Inc")
                                .WithTitle ("Managing Director")
                                .WithCustomId(signerId))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(signerEmail)
                                        .OnPage(0)
                                        .AtPosition(500, 100)))
                    .Build ();

            PackageId packageId = ossClient.CreatePackage (package);
            ossClient.SendPackage(packageId);

            IDictionary<string, string> signerSessionFields = new Dictionary<string, string>();
            signerSessionFields.Add(signerSessionFieldKey, email1);
            string signerAuthenticationToken = ossClient.AuthenticationTokenService.CreateSignerAuthenticationToken(packageId, signerEmail, delegateeEmail, signerSessionFields);
            string signerAuthenticationTokenForSingleUse = ossClient.AuthenticationTokenService.CreateSignerAuthenticationTokenForSingleUse(packageId, signerEmail, delegateeEmail, signerSessionFields);

            //This session id can be set in a cookie header
            SignerSessionIdForMultiUse = AuthenticationClient.GetSessionIdForSignerAuthenticationToken(signerAuthenticationToken);
            SignerSessionIdForSingleUse = AuthenticationClient.GetSessionIdForSignerAuthenticationToken(signerAuthenticationTokenForSingleUse);
        }
    }
}

