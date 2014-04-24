using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class SenderAuthenticationTokenExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SenderAuthenticationTokenExample(Props.GetInstance()).Run();
        }

        public string SenderSessionId { get; private set; }
        
        private AuthenticationClient AuthenticationClient;
        private Stream fileStream;


        public SenderAuthenticationTokenExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("webpage.url"))
        {
        }

        public SenderAuthenticationTokenExample( string apiKey, string apiUrl, string webpageUrl) : base( apiKey, apiUrl )
        {
            this.AuthenticationClient = new AuthenticationClient(webpageUrl);
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("SenderAuthenticationTokenExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .ExpiresOn(DateTime.Now.AddMonths(1))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream, DocumentType.PDF)
                             )
                .Build();

            PackageId packageId = eslClient.CreatePackage(superDuperPackage);

            string senderAuthenticationToken = eslClient.GetAuthenticationTokenService().CreateSenderAuthenticationToken(packageId);

            SenderSessionId = AuthenticationClient.GetSessionIdForSenderAuthenticationToken(senderAuthenticationToken);
        }

    }
}

