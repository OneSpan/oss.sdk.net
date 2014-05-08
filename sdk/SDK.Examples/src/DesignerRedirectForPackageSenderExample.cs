using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class DesignerRedirectForPackageSenderExample: SDKSample
    {
        public static void Main (string[] args)
        {
            new DesignerRedirectForPackageSenderExample(Props.GetInstance()).Run();
        }

        public string GeneratedLinkToDesignerForSender{ get; private set; }

        private AuthenticationClient authenticationClient;
        private Stream fileStream;
        private string packageSenderEmail;

        public DesignerRedirectForPackageSenderExample( Props props ) : this(props.Get("api.url"),
                props.Get("api.key"),
                props.Get("webpage.url"))
        {
        }

        public DesignerRedirectForPackageSenderExample( string apiKey, string apiUrl, string webpageUrl) : base( apiKey, apiUrl )
        {
            this.packageSenderEmail = GetRandomEmail();
            this.authenticationClient = new AuthenticationClient(webpageUrl);
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            //Create a user on behalf of which you are going to send the package
            eslClient.AccountService.InviteUser(
                AccountMemberBuilder.NewAccountMember(packageSenderEmail)
                .WithFirstName("firstName")
                .WithLastName("lastName")
                .WithCompany("company")
                .WithTitle("title")
                .WithLanguage( "language" )
                .WithPhoneNumber( "phoneNumber" )
                .Build()
            );

            PackageId customSenderPackageId = CreatePackageWithCustomSender(packageSenderEmail);


            string senderAuthenticationToken = eslClient.AuthenticationTokenService.CreateSenderAuthenticationToken(customSenderPackageId);


            GeneratedLinkToDesignerForSender = authenticationClient.BuildRedirectToDesignerForSender(senderAuthenticationToken, customSenderPackageId);

            //This is an example url that can be used in an iFrame or to open a browser window with a sender session (created from the package sender authentication token) and a redirect to the designer page.
            System.Console.WriteLine("Designer redirect url: " + GeneratedLinkToDesignerForSender);
        }

        private PackageId CreatePackageWithCustomSender(string PackageSenderEmail)
        {
            SenderInfo customSenderInfo = SenderInfoBuilder.NewSenderInfo(PackageSenderEmail)
                                    .WithName("firstName", "lastName")
                                    .WithTitle("title")
                                    .WithCompany("company")
                                    .Build();

            DocumentPackage customSenderPackage = PackageBuilder.NewPackageNamed("DesignerRedirectForPackageSenderExample " + DateTime.Now)
                .WithSenderInfo(customSenderInfo)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .ExpiresOn(DateTime.Now.AddMonths(1))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream, DocumentType.PDF)
                              .WithId("doc1"))
                .Build();
            PackageId customSenderPackageId = eslClient.CreatePackage (customSenderPackage);
            return customSenderPackageId;
        }
    }
}

