using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
    public class AccountBuilder
    {
        private Company company;
        private Nullable<DateTime> created;
        private Nullable<DateTime> updated;
        private IList<CustomField> customFields;
        private IDictionary<string, object> data;
        private string id;
        private IList<License> licenses;
        private string LogoUrl;
        private string name;
        private string owner;
        private AccountProviders providers;

        private AccountBuilder()
        {
            customFields = new List<CustomField>();
            licenses = new List<License>();
            data = new Dictionary<string, object>();
        }

        public static AccountBuilder NewAccount() {
            return new AccountBuilder();
        }

        public AccountBuilder WithName( string value ) {
            name = value;
            return this;
        }

        public AccountBuilder WithOwner( string value ) {
            owner = value;
            return this;
        }

        public AccountBuilder WithLogoUrl( string value ) {
            LogoUrl = value;
            return this;
        }

        public AccountBuilder WithId( string value ) {
            id = value;
            return this;
        }

        public AccountBuilder WithCompany(Company value)
        {
            company = value;
            return this;
        }
        
        public AccountBuilder CreatedOn( Nullable<DateTime> value ) {
            created = value;
            return this;
        }

        public AccountBuilder UpdatedOn( Nullable<DateTime> value ) {
            updated = value;
            return this;
        }
        
        public AccountBuilder WithCustomField( CustomField value ) {
            customFields.Add(value);
            return this;
        }
        

        public AccountBuilder WithLicense(License value)
        {
            licenses.Add(value);
            return this;
        }

        public AccountBuilder WithAccountProviders(AccountProviders value)
        {
            providers = value;
            return this;
        }

        public AccountBuilder WithAccountProviders(IList<Provider> documents, IList<Provider> users)
        {
            providers = new AccountProviders();
            foreach (Provider provider in documents)
            {
                providers.Documents.Add(provider);
            }
            foreach (Provider provider in users)
            {
                providers.Users.Add(provider);
            }

            return this;
        }

        public AccountBuilder WithData(IDictionary<string, object> value)
        {
            data = value;
            return this;
        }
        
        public Account Build() {
            Account account = new Account();
            account.Company = company;
            account.Created = created;
            account.Updated = updated;
            foreach( CustomField field in customFields ) {
                account.CustomFields.Add( field );
            }
            account.Data = data;
            account.Id = id;
            foreach (License license in licenses)
            {
                account.Licenses.Add(license);
            }

            account.LogoUrl = LogoUrl;
            account.Name = name;
            account.Owner = owner;
            account.Providers = providers;
            return account;
        }
    }
}