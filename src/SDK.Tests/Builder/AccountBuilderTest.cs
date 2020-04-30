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
        [Test]
        public void withSpecifiedValues ()
        {
            AccountBuilder accountBuilder = AccountBuilder.NewAccount()
                .WithName("account_name")
                .WithId("account_id")
                .WithOwner("account_owner")
                .WithLogoUrl("account_logoUrl")
                .WithData(new Dictionary<string, object>() {{"account_data_key1", "account_data_value1"}})
                .WithCompany(CompanyBuilder.NewCompany("company_name")
                    .WithAddress(AddressBuilder.NewAddress()
                        .WithAddress1("company_address_address1")
                        .WithAddress2("company_address_address2")
                        .WithCity("company_address_city")
                        .WithCountry("company_address_country")
                        .WithState("company_address_state")
                        .WithZipCode("company_address_zipCode").Build())
                    .WithId("company_id")
                    .WithData(new Dictionary<string, object>() {{"company_data_key", "company_data_value"}})
                    .Build())
                .WithCustomField(CustomFieldBuilder.CustomFieldWithId("field_id")
                    .WithDefaultValue("field_default_value")
                    .IsRequired(true)
                    .WithTranslation(TranslationBuilder.NewTranslation("en").Build())
                    .Build())
                .WithLicense(LicenseBuilder.NewLicense()
                    .CreatedOn(DateTime.Now)
                    .WithPaidUntil(DateTime.MaxValue)
                    .WithStatus("license_status")
                    .WithTransaction(DateTime.Now,
                        CreditCardBuilder.NewCreditCard()
                            .WithCvv("cvv")
                            .WithName("creditCard_name")
                            .WithNumber("12345")
                            .WithType("visa")
                            .WithExpiration(12, 12)
                            .Build(),
                        PriceBuilder.NewPrice()
                            .WithAmount(1000)
                            .WithCurrency("currency_id", "currency_name",
                                new Dictionary<string, object>() {{"currency_data_key", "currency_data_value"}})
                            .Build())
                    .WithPlan(PlanBuilder.NewPlan("plan_name")
                        .WithId("plan_id")
                        .WithContract("plan_contract")
                        .WithDescription("plan_description")
                        .WithGroup("plan_group")
                        .WithCycle("plan_cycle")
                        .WithOriginal("original")
                        .WithData(new Dictionary<string, object>() {{"plan_data_key", "plan_data_value"}})
                        .WithFreeCycles(1, "free_cycle")
                        .WithQuota("quota_cycle", 1, "quota_scope", "quota_target")
                        .WithFeatures(new Dictionary<string, object>() {{"feature_key", "feature_value"}})
                        .WithPrice(PriceBuilder.NewPrice()
                            .WithAmount(2000)
                            .WithCurrency("plan_price_currency_id", "plan_price_currency_name",
                                new Dictionary<string, object>()
                                    {{"plan_price_currency_data_key", "plan_price_currency_data_value"}})
                            .Build())
                        .Build())
                    .Build())
                .WithAccountProviders(new List<Provider>()
                {
                    ProviderBuilder.NewProvider("doc_provider_name")
                        .WithData(new Dictionary<string, object>()
                            {{"doc_provider_data_key", "doc_provider_data_value"}})
                        .WithId("doc_provider_id")
                        .WithProvides("doc_provides")
                        .Build()
                }, new List<Provider>()
                {
                    ProviderBuilder.NewProvider("user_provider_name")
                        .WithData(new Dictionary<string, object>()
                            {{"usr_provider_data_key", "usr_provider_data_value"}})
                        .WithId("usr_provider_id")
                        .WithProvides("usr_provides")
                        .Build()
                });

            Account account = accountBuilder.Build();
            
            Assert.AreEqual("account_name", account.Name);
            Assert.AreEqual("account_data_value1", account.Data["account_data_key1"]);
            Assert.AreEqual("company_id", account.Company.Id);
            Assert.AreEqual(1, account.CustomFields.Count);
            Assert.AreEqual(1, account.Licenses.Count);
        }
    }
}