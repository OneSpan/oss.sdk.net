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

		private PackageBuilder(string packageName)
		{
			this.packageName = packageName;
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

		public DocumentPackage Build ()
		{
			DocumentPackage package = new DocumentPackage (packageName, autocomplete, signers);

			package.Description = description;
			package.ExpiryDate = expiryDate;
			package.EmailMessage = emailMessage;
			package.InPerson = inPerson;
			return package;
		}
	}
}