using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Silanis.ESL.API;

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
			this.status = new PackageStatusConverter(new Nullable<PackageStatus>(package.Status)).ToSDKPackageStatus().Value;
			this.emailMessage = package.EmailMessage;
            this.settings = new DocumentPackageSettingsBuilder(package.Settings).build();
			this.senderInfo = new SenderConverter(package.Sender).ToSDKSenderInfo();
            this.attributes = new DocumentPackageAttributes(package.Data);

			foreach ( Silanis.ESL.API.Role role in package.Roles ) {
				if ( role.Signers.Count == 0 ) {
					WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder(role.Id)));   
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
				Document document = new DocumentConverter( apiDocument, package ).ToSDKDocument();

				WithDocument( document );
			}
		}

		public static PackageBuilder NewPackageNamed (string name)
		{
			return new PackageBuilder (name);
		}

		public PackageBuilder WithID(PackageId id)
		{
			this.id = id;
			return this;
		}


        public PackageBuilder WithAutomaticCompletion()
        {
            this.autocomplete = true;
            return this;
        }
        
        public PackageBuilder WithoutAutomaticCompletion()
        {
            this.autocomplete = false;
            return this;
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
                placeholders[signer.Id] = signer;
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
        
		public PackageBuilder WithStatus (DocumentPackageStatus status) {
			this.status = status;
			return this;
		}

        public PackageBuilder WithAttributes(DocumentPackageAttributes attributes)
        {
            this.attributes = attributes;
            return this;
        }

        public PackageBuilder WithAttributes(DocumentPackageAttributesBuilder attributesBuilder)
        {
            return WithAttributes( attributesBuilder.Build() );
        }

        [Obsolete("Use WithAttributes() instead.  Notice the uppercase W.")]
        public PackageBuilder withAttributes( DocumentPackageAttributes attributes) {
            return WithAttributes( attributes );
        } 

		public DocumentPackage Build()
        {
            DocumentPackage package = new DocumentPackage(id, packageName, autocomplete, signers, placeholders, documents);
            package.Description = description;
            package.ExpiryDate = expiryDate;
            package.EmailMessage = emailMessage;
            package.Status = status;
            package.Language = language;
            package.Settings = settings;
            package.SenderInfo = senderInfo;
            package.Attributes = attributes;

			return package;
		}
        
    }
    
}