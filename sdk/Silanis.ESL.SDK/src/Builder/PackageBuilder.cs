using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Builder
{
	public class PackageBuilder
	{
		private readonly string packageName;
		private string description = String.Empty;
		private bool autocomplete = true;
		private Nullable<DateTime> expiryDate;
		private string emailMessage = String.Empty;
		private bool inPerson;
		private IDictionary<string, Signer> signers = new Dictionary<string, Signer> ();
		private IDictionary<string, Document> documents = new Dictionary<string, Document>();
		private PackageId id;
		private Silanis.ESL.API.PackageStatus status;

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
			this.status = package.Status;
			this.inPerson = package.Settings.Ceremony.InPerson;
			this.emailMessage = package.EmailMessage;

			foreach ( Silanis.ESL.API.Role role in package.Roles ) {
				if ( role.Signers.Count == 0 ) {
					continue;
				}

				Signer signer = SignerBuilder.NewSignerFromAPISigner( role ).Build();

				WithSigner( signer );
			}

			foreach ( Silanis.ESL.API.Document apiDocument in package.Documents ) {
				Document document = DocumentBuilder.NewDocumentFromAPIDocument( apiDocument, package ).Build();

				WithDocument( document );
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

		public PackageBuilder WithEmailMessage (string emailMessage)
		{
			this.emailMessage = emailMessage;
			return this;
		}

		public PackageBuilder InPerson (bool inPerson)
		{
			this.inPerson = inPerson;
			return this;
		}

		public PackageBuilder WithSigner (SignerBuilder builder)
		{
			return WithSigner (builder.Build());
		}

		public PackageBuilder WithSigner(Signer signer)
		{
			signers [signer.Email] = signer;
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

		public DocumentPackage Build ()
		{
			DocumentPackage package = new DocumentPackage (id, packageName, autocomplete, signers, documents);

			package.Description = description;
			package.ExpiryDate = expiryDate;
			package.EmailMessage = emailMessage;
			package.InPerson = inPerson;
			package.Status = status;
			return package;
		}
	}
}