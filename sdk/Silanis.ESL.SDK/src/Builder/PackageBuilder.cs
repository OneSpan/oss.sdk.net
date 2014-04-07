using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Silanis.ESL.SDK.Builder
{
	public class PackageBuilder
	{
		private readonly string packageName;
		private string description = String.Empty;
		private bool autocomplete = true;
		private Nullable<DateTime> expiryDate;
		private string emailMessage = String.Empty;
		private IDictionary<string, Signer> signers = new Dictionary<string, Signer> ();
        private IDictionary<string, Signer> placeholders = new Dictionary<string, Signer> ();
		private IDictionary<string, Document> documents = new Dictionary<string, Document>();
		private PackageId id;
		private DocumentPackageStatus status;
		private CultureInfo language;
        private DocumentPackageSettings settings;
        private SenderInfo senderInfo;
        private DocumentPackageAttributes attributes;

		private PackageBuilder(string packageName)
		{
			this.packageName = packageName;
		}

		internal PackageBuilder (Silanis.ESL.API.Package package)
		{
			this.id = new PackageId( package.Id );
			this.packageName = package.Name;
			this.autocomplete = package.Autocomplete;
			this.description = package.Description;
			this.expiryDate = package.Due;
			this.status = ConvertPackageStatus(package.Status);
			this.emailMessage = package.EmailMessage;
            this.settings = new DocumentPackageSettingsBuilder(package.Settings).build();
			this.senderInfo = new SenderConverter(package.Sender).ToSDKSenderInfo();
            this.attributes = new DocumentPackageAttributes(package.Data);

			foreach ( Silanis.ESL.API.Role role in package.Roles ) {
				if ( role.Signers.Count == 0 ) {
					WithSigner(SignerBuilder.NewSignerPlaceholderWithRoleId(new RoleId(role.Id)));   
				}
				else if (role.Signers[0].Group != null)
				{
					WithSigner(SignerBuilder.NewSignerFromGroup(new GroupId(role.Signers[0].Group.Id)));
				}
				else
				{
					WithSigner(SignerBuilder.NewSignerFromAPIRole(role).Build());
					if (role.Type == Silanis.ESL.API.RoleType.SENDER)
					{
						Silanis.ESL.API.Signer senderSigner = role.Signers[0];
						WithSenderInfo( SenderInfoBuilder.NewSenderInfo(senderSigner.Email)
							.WithName(senderSigner.FirstName, senderSigner.LastName)
							.WithCompany(senderSigner.Company)
							.WithTitle(senderSigner.Title));
					}
				}
			}

			foreach ( Silanis.ESL.API.Document apiDocument in package.Documents ) {
				Document document = DocumentBuilder.NewDocumentFromAPIDocument( apiDocument, package ).Build();

				WithDocument( document );
			}
		}

		private DocumentPackageStatus ConvertPackageStatus (Silanis.ESL.API.PackageStatus status)
		{
			switch (status)
			{
			case Silanis.ESL.API.PackageStatus.DRAFT:
				return DocumentPackageStatus.DRAFT;
			case Silanis.ESL.API.PackageStatus.SENT:
				return DocumentPackageStatus.SENT;
			case Silanis.ESL.API.PackageStatus.COMPLETED:
				return DocumentPackageStatus.COMPLETED;
			case Silanis.ESL.API.PackageStatus.ARCHIVED:
				return DocumentPackageStatus.ARCHIVED;
			case Silanis.ESL.API.PackageStatus.DECLINED:
				return DocumentPackageStatus.DECLINED;
			case Silanis.ESL.API.PackageStatus.OPTED_OUT:
				return DocumentPackageStatus.OPTED_OUT;
			case Silanis.ESL.API.PackageStatus.EXPIRED:
				return DocumentPackageStatus.EXPIRED;
			default:
				throw new EslException("Unknown Silanis.ESL.API.PackageStatus value: " + status);
			}
		}

		public static PackageBuilder NewPackageNamed (string name)
		{
			return new PackageBuilder (name);
		}

		public PackageBuilder DescribedAs(string description)
		{
			this.description = description;
			return this;
		}

		public PackageBuilder ExpiresOn (DateTime expiryDate)
		{
			this.expiryDate = expiryDate;
			return this;
		}

		public PackageBuilder WithLanguage (CultureInfo language)
		{
			this.language = language;
			return this;
		}

		public PackageBuilder WithEmailMessage (string emailMessage)
		{
			this.emailMessage = emailMessage;
			return this;
		}

		public PackageBuilder WithSigner (SignerBuilder builder)
		{
			return WithSigner (builder.Build());
		}

		public PackageBuilder WithSigner(Signer signer)
        {
            if (signer.IsPlaceholderSigner())
            {
                placeholders[signer.RoleId] = signer;
            }
			else if (signer.IsGroupSigner())
			{
				signers[signer.GroupId.Id] = signer;
			}
			else
			{
				signers[signer.Email] = signer;
			}
			return this;
		}

		public PackageBuilder WithDocument (DocumentBuilder builder)
		{
			return WithDocument (builder.Build());
		}

		public PackageBuilder WithDocument (Document document)
		{
			documents [document.Name] = document;
			return this;
		}

        public PackageBuilder WithSettings (DocumentPackageSettings settings)
        {
            this.settings = settings;
            return this;
        }

        public PackageBuilder WithSettings (DocumentPackageSettingsBuilder builder)
        {
            return WithSettings(builder.build());
        }

        public PackageBuilder WithSenderInfo( SenderInfoBuilder builder ) {
            return WithSenderInfo(builder.Build());
        }

        public PackageBuilder WithSenderInfo( SenderInfo senderInfo ) {
            this.senderInfo = senderInfo;
            return this;
        }

        public PackageBuilder withAttributes( DocumentPackageAttributes attributes) {
            this.attributes = attributes;
            return this;
        } 

		public DocumentPackage Build()
        {
            Support.LogMethodEntry();
            DocumentPackage package = new DocumentPackage(id, packageName, autocomplete, signers, placeholders, documents);

            package.Description = description;
            package.ExpiryDate = expiryDate;
            package.EmailMessage = emailMessage;
            package.Status = status;
            package.Language = language;
            package.Settings = settings;
            package.SenderInfo = senderInfo;
            if (attributes == null)
            {
                attributes = new DocumentPackageAttributes();
            }
            attributes.Append( "sdk", ".NET v" + CurrentVersion );
            package.Attributes = attributes;

			Support.LogMethodExit(package);
			return package;
		}
        
        public Version CurrentVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }  	
    }
    
}