using System;
using System.Collections.Generic;
using System.Globalization;

namespace Silanis.ESL.SDK
{
    public class DocumentPackage
    {

        public DocumentPackage(PackageId id, string packageName, bool autocomplete, IDictionary<string, Signer> signers, IDictionary<string, Document> documents)
        {
            Id = id;
            Name = packageName;
            Autocomplete = autocomplete;
            Signers = signers;
            Documents = documents;
        }

        public PackageId Id
        {
            get;
            private set;
        }

        public DocumentPackageStatus Status
        {
            get;
            set;
        }

        public string Name
        {
            get;
            private set;
        }

        public bool Autocomplete
        {
            get;
            private set;
        }

        public IDictionary<string, Signer> Signers
        {
            get;
            private set;
        }

        public IDictionary<string, Document> Documents
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            set;
        }

        public CultureInfo Language
        {
            get;
            set;
        }

        public Nullable<DateTime> ExpiryDate
        {
            get;
            set;
        }

        public string EmailMessage
        {
            get;
            set;
        }

        public bool InPerson
        {
            get;
            set;
        }

        public DocumentPackageSettings Settings
        {
            get;
            set;
        }

        internal Silanis.ESL.API.Package ToAPIPackage()
        {
            Silanis.ESL.API.Package package = new Silanis.ESL.API.Package();

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
                Silanis.ESL.API.PackageSettings settings = new Silanis.ESL.API.PackageSettings();
                Silanis.ESL.API.CeremonySettings ceremonySettings = new Silanis.ESL.API.CeremonySettings();

                ceremonySettings.InPerson = InPerson;
                settings.Ceremony = ceremonySettings;
                package.Settings = settings;
            }

            int signerCount = 1;
            foreach (Signer signer in Signers.Values)
            {
                Silanis.ESL.API.Role role = new Silanis.ESL.API.Role();

                role.Id = "role" + signerCount;
                role.Name = "signer" + signerCount;
                role.AddSigner(signer.ToAPISigner());
                role.Index = signer.SigningOrder;
                role.Reassign = signer.CanChangeSigner;

                if (!String.IsNullOrEmpty(signer.Message))
                {
                    Silanis.ESL.API.BaseMessage message = new Silanis.ESL.API.BaseMessage();

                    message.Content = signer.Message;
                    role.EmailMessage = message;
                }

                package.AddRole(role);
                signerCount++;
            }

            return package;
        }
    }
}