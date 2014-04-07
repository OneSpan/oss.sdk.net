using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CustomSenderInfoExample : SDKSample
    {
        public const string SENDER_FIRST_NAME = "Rob";
        public const string SENDER_SECOND_NAME = "Mason";
        public const string SENDER_TITLE = "Chief Vizier";
        public const string SENDER_COMPANY = "The Masons";

        public static void Main(string[] args)
        {
            CustomSenderInfoExample example = new CustomSenderInfoExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.eslClient.GetPackage(example.PackageId);
        }

        private string senderEmail;
        private Stream fileStream1;

        private DocumentPackage package;
        public DocumentPackage Package
        {
            get
            {
                return package;
            }
        }

        private PackageId packageId;
        public PackageId PackageId
        {
            get
            {
                return packageId;
            }
        }

        public CustomSenderInfoExample(Props props) : this(props.Get("api.url"), props.Get("api.key"))
        {
        }

        public CustomSenderInfoExample(string apiKey, string apiUrl) : base( apiKey, apiUrl )
        {
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            senderEmail = System.Guid.NewGuid().ToString().Replace("-","") + "@e-signlive.com";
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

            SenderInfo senderInfo = SenderInfoBuilder.NewSenderInfo(senderEmail)
                                    .WithName(SENDER_FIRST_NAME, SENDER_SECOND_NAME)
                                    .WithTitle(SENDER_TITLE)
                                    .WithCompany(SENDER_COMPANY)
                                    .Build();

            package = PackageBuilder.NewPackageNamed( "CustomSenderInfoExample " + DateTime.Now )
                      .WithSenderInfo( senderInfo )
                      .DescribedAs( "This is a package created using the e-SignLive SDK" )
                      .ExpiresOn( DateTime.Now.AddMonths(1) )
                      .WithEmailMessage( "This message should be delivered to all signers" )
                      .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                    .FromStream(fileStream1, DocumentType.PDF))
                      .Build();

            packageId = eslClient.CreatePackage( package );
        }
    }
}

