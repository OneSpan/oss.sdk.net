using System;
using System.Collections.Generic;
using System.Globalization;

namespace Silanis.ESL.SDK
{
    public class DocumentPackage
    {

        public DocumentPackage(PackageId id, string packageName, bool autocomplete, IDictionary<string, Signer> signers, IDictionary<string, Signer> placeholders, IDictionary<string, Document> documents)
        {
            Id = id;
            Name = packageName;
            Autocomplete = autocomplete;
            Signers = signers;
            Documents = documents;
            Placeholders = placeholders;
        }

        public PackageId Id
        {
            get;
            set;
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

        public IDictionary<string, Signer> Placeholders
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

        public DocumentPackageSettings Settings
        {
            get;
            set;
        }

        public SenderInfo SenderInfo
        {
            get;
            set;
        }

        public DocumentPackageAttributes Attributes
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

            if (Settings != null)
            {
                package.Settings = Settings.toAPIPackageSettings();
            }

            if (SenderInfo != null)
            {
                package.Sender = new SenderConverter(SenderInfo).ToAPISender();
            }

            if (Attributes != null)
            {
                package.Data = Attributes.Contents;
            }

            int signerCount = 1;
            foreach (Signer signer in Signers.Values)
            {
                Silanis.ESL.API.Role role = ToAPIRole(signer, signerCount);
                package.AddRole(role);
                signerCount++;
            }
            foreach (Signer signer in Placeholders.Values)
            {
                Silanis.ESL.API.Role role = ToAPIRole(signer, signerCount);
                package.AddRole(role);
                signerCount++;
            }

            return package;
        }
        
        internal Silanis.ESL.API.Role ToAPIRole(Signer signer, int index)
        {
            Silanis.ESL.API.Role role = new Silanis.ESL.API.Role();

            if ( !signer.IsPlaceholderSigner() ) {
                role.AddSigner(signer.ToAPISigner());
            }
            role.Index = signer.SigningOrder;
            role.Reassign = signer.CanChangeSigner;

            if (String.IsNullOrEmpty(signer.RoleId))
            {
                role.Id = role.Name = "signer" + index;
            }
            else
            {
                role.Id = signer.RoleId;
                role.Name = signer.RoleId;
            }

            if (!String.IsNullOrEmpty(signer.Message))
            {
                Silanis.ESL.API.BaseMessage message = new Silanis.ESL.API.BaseMessage();

                message.Content = signer.Message;
                role.EmailMessage = message;
            }
            
            return role;
        }
    }
}