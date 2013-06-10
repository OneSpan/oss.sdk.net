using System;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class DocumentPackage
	{

		public DocumentPackage (string packageName, bool autocomplete, IDictionary<string, Signer> signers)
		{
			Name = packageName;
			Autocomplete = autocomplete;
			Signers = signers;
		}

		public string Name {
			get;
			private set;
		}

		public bool Autocomplete {
			get;
			private set;
		}

		public IDictionary<string, Signer> Signers {
			get;
			private set;
		}

		public string Description {
			get;
			set;
		}

		public Nullable<DateTime> ExpiryDate {
			get;
			set;
		}

		public string EmailMessage {
			get;
			set;
		}

		public bool InPerson {
			get;
			set;
		}

		internal Package ToAPIPackage ()
		{
			Package package = new Package ();

			package.Name = Name;
			package.Description = Description;
			package.Autocomplete = Autocomplete;
			package.Due = ExpiryDate;
			package.EmailMessage = EmailMessage;

			if (InPerson)
			{
				PackageSettings settings = new PackageSettings ();
				CeremonySettings ceremonySettings = new CeremonySettings ();

				ceremonySettings.InPerson = InPerson;
				settings.Ceremony = ceremonySettings;
				package.Settings = settings;
			}

			int signerCount = 1;
			foreach (Signer signer in Signers.Values)
			{
				Role role = new Role ();

				role.Id = "role" + signerCount;
				role.Name = "signer" + signerCount;
				role.AddSigner (signer.ToAPISigner());

				package.AddRole (role);
				signerCount++;
			}

			return package;
		}
	}
}