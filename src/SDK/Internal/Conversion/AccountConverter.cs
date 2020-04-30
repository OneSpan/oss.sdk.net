using System;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class AccountConverter
    {
        private Account sdkAccount;
        private OneSpanSign.API.Account apiAccount;

        public AccountConverter( Account sdkAccount )
        {
            this.sdkAccount = sdkAccount;
        }

        public AccountConverter( OneSpanSign.API.Account apiAccount ) 
        {
            this.apiAccount = apiAccount;
        }

        public Account ToSDKAccount() {
            if (sdkAccount != null)
            {
                return sdkAccount;
            }
            else if (apiAccount != null)
            {
                AccountBuilder builder = AccountBuilder.NewAccount()
                    .WithCompany(new CompanyConverter(apiAccount.Company).ToSDKCompany())
                    .CreatedOn(apiAccount.Created)
                    .UpdatedOn(apiAccount.Updated)
                    .WithData(apiAccount.Data)
                    .WithId(apiAccount.Id)
                    .WithLogoUrl(apiAccount.LogoUrl)
                    .WithOwner(apiAccount.Owner)
                    .WithName(apiAccount.Name)
                    .WithAccountProviders(new AccountProvidersConverter(apiAccount.Providers).ToSDKAccountProviders());
                foreach (API.CustomField field in apiAccount.CustomFields)
                {
                    builder.WithCustomField(new CustomFieldConverter(field).ToSDKCustomField());
                }

                foreach (API.License license in apiAccount.Licenses)
                {
                    builder.WithLicense(new LicenseConverter(license).ToSDKLicense());
                }
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.Account ToAPIAccount() {
            if (apiAccount != null)
            {
                return apiAccount;
            }
            else if (sdkAccount != null)
            {
                OneSpanSign.API.Account result = new OneSpanSign.API.Account();
                result.Company = new CompanyConverter(sdkAccount.Company).ToAPICompany();
                result.Created = sdkAccount.Created;
                result.Updated = sdkAccount.Updated;
                result.Data = sdkAccount.Data;
                result.Id = sdkAccount.Id;
                result.Name = sdkAccount.Name;
                result.Owner = sdkAccount.Owner;
                result.LogoUrl = sdkAccount.LogoUrl;
                result.Providers = new AccountProvidersConverter(sdkAccount.Providers).ToAPIAccountProviders();
                foreach (License license in sdkAccount.Licenses)
                {
                    result.AddLicense(new LicenseConverter(license).ToAPILicense());
                }
                foreach (CustomField field in sdkAccount.CustomFields)
                {
                    result.AddCustomField(new CustomFieldConverter(field).ToAPICustomField());
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

