using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

/**
 * A simple example that explains how to create a sender authentication token for the sender of a particular package using OAuth2 authentication
 * and then use that token to obtain a sender session.
 * optionally, it can specify a delegator user, then the sender session to be initialized as a delegation session.
 * <seealso cref="OSSAuthClientConfig.WithDelegatorId(string)"/>
 * For a more typical (and complex) example usage: {@link DesignerRedirectForPackageSenderExample}
 */
namespace SDK.Examples
{
    public class SenderAuthenticationTokenOauth2Example : OAuth2SDKSample
    {
        public string SenderSessionId { get; private set; }
        private AuthenticationClient AuthenticationClient;

        public SenderAuthenticationTokenOauth2Example() : base()
        {
            this.AuthenticationClient = new AuthenticationClient(webpageUrl);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithTitle("Managing Director")
                    .WithCompany("Acme Inc."))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream1, DocumentType.PDF)
                             )
                .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);

            string senderAuthenticationToken = ossClient.AuthenticationTokenService.CreateSenderAuthenticationToken(packageId);

            SenderSessionId = AuthenticationClient.GetSessionIdForSenderAuthenticationToken(senderAuthenticationToken);
        }

    }
}

