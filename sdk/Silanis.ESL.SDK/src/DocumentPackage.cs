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
            Status = DocumentPackageStatus.DRAFT;
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
            set;
        }

        public bool Autocomplete
        {
            get;
            set;
        }

        private IDictionary<string, Signer> signers = new Dictionary<string, Signer>(StringComparer.OrdinalIgnoreCase);
        public IDictionary<string, Signer> Signers
        {
            get{ return signers;}
            set{ signers = value;}
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

        public Nullable<DateTime> UpdatedDate
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

        public IList<Message> Messages
        {
            get;
            set;
        }
    }
}