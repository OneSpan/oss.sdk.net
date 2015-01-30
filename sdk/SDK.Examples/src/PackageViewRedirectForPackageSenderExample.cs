using System;
using Silanis.ESL.SDK;
using System.IO;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class PackageViewRedirectForPackageSenderExample: SDKSample
    {
        public static void Main (string[] args)
        {
            new PackageViewRedirectForPackageSenderExample(Props.GetInstance()).Run();
        }

        public string generatedLinkToPackageViewForSender;

        private AuthenticationClient authenticationClient;
        private Stream fileStream;
        private string senderEmail;

        public PackageViewRedirectForPackageSenderExample( Props props ) : this(props.Get("api.key"),
                                                                             props.Get("api.url"),
                                                                             props.Get("webpage.url"))
        {
        }

        public PackageViewRedirectForPackageSenderExample( string apiKey, string apiUrl, string webpageUrl) : base( apiKey, apiUrl )
        {
            this.authenticationClient = new AuthenticationClient(webpageUrl);
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.senderEmail = GetRandomEmail();
        }

        override public void Execute()
        {
            eslClient.AccountService.InviteUser(
                AccountMemberBuilder.NewAccountMember(senderEmail)
                .WithFirstName("firstName")
                .WithLastName("lastName")
                .WithCompany("company")
                .WithTitle("title")
                .WithLanguage( "language" )
                .WithPhoneNumber( "phoneNumber" )
                .Build()
                );

            SenderInfo customSenderInfo = SenderInfoBuilder.NewSenderInfo(senderEmail)
                .WithName("firstName", "lastName")
                    .WithTitle("title")
                    .WithCompany("company")
                    .Build();

            DocumentPackage customSenderPackage = PackageBuilder.NewPackageNamed("PackageViewRedirectForPackageSenderExample " + DateTime.Now)
                .WithSenderInfo(customSenderInfo)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(1))
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .FromStream(fileStream, DocumentType.PDF)
                                  .WithId("doc1"))
                    .Build();
            PackageId customSenderPackageId = eslClient.CreatePackage (customSenderPackage);

            string senderAuthenticationToken = eslClient.AuthenticationTokenService.CreateSenderAuthenticationToken(customSenderPackageId);

            generatedLinkToPackageViewForSender = authenticationClient.BuildRedirectToPackageViewForSender(senderAuthenticationToken, customSenderPackageId);

            System.Console.WriteLine("PackageView redirect url: " + generatedLinkToPackageViewForSender);
        }
    }
}

