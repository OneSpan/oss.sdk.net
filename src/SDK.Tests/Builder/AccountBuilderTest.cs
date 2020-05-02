using System;
using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountBuilderTest
    {
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

        [Test]
        public void withSpecifiedValues()
        {
            AccountBuilder accountBuilder = AccountBuilder.NewAccount()
                .WithName(ACC_NAME)
                .WithId(ACC_ID)
                .WithOwner(ACC_OWNER)
                .WithLogoUrl(ACC_LOGOURL)
                .WithData(ACC_DATA)
                .WithCompany(CompanyBuilder.NewCompany(ACC_CO_NAME)
                    .WithAddress(AddressBuilder.NewAddress()
                        .WithAddress1(ACC_CO_ADDR_ADDR1)
                        .WithAddress2(ACC_CO_ADDR_ADDR2)
                        .WithCity(ACC_CO_ADDR_CITY)
                        .WithCountry(ACC_CO_ADDR_COUNTRY)
                        .WithState(ACC_CO_ADDR_STATE)
                        .WithZipCode(ACC_CO_ADDR_ZIP).Build())
                    .WithId(ACC_CO_ID)
                    .WithData(ACC_CO_DATA)
                    .Build())
                .WithCustomField(CustomFieldBuilder.CustomFieldWithId(ACC_FIELD_ID)
                    .WithDefaultValue(ACC_FIELD_DEF_VLE)
                    .IsRequired(ACC_FIELD_IS_REQUIRED)
                    .WithTranslation(TranslationBuilder.NewTranslation(ACC_FIELD_TRANSL_LANG).Build())
                    .Build())
                .WithLicense(LicenseBuilder.NewLicense()
                    .CreatedOn(ACC_LIC_CREATED)
                    .WithPaidUntil(ACC_LIC_PAIDUNTIL)
                    .WithStatus(ACC_LIC_STATUS)
                    .WithTransaction(ACC_LIC_TRANS_CREATED,
                        CreditCardBuilder.NewCreditCard()
                            .WithCvv(ACC_LIC_TRANS_CC_CVV)
                            .WithName(ACC_LIC_TRANS_CC_NAME)
                            .WithNumber(ACC_LIC_TRANS_CC_NUM)
                            .WithType(ACC_LIC_TRANS_CC_TYPE)
                            .WithExpiration(ACC_LIC_TRANS_CC_EXP_MONTH, ACC_LIC_TRANS_CC_EXP_YEAR)
                            .Build(),
                        PriceBuilder.NewPrice()
                            .WithAmount(ACC_LIC_TRANS_PRICE_AMOUNT)
                            .WithCurrency(ACC_LIC_TRANS_PRICE_CURR_ID, ACC_LIC_TRANS_PRICE_CURR_NAME,
                                ACC_LIC_TRANS_PRICE_CURR_DATA)
                            .Build())
                    .WithPlan(PlanBuilder.NewPlan(ACC_LIC_PLAN_ID)
                        .WithId(ACC_LIC_PLAN_NAME)
                        .WithContract(ACC_LIC_PLAN_CONTRACT)
                        .WithDescription(ACC_LIC_PLAN_DES)
                        .WithGroup(ACC_LIC_PLAN_GRP)
                        .WithCycle(ACC_LIC_PLAN_CYC)
                        .WithOriginal(ACC_LIC_PLAN_ORI)
                        .WithData(ACC_LIC_PLAN_DATA)
                        .WithFreeCycles(ACC_LIC_PLAN_CYC_COUNT, ACC_LIC_PLAN_CYC_CYCLE)
                        .WithQuota(ACC_LIC_PLAN_QUOTA_CYCLE, ACC_LIC_PLAN_QUOTA_LIMIT, ACC_LIC_PLAN_QUOTA_SCOPE,
                            ACC_LIC_PLAN_QUOTA_TARGET)
                        .WithFeatures(ACC_LIC_PLAN_FEAT)
                        .WithPrice(PriceBuilder.NewPrice()
                            .WithAmount(ACC_LIC_PLAN_PRICE_AMOUNT)
                            .WithCurrency(ACC_LIC_PLAN_PRICE_CURR_ID, ACC_LIC_PLAN_PRICE_CURR_NAME,
                                ACC_LIC_PLAN_PRICE_CURR_DATA)
                            .Build())
                        .Build())
                    .Build())
                .WithAccountProviders(new List<Provider>()
                {
                    ProviderBuilder.NewProvider(ACC_PROV_DOC_NAME)
                        .WithData(ACC_PROV_DOC_DATA)
                        .WithId(ACC_PROV_DOC_ID)
                        .WithProvides(ACC_PROV_DOC_NAME)
                        .Build()
                }, new List<Provider>()
                {
                    ProviderBuilder.NewProvider(ACC_PROV_USR_NAME)
                        .WithData(ACC_PROV_USR_DATA)
                        .WithId(ACC_PROV_USR_ID)
                        .WithProvides(ACC_PROV_USR_PROVIDES)
                        .Build()
                });

            Account account = accountBuilder.Build();

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
    }
}