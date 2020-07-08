using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class AccountConverterTest
    {
        private Account sdkAccount = null;
        private OneSpanSign.API.Account apiAccount = null;
        private AccountConverter converter = null;

        private static readonly string ACC_NAME = "account_name";
        private static readonly string ACC_ID = "account_id";
        private static readonly string ACC_OWNER = "account_owner";
        private static readonly string ACC_LOGOURL = "account_logoUrl";
        private static readonly DateTime ACC_CREATED = DateTime.Now;
        private static readonly DateTime ACC_UPDATED = DateTime.Now;

        private static readonly IDictionary<string, object> ACC_DATA = new Dictionary<string, object>()
            {{"account_data_0_key", "account_data_0_value"}};

        private static readonly string ACC_CO_NAME = "company_name";
        private static readonly string ACC_CO_ID = "company_id";

        private static readonly IDictionary<string, object> ACC_CO_DATA = new Dictionary<string, object>()
            {{"account_company_data_0_key", "account_company_data_0_value"}};

        private static readonly string ACC_CO_ADDR_ADDR1 = "company_address_address1";
        private static readonly string ACC_CO_ADDR_ADDR2 = "company_address_address2";
        private static readonly string ACC_CO_ADDR_CITY = "company_address_city";
        private static readonly string ACC_CO_ADDR_COUNTRY = "company_address_country";
        private static readonly string ACC_CO_ADDR_STATE = "company_address_state";
        private static readonly string ACC_CO_ADDR_ZIP = "company_address_zipcode";
        private static readonly string ACC_FIELD_ID = "field_0_id";
        private static readonly string ACC_FIELD_DEF_VLE = "field_0_default_value";
        private static readonly bool ACC_FIELD_IS_REQUIRED = true;
        private static readonly string ACC_FIELD_TRANSL_LANG = "en";
        private static readonly DateTime ACC_LIC_CREATED = DateTime.Now;
        private static readonly DateTime ACC_LIC_PAIDUNTIL = DateTime.Now;
        private static readonly string ACC_LIC_STATUS = "license_0_status";
        private static readonly DateTime ACC_LIC_TRANS_CREATED = DateTime.Now;
        private static readonly string ACC_LIC_TRANS_CC_CVV = "license_0_transaction_0_creditCard_cvv";
        private static readonly string ACC_LIC_TRANS_CC_NAME = "license_0_transaction_0_creditCard_name";
        private static readonly string ACC_LIC_TRANS_CC_NUM = "license_0_transaction_0_creditCard_number";
        private static readonly string ACC_LIC_TRANS_CC_TYPE = "license_0_transaction_0_creditCard_type";
        private static readonly Int32 ACC_LIC_TRANS_CC_EXP_MONTH = 12;
        private static readonly Int32 ACC_LIC_TRANS_CC_EXP_YEAR = 12;
        private static readonly Int32 ACC_LIC_TRANS_PRICE_AMOUNT = 1000;
        private static readonly string ACC_LIC_TRANS_PRICE_CURR_ID = "transaction_0_price_currency_id";
        private static readonly string ACC_LIC_TRANS_PRICE_CURR_NAME = "transaction_0_price_currency_name";

        private static readonly IDictionary<string, object> ACC_LIC_TRANS_PRICE_CURR_DATA =
            new Dictionary<string, object>()
                {{"transaction_0_price_currency_data_0_key", "transaction_0_price_currency_data_0_value"}};

        private static readonly string ACC_LIC_PLAN_NAME = "plan_name";
        private static readonly string ACC_LIC_PLAN_ID = "plan_id";
        private static readonly string ACC_LIC_PLAN_CONTRACT = "plan_contract";
        private static readonly string ACC_LIC_PLAN_DES = "plan_description";
        private static readonly string ACC_LIC_PLAN_GRP = "plan_group";
        private static readonly string ACC_LIC_PLAN_CYC = "plan_cycle";
        private static readonly string ACC_LIC_PLAN_ORI = "plan_original";
        private static readonly Int32 ACC_LIC_PLAN_CYC_COUNT = 1;
        private static readonly string ACC_LIC_PLAN_CYC_CYCLE = "plan_cycle_freeCycle";

        private static readonly IDictionary<string, object> ACC_LIC_PLAN_DATA = new Dictionary<string, object>()
            {{"plan_data_0_key", "plan_data_0_value"}};

        private static readonly IDictionary<string, object> ACC_LIC_PLAN_FEAT = new Dictionary<string, object>()
            {{"plan_feature_0_key", "plan_feature_0_value"}};

        private static readonly string ACC_LIC_PLAN_QUOTA_CYCLE = "quota_cycle";
        private static readonly Int32 ACC_LIC_PLAN_QUOTA_LIMIT = 1;
        private static readonly string ACC_LIC_PLAN_QUOTA_SCOPE = "quota_scope";
        private static readonly string ACC_LIC_PLAN_QUOTA_TARGET = "quota_target";
        private static readonly Int32 ACC_LIC_PLAN_PRICE_AMOUNT = 2000;
        private static readonly string ACC_LIC_PLAN_PRICE_CURR_ID = "plan_price_currency_id";
        private static readonly string ACC_LIC_PLAN_PRICE_CURR_NAME = "plan_price_currency_name";

        private static readonly IDictionary<string, object> ACC_LIC_PLAN_PRICE_CURR_DATA =
            new Dictionary<string, object>() {{"plan_price_data_0_key", "plan_price_data_0_value"}};

        private static readonly string ACC_PROV_DOC_ID = "doc_provider_id";
        private static readonly string ACC_PROV_USR_ID = "usr_provider_id";
        private static readonly string ACC_PROV_DOC_NAME = "doc_provider_name";
        private static readonly string ACC_PROV_USR_NAME = "usr_provider_name";
        private static readonly string ACC_PROV_DOC_PROVIDES = "doc_provider_provides";
        private static readonly string ACC_PROV_USR_PROVIDES = "usr_provider_provides";

        private static readonly IDictionary<string, object> ACC_PROV_DOC_DATA = new Dictionary<string, object>()
            {{"provider_doc_0_data_0_key", "provider_doc_0_data_0_value"}};

        private static readonly IDictionary<string, object> ACC_PROV_USR_DATA = new Dictionary<string, object>()
            {{"provider_usr_0_data_0_key", "provider_usr_0_data_0_value"}};

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiAccount = null;
            converter = new AccountConverter(apiAccount);
            Assert.IsNull(converter.ToSDKAccount());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkAccount = null;
            converter = new AccountConverter(sdkAccount);
            Assert.IsNull(converter.ToSDKAccount());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkAccount = CreateTypicalSDKAccount();
            converter = new AccountConverter(sdkAccount);
            Account account = converter.ToSDKAccount();

            Assert.IsNotNull(account);
            Assert.AreEqual(sdkAccount, account);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiAccount = CreateTypicalAPIAccount();
            converter = new AccountConverter(apiAccount);
            Account account = converter.ToSDKAccount();

            Assert.IsNotNull(account);
            Assert.AreEqual(ACC_NAME, account.Name);

            Assert.AreEqual(ACC_CO_ID, account.Company.Id);
            Assert.AreEqual(ACC_CO_ADDR_ADDR1, account.Company.Address.Address1);

            Assert.AreEqual(1, account.CustomFields.Count);
            Assert.AreEqual(ACC_FIELD_DEF_VLE, account.CustomFields[0].Value);
            Assert.AreEqual(1, account.CustomFields[0].Translations.Count);
            Assert.AreEqual(ACC_FIELD_TRANSL_LANG, account.CustomFields[0].Translations[0].Language);

            Assert.AreEqual(1, account.Licenses.Count);
            Assert.AreEqual(ACC_LIC_STATUS, account.Licenses[0].Status);
            Assert.AreEqual(1, account.Licenses[0].Transactions.Count);
            Assert.AreEqual(ACC_LIC_TRANS_CC_NUM, account.Licenses[0].Transactions[0].CreditCard.Number);
            Assert.AreEqual(ACC_LIC_TRANS_PRICE_AMOUNT, account.Licenses[0].Transactions[0].Price.Amount);
            Assert.AreEqual(ACC_LIC_PLAN_CONTRACT, account.Licenses[0].Plan.Contract);
            Assert.AreEqual(ACC_LIC_PLAN_PRICE_AMOUNT, account.Licenses[0].Plan.Price.Amount);

            Assert.AreEqual(1, account.Providers.Documents.Count);
            Assert.AreEqual(ACC_PROV_DOC_NAME, account.Providers.Documents[0].Name);
            Assert.AreEqual(1, account.Providers.Users.Count);
            Assert.AreEqual(ACC_PROV_USR_NAME, account.Providers.Users[0].Name);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkAccount = null;
            converter = new AccountConverter(sdkAccount);

            Assert.IsNull(converter.ToAPIAccount());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiAccount = null;
            converter = new AccountConverter(apiAccount);

            Assert.IsNull(converter.ToAPIAccount());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiAccount = CreateTypicalAPIAccount();
            converter = new AccountConverter(apiAccount);

            OneSpanSign.API.Account account = converter.ToAPIAccount();

            Assert.IsNotNull(account);
            Assert.AreEqual(apiAccount, account);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkAccount = CreateTypicalSDKAccount();
            converter = new AccountConverter(sdkAccount);

            OneSpanSign.API.Account account = converter.ToAPIAccount();

            Assert.IsNotNull(account);
            Assert.AreEqual(ACC_NAME, account.Name);

            Assert.AreEqual(ACC_CO_ID, account.Company.Id);
            Assert.AreEqual(ACC_CO_ADDR_ADDR1, account.Company.Address.Address1);

            Assert.AreEqual(1, account.CustomFields.Count);
            Assert.AreEqual(ACC_FIELD_DEF_VLE, account.CustomFields[0].Value);
            Assert.AreEqual(1, account.CustomFields[0].Translations.Count);
            Assert.AreEqual(ACC_FIELD_TRANSL_LANG, account.CustomFields[0].Translations[0].Language);

            Assert.AreEqual(1, account.Licenses.Count);
            Assert.AreEqual(ACC_LIC_STATUS, account.Licenses[0].Status);
            Assert.AreEqual(1, account.Licenses[0].Transactions.Count);
            Assert.AreEqual(ACC_LIC_TRANS_CC_NUM, account.Licenses[0].Transactions[0].CreditCard.Number);
            Assert.AreEqual(ACC_LIC_TRANS_PRICE_AMOUNT, account.Licenses[0].Transactions[0].Price.Amount);
            Assert.AreEqual(ACC_LIC_PLAN_CONTRACT, account.Licenses[0].Plan.Contract);
            Assert.AreEqual(ACC_LIC_PLAN_PRICE_AMOUNT, account.Licenses[0].Plan.Price.Amount);

            Assert.AreEqual(1, account.Providers.Documents.Count);
            Assert.AreEqual(ACC_PROV_DOC_NAME, account.Providers.Documents[0].Name);
            Assert.AreEqual(1, account.Providers.Users.Count);
            Assert.AreEqual(ACC_PROV_USR_NAME, account.Providers.Users[0].Name);
        }

        private Account CreateTypicalSDKAccount()
        {
            Account account = new Account();

            account.Name = ACC_NAME;
            account.Id = ACC_ID;
            account.Owner = ACC_OWNER;
            account.LogoUrl = ACC_LOGOURL;
            account.Data = ACC_DATA;
            account.Created = ACC_CREATED;
            account.Updated = ACC_UPDATED;

            Company company = new Company();
            company.Id = ACC_CO_ID;
            company.Name = ACC_CO_NAME;
            company.Data = ACC_CO_DATA;
            Address address = new Address();
            address.Address1 = ACC_CO_ADDR_ADDR1;
            address.Address2 = ACC_CO_ADDR_ADDR2;
            address.City = ACC_CO_ADDR_CITY;
            address.Country = ACC_CO_ADDR_COUNTRY;
            address.State = ACC_CO_ADDR_STATE;
            address.ZipCode = ACC_CO_ADDR_ZIP;
            company.Address = address;
            account.Company = company;

            CustomField customField = new CustomField();
            customField.Id = ACC_FIELD_ID;
            customField.Required = ACC_FIELD_IS_REQUIRED;
            customField.Value = ACC_FIELD_DEF_VLE;
            Translation translation = new Translation();
            translation.Language = ACC_FIELD_TRANSL_LANG;
            customField.AddTranslations(new List<Translation> {translation});
            account.AddCustomField(customField);

            License license = new License();
            license.Created = ACC_LIC_CREATED;
            license.Status = ACC_LIC_STATUS;
            license.PaidUntil = ACC_LIC_PAIDUNTIL;
            Transaction transaction = new Transaction();
            transaction.Created = ACC_LIC_TRANS_CREATED;
            CreditCard creditCard = new CreditCard();
            creditCard.Cvv = ACC_LIC_TRANS_CC_CVV;
            creditCard.Type = ACC_LIC_TRANS_CC_TYPE;
            creditCard.Name = ACC_LIC_TRANS_CC_NAME;
            creditCard.Number = ACC_LIC_TRANS_CC_NUM;
            CcExpiration ccExpiration = new CcExpiration();
            ccExpiration.Month = ACC_LIC_TRANS_CC_EXP_MONTH;
            ccExpiration.Year = ACC_LIC_TRANS_CC_EXP_YEAR;
            creditCard.Expiration = ccExpiration;
            transaction.CreditCard = creditCard;
            Price price = new Price();
            price.Amount = ACC_LIC_TRANS_PRICE_AMOUNT;
            Currency currency = new Currency();
            currency.Data = ACC_LIC_TRANS_PRICE_CURR_DATA;
            currency.Name = ACC_LIC_TRANS_PRICE_CURR_NAME;
            currency.Id = ACC_LIC_TRANS_PRICE_CURR_ID;
            price.Currency = currency;
            transaction.Price = price;
            license.AddTransaction(transaction);
            Plan plan = new Plan();
            plan.Contract = ACC_LIC_PLAN_CONTRACT;
            plan.Group = ACC_LIC_PLAN_GRP;
            plan.Original = ACC_LIC_PLAN_ORI;
            plan.Description = ACC_LIC_PLAN_DES;
            plan.Data = ACC_LIC_PLAN_DATA;
            plan.Cycle = ACC_LIC_PLAN_CYC;
            plan.Id = ACC_LIC_PLAN_ID;
            plan.Features = ACC_LIC_PLAN_FEAT;
            plan.Name = ACC_LIC_PLAN_NAME;
            CycleCount cycleCount = new CycleCount();
            cycleCount.Cycle = ACC_LIC_PLAN_CYC_CYCLE;
            cycleCount.Count = ACC_LIC_PLAN_CYC_COUNT;
            plan.FreeCycles = cycleCount;
            Quota quota = new Quota();
            quota.Target = ACC_LIC_PLAN_QUOTA_TARGET;
            quota.Limit = ACC_LIC_PLAN_QUOTA_LIMIT;
            quota.Cycle = ACC_LIC_PLAN_QUOTA_CYCLE;
            quota.Scope = ACC_LIC_PLAN_QUOTA_SCOPE;
            plan.AddQuota(quota);
            Price price1 = new Price();
            price1.Amount = ACC_LIC_PLAN_PRICE_AMOUNT;
            Currency currency1 = new Currency();
            currency1.Id = ACC_LIC_PLAN_PRICE_CURR_ID;
            currency1.Name = ACC_LIC_PLAN_PRICE_CURR_NAME;
            currency1.Data = ACC_LIC_PLAN_PRICE_CURR_DATA;
            price1.Currency = currency1;
            plan.Price = price1;
            license.Plan = plan;
            account.AddLicense(license);

            AccountProviders accountProviders = new AccountProviders();
            Provider provider = new Provider();
            provider.Id = ACC_PROV_DOC_ID;
            provider.Name = ACC_PROV_DOC_NAME;
            provider.Data = ACC_PROV_DOC_DATA;
            provider.Provides = ACC_PROV_DOC_PROVIDES;
            accountProviders.AddDocument(provider);
            Provider provider1 = new Provider();
            provider1.Id = ACC_PROV_USR_ID;
            provider1.Name = ACC_PROV_USR_NAME;
            provider1.Data = ACC_PROV_USR_DATA;
            provider1.Provides = ACC_PROV_USR_PROVIDES;
            accountProviders.AddUser(provider1);
            account.Providers = accountProviders;

            return account;
        }

        private OneSpanSign.API.Account CreateTypicalAPIAccount()
        {
            OneSpanSign.API.Account account = new OneSpanSign.API.Account();

            account.Name = ACC_NAME;
            account.Id = ACC_ID;
            account.Owner = ACC_OWNER;
            account.LogoUrl = ACC_LOGOURL;
            account.Data = ACC_DATA;
            account.Created = ACC_CREATED;
            account.Updated = ACC_UPDATED;

            OneSpanSign.API.Company company = new OneSpanSign.API.Company();
            company.Id = ACC_CO_ID;
            company.Name = ACC_CO_NAME;
            company.Data = ACC_CO_DATA;
            OneSpanSign.API.Address address = new OneSpanSign.API.Address();
            address.Address1 = ACC_CO_ADDR_ADDR1;
            address.Address2 = ACC_CO_ADDR_ADDR2;
            address.City = ACC_CO_ADDR_CITY;
            address.Country = ACC_CO_ADDR_COUNTRY;
            address.State = ACC_CO_ADDR_STATE;
            address.Zipcode = ACC_CO_ADDR_ZIP;
            company.Address = address;
            account.Company = company;

            OneSpanSign.API.CustomField customField = new OneSpanSign.API.CustomField();
            customField.Id = ACC_FIELD_ID;
            customField.Required = ACC_FIELD_IS_REQUIRED;
            customField.Value = ACC_FIELD_DEF_VLE;
            OneSpanSign.API.Translation translation = new OneSpanSign.API.Translation();
            translation.Language = ACC_FIELD_TRANSL_LANG;
            customField.AddTranslation(translation);
            account.AddCustomField(customField);

            OneSpanSign.API.License license = new OneSpanSign.API.License();
            license.Created = ACC_LIC_CREATED;
            license.Status = ACC_LIC_STATUS;
            license.PaidUntil = ACC_LIC_PAIDUNTIL;
            OneSpanSign.API.Transaction transaction = new OneSpanSign.API.Transaction();
            transaction.Created = ACC_LIC_TRANS_CREATED;
            OneSpanSign.API.CreditCard creditCard = new OneSpanSign.API.CreditCard();
            creditCard.Cvv = ACC_LIC_TRANS_CC_CVV;
            creditCard.Type = ACC_LIC_TRANS_CC_TYPE;
            creditCard.Name = ACC_LIC_TRANS_CC_NAME;
            creditCard.Number = ACC_LIC_TRANS_CC_NUM;
            OneSpanSign.API.CcExpiration ccExpiration = new OneSpanSign.API.CcExpiration();
            ccExpiration.Month = ACC_LIC_TRANS_CC_EXP_MONTH;
            ccExpiration.Year = ACC_LIC_TRANS_CC_EXP_YEAR;
            creditCard.Expiration = ccExpiration;
            transaction.CreditCard = creditCard;
            OneSpanSign.API.Price price = new OneSpanSign.API.Price();
            price.Amount = ACC_LIC_TRANS_PRICE_AMOUNT;
            OneSpanSign.API.Currency currency = new OneSpanSign.API.Currency();
            currency.Data = ACC_LIC_TRANS_PRICE_CURR_DATA;
            currency.Name = ACC_LIC_TRANS_PRICE_CURR_NAME;
            currency.Id = ACC_LIC_TRANS_PRICE_CURR_ID;
            price.Currency = currency;
            transaction.Price = price;
            license.AddTransaction(transaction);
            OneSpanSign.API.Plan plan = new OneSpanSign.API.Plan();
            plan.Contract = ACC_LIC_PLAN_CONTRACT;
            plan.Group = ACC_LIC_PLAN_GRP;
            plan.Original = ACC_LIC_PLAN_ORI;
            plan.Description = ACC_LIC_PLAN_DES;
            plan.Data = ACC_LIC_PLAN_DATA;
            plan.Cycle = ACC_LIC_PLAN_CYC;
            plan.Id = ACC_LIC_PLAN_ID;
            plan.Features = ACC_LIC_PLAN_FEAT;
            plan.Name = ACC_LIC_PLAN_NAME;
            OneSpanSign.API.CycleCount cycleCount = new OneSpanSign.API.CycleCount();
            cycleCount.Cycle = ACC_LIC_PLAN_CYC_CYCLE;
            cycleCount.Count = ACC_LIC_PLAN_CYC_COUNT;
            plan.FreeCycles = cycleCount;
            OneSpanSign.API.Quota quota = new OneSpanSign.API.Quota();
            quota.Target = ACC_LIC_PLAN_QUOTA_TARGET;
            quota.Limit = ACC_LIC_PLAN_QUOTA_LIMIT;
            quota.Cycle = ACC_LIC_PLAN_QUOTA_CYCLE;
            quota.Scope = ACC_LIC_PLAN_QUOTA_SCOPE;
            plan.AddQuota(quota);
            OneSpanSign.API.Price price1 = new OneSpanSign.API.Price();
            price1.Amount = ACC_LIC_PLAN_PRICE_AMOUNT;
            OneSpanSign.API.Currency currency1 = new OneSpanSign.API.Currency();
            currency1.Id = ACC_LIC_PLAN_PRICE_CURR_ID;
            currency1.Name = ACC_LIC_PLAN_PRICE_CURR_NAME;
            currency1.Data = ACC_LIC_PLAN_PRICE_CURR_DATA;
            price1.Currency = currency1;
            plan.Price = price1;
            license.Plan = plan;
            account.AddLicense(license);

            OneSpanSign.API.AccountProviders accountProviders = new OneSpanSign.API.AccountProviders();
            OneSpanSign.API.Provider provider = new OneSpanSign.API.Provider();
            provider.Id = ACC_PROV_DOC_ID;
            provider.Name = ACC_PROV_DOC_NAME;
            provider.Data = ACC_PROV_DOC_DATA;
            provider.Provides = ACC_PROV_DOC_PROVIDES;
            accountProviders.AddDocument(provider);
            OneSpanSign.API.Provider provider1 = new OneSpanSign.API.Provider();
            provider1.Id = ACC_PROV_USR_ID;
            provider1.Name = ACC_PROV_USR_NAME;
            provider1.Data = ACC_PROV_USR_DATA;
            provider1.Provides = ACC_PROV_USR_PROVIDES;
            accountProviders.AddUser(provider1);
            account.Providers = accountProviders;

            return account;
        }
    }
}