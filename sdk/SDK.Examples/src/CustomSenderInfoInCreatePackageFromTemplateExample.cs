using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CustomSenderInfoInCreatePackageFromTemplateExample : SDKSample
    {
        public const string SENDER_FIRST_NAME = "Rob";
        public const string SENDER_SECOND_NAME = "Mason";
        public const string SENDER_TITLE = "Chief Vizier";
        public const string SENDER_COMPANY = "The Masons";

        private Stream fileStream1;
        private PackageId templateId;
        public PackageId TemplateId
        {
            get
            {
                return templateId;
            }
        }
        private string senderEmail;
        public string SenderEmail
        {
            get
            {
                return senderEmail;
            }
        }
        public static void Main(string[] args)
        {
            new CustomSenderInfoInCreatePackageFromTemplateExample(Props.GetInstance()).Run();
        }


        public CustomSenderInfoInCreatePackageFromTemplateExample(Props props) : this(props.Get("api.key"), props.Get("api.url"))
        {
        }

        public CustomSenderInfoInCreatePackageFromTemplateExample(string apiKey, string apiUrl) : base( apiKey, apiUrl )
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

            DocumentPackage template =
                PackageBuilder.NewPackageNamed("CustomSenderInfoInCreateNewTemplateExample: " + DateTime.Now)
                .DescribedAs("This is a template created using the e-SignLive SDK")
                .WithEmailMessage("This message should be delivered to all signers")
                .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder("PlaceholderId1")))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream1, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(new Placeholder("PlaceholderId1"))
                                             .OnPage(0)
                                             .AtPosition(100, 100)
                                            )
                             )
                .Build();

            templateId = eslClient.CreateTemplate(template);

            packageId = eslClient.CreatePackageFromTemplate(templateId,
                        PackageBuilder.NewPackageNamed("CustomSenderInfoInCreatePackageFromTemplate packageFromTemplate")
                        .WithSenderInfo( SenderInfoBuilder.NewSenderInfo(senderEmail)
                                         .WithName(SENDER_FIRST_NAME, SENDER_SECOND_NAME)
                                         .WithTitle(SENDER_TITLE)
                                         .WithCompany(SENDER_COMPANY) )
                                     .Build() );
            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}

