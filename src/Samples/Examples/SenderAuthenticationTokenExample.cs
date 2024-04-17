using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;

namespace SDK.Examples
{
    public class SenderAuthenticationTokenExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SenderAuthenticationTokenExample().Run();
        }

        public string SenderSessionId { get; private set; }
        
        private AuthenticationClient AuthenticationClient;

        public SenderAuthenticationTokenExample()
        {
            this.AuthenticationClient = new AuthenticationClient(webpageUrl);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .ExpiresOn(DateTime.Now.AddMonths(1))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream1, DocumentType.PDF)
                             )
                .Build();

            PackageId packageId = ossClient.CreatePackage(superDuperPackage);

            string senderAuthenticationToken = ossClient.AuthenticationTokenService.CreateSenderAuthenticationToken(packageId);

            SenderSessionId = AuthenticationClient.GetSessionIdForSenderAuthenticationToken(senderAuthenticationToken);
        }

    }
}

