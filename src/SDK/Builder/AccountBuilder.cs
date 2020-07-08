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
        private string logoUrl;
        private string name;
        private string owner;
        private AccountProviders providers;

        private AccountBuilder()
        {
            customFields = new List<CustomField>();
            licenses = new List<License>();
            data = new Dictionary<string, object>();
        }

        public static AccountBuilder NewAccount()
        {
            return new AccountBuilder();
        }

        public AccountBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public AccountBuilder WithOwner(string owner)
        {
            this.owner = owner;
            return this;
        }

        public AccountBuilder WithLogoUrl(string logoUrl)
        {
            this.logoUrl = logoUrl;
            return this;
        }

        public AccountBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public AccountBuilder WithCompany(Company company)
        {
            this.company = company;
            return this;
        }

        public AccountBuilder CreatedOn(Nullable<DateTime> created)
        {
            this.created = created;
            return this;
        }

        public AccountBuilder UpdatedOn(Nullable<DateTime> updated)
        {
            this.updated = updated;
            return this;
        }

        public AccountBuilder WithCustomField(CustomField customField)
        {
            this.customFields.Add(customField);
            return this;
        }


        public AccountBuilder WithLicense(License license)
        {
            licenses.Add(license);
            return this;
        }

        public AccountBuilder WithAccountProviders(AccountProviders providers)
        {
            this.providers = providers;
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

        public AccountBuilder WithData(IDictionary<string, object> data)
        {
            this.data = data;
            return this;
        }

        public Account Build()
        {
            Account account = new Account();
            account.Company = company;
            account.Created = created;
            account.Updated = updated;
            foreach (CustomField field in customFields)
            {
                account.CustomFields.Add(field);
            }

            account.Data = data;
            account.Id = id;
            foreach (License license in licenses)
            {
                account.Licenses.Add(license);
            }

            account.LogoUrl = logoUrl;
            account.Name = name;
            account.Owner = owner;
            account.Providers = providers;
            return account;
        }
    }
}