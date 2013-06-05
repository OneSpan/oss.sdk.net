using System;

namespace Silanis.ESL.SDK
{
	public class DocumentPackage
	{

		public DocumentPackage(string name, bool autocomplete)
		{
			Name = name;
			Autocomplete = autocomplete;
		}

		public string Name {
			get;
			private set;
		}

		public bool Autocomplete {
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

			return package;
		}
	}
}