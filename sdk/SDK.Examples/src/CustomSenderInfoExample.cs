using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CustomSenderInfoExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new CustomSenderInfoExample(Props.GetInstance()).Run();
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

        public CustomSenderInfoExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

		public CustomSenderInfoExample(string apiKey, string apiUrl, string senderEmail) : base( apiKey, apiUrl )
        {
			this.senderEmail = senderEmail;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
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

			SenderInfo senderInfo = SenderInfoBuilder.NewSenderInfo(senderEmail)
				.WithName("Rob", "Mason")
				.WithTitle("Chief Vizier")
				.WithCompany("The Masons")
				.Build();

			DocumentPackage package = PackageBuilder.NewPackageNamed( "CustomSenderInfoExample " + DateTime.Now )
				.WithSenderInfo( senderInfo )
                .DescribedAs( "This is a package created using the e-SignLive SDK" )
                .ExpiresOn( DateTime.Now.AddMonths(1) )
                .WithEmailMessage( "This message should be delivered to all signers" )
                .Build();

			packageId = eslClient.CreatePackage( package );
        }
    }
}

