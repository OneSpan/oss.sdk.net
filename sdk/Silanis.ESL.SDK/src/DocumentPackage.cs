using System;
using Silanis.ESL.API;
using System.Collections.Generic;
using System.Globalization;

namespace Silanis.ESL.SDK
{
	public class DocumentPackage
	{

		public DocumentPackage (PackageId id, string packageName, bool autocomplete, IDictionary<string, Signer> signers, IDictionary<string, Document> documents)
		{
			Id = id;
			Name = packageName;
			Autocomplete = autocomplete;
			Signers = signers;
			Documents = documents;
		}

		public PackageId Id {
			get;
			private set;
		}

		public Silanis.ESL.API.PackageStatus Status {
			get;
			set;
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

		public IDictionary<string, Document> Documents {
			get;
			private set;
		}

		public string Description {
			get;
			set;
		}

		public CultureInfo Language {
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

			if (Language != null)
			{
				package.Language = Language.TwoLetterISOLanguageName;
			}

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
				role.Index = signer.SigningOrder;
				role.Reassign = signer.CanChangeSigner;

				if (!String.IsNullOrEmpty(signer.Message))
				{
					BaseMessage message = new BaseMessage ();

					message.Content = signer.Message;
					role.EmailMessage = message;
				}

				package.AddRole (role);
				signerCount++;
			}

			return package;
		}
	}
}