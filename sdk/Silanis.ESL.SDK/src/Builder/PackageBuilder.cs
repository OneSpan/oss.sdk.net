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
		private IDictionary<string, Signer> signers = new Dictionary<string, Signer>(StringComparer.OrdinalIgnoreCase);
        private IDictionary<string, Signer> placeholders = new Dictionary<string, Signer> ();
		private IDictionary<string, Document> documents = new Dictionary<string, Document>();
		private PackageId id;
		private DocumentPackageStatus status = DocumentPackageStatus.DRAFT;
		private CultureInfo language;
        private DocumentPackageSettings settings;
        private SenderInfo senderInfo;
        private DocumentPackageAttributes attributes;
        private IList<Message> messages = new List<Message>();

		private PackageBuilder(string packageName)
		{
			this.packageName = packageName;
		}

		public static PackageBuilder NewPackageNamed (string name)
		{
			return new PackageBuilder (name);
		}

        [Obsolete("Please do not use WithID() from now on. Will get deleted in a future release.")]
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

        public PackageBuilder ExpiresOn (Nullable<DateTime> expiryDate)
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
            package.Messages = messages;

			return package;
		}
        
    }
    
}