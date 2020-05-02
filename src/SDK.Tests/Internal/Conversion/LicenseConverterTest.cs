using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class LicenseConverterTest
    {
        private License sdkLicense = null;
        private OneSpanSign.API.License apiLicense = null;
        private LicenseConverter converter = null;

        private static readonly DateTime LIC_CREATED = DateTime.Now;
        private static readonly DateTime LIC_PAIDUNTIL = DateTime.Now;
        private static readonly string LIC_STATUS = "license_0_status";
        private static readonly DateTime LIC_TRANS_CREATED = DateTime.Now;
        private static readonly string LIC_TRANS_CC_CVV = "license_0_transaction_0_creditCard_cvv";
        private static readonly string LIC_TRANS_CC_NAME = "license_0_transaction_0_creditCard_name";
        private static readonly string LIC_TRANS_CC_NUM = "license_0_transaction_0_creditCard_number";
        private static readonly string LIC_TRANS_CC_TYPE = "license_0_transaction_0_creditCard_type";
        private static readonly Int32 LIC_TRANS_CC_EXP_MONTH = 12;
        private static readonly Int32 LIC_TRANS_CC_EXP_YEAR = 12;
        private static readonly Int32 LIC_TRANS_PRICE_AMOUNT = 1000;
        private static readonly string LIC_TRANS_PRICE_CURR_ID = "transaction_0_price_currency_id";
        private static readonly string LIC_TRANS_PRICE_CURR_NAME = "transaction_0_price_currency_name";

        private static readonly IDictionary<string, object> LIC_TRANS_PRICE_CURR_DATA =
            new Dictionary<string, object>()
                {{"transaction_0_price_currency_data_0_key", "transaction_0_price_currency_data_0_value"}};

        private static readonly string LIC_PLAN_NAME = "plan_name";
        private static readonly string LIC_PLAN_ID = "plan_id";
        private static readonly string LIC_PLAN_CONTRACT = "plan_contract";
        private static readonly string LIC_PLAN_DES = "plan_description";
        private static readonly string LIC_PLAN_GRP = "plan_group";
        private static readonly string LIC_PLAN_CYC = "plan_cycle";
        private static readonly string LIC_PLAN_ORI = "plan_original";
        private static readonly Int32 LIC_PLAN_CYC_COUNT = 1;
        private static readonly string LIC_PLAN_CYC_CYCLE = "plan_cycle_freeCycle";

        private static readonly IDictionary<string, object> LIC_PLAN_DATA = new Dictionary<string, object>()
            {{"plan_data_0_key", "plan_data_0_value"}};

        private static readonly IDictionary<string, object> LIC_PLAN_FEAT = new Dictionary<string, object>()
            {{"plan_feature_0_key", "plan_feature_0_value"}};

        private static readonly string LIC_PLAN_QUOTA_CYCLE = "quota_cycle";
        private static readonly Int32 LIC_PLAN_QUOTA_LIMIT = 1;
        private static readonly string LIC_PLAN_QUOTA_SCOPE = "quota_scope";
        private static readonly string LIC_PLAN_QUOTA_TARGET = "quota_target";
        private static readonly Int32 LIC_PLAN_PRICE_AMOUNT = 2000;
        private static readonly string LIC_PLAN_PRICE_CURR_ID = "plan_price_currency_id";
        private static readonly string LIC_PLAN_PRICE_CURR_NAME = "plan_price_currency_name";

        private static readonly IDictionary<string, object> LIC_PLAN_PRICE_CURR_DATA =
            new Dictionary<string, object>() {{"plan_price_data_0_key", "plan_price_data_0_value"}};

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiLicense = null;
            converter = new LicenseConverter(apiLicense);
            Assert.IsNull(converter.ToSDKLicense());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkLicense = null;
            converter = new LicenseConverter(sdkLicense);
            Assert.IsNull(converter.ToSDKLicense());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkLicense = CreateTypicalSDKLicense();
            converter = new LicenseConverter(sdkLicense);
            License license = converter.ToSDKLicense();

            Assert.IsNotNull(license);
            Assert.AreEqual(sdkLicense, license);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiLicense = CreateTypicalAPILicense();
            converter = new LicenseConverter(apiLicense);
            License license = converter.ToSDKLicense();

            Assert.IsNotNull(license);
            Assert.AreEqual(LIC_STATUS, license.Status);
            Assert.AreEqual(1, license.Transactions.Count);
            Assert.AreEqual(LIC_TRANS_CC_NUM, license.Transactions[0].CreditCard.Number);
            Assert.AreEqual(LIC_TRANS_PRICE_AMOUNT, license.Transactions[0].Price.Amount);
            Assert.AreEqual(LIC_PLAN_CONTRACT, license.Plan.Contract);
            Assert.AreEqual(LIC_PLAN_PRICE_AMOUNT, license.Plan.Price.Amount);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkLicense = null;
            converter = new LicenseConverter(sdkLicense);

            Assert.IsNull(converter.ToAPILicense());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiLicense = null;
            converter = new LicenseConverter(apiLicense);

            Assert.IsNull(converter.ToAPILicense());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiLicense = CreateTypicalAPILicense();
            converter = new LicenseConverter(apiLicense);

            OneSpanSign.API.License license = converter.ToAPILicense();

            Assert.IsNotNull(license);
            Assert.AreEqual(apiLicense, license);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkLicense = CreateTypicalSDKLicense();
            converter = new LicenseConverter(sdkLicense);

            OneSpanSign.API.License license = converter.ToAPILicense();

            Assert.IsNotNull(license);
            Assert.AreEqual(LIC_STATUS, license.Status);
            Assert.AreEqual(1, license.Transactions.Count);
            Assert.AreEqual(LIC_TRANS_CC_NUM, license.Transactions[0].CreditCard.Number);
            Assert.AreEqual(LIC_TRANS_PRICE_AMOUNT, license.Transactions[0].Price.Amount);
            Assert.AreEqual(LIC_PLAN_CONTRACT, license.Plan.Contract);
            Assert.AreEqual(LIC_PLAN_PRICE_AMOUNT, license.Plan.Price.Amount);
        }

        private License CreateTypicalSDKLicense()
        {
            License license = new License();

            license.Created = LIC_CREATED;
            license.Status = LIC_STATUS;
            license.PaidUntil = LIC_PAIDUNTIL;
            Transaction transaction = new Transaction();
            transaction.Created = LIC_TRANS_CREATED;
            CreditCard creditCard = new CreditCard();
            creditCard.Cvv = LIC_TRANS_CC_CVV;
            creditCard.Type = LIC_TRANS_CC_TYPE;
            creditCard.Name = LIC_TRANS_CC_NAME;
            creditCard.Number = LIC_TRANS_CC_NUM;
            CcExpiration ccExpiration = new CcExpiration();
            ccExpiration.Month = LIC_TRANS_CC_EXP_MONTH;
            ccExpiration.Year = LIC_TRANS_CC_EXP_YEAR;
            creditCard.Expiration = ccExpiration;
            transaction.CreditCard = creditCard;
            Price price = new Price();
            price.Amount = LIC_TRANS_PRICE_AMOUNT;
            Currency currency = new Currency();
            currency.Data = LIC_TRANS_PRICE_CURR_DATA;
            currency.Name = LIC_TRANS_PRICE_CURR_NAME;
            currency.Id = LIC_TRANS_PRICE_CURR_ID;
            price.Currency = currency;
            transaction.Price = price;
            license.AddTransaction(transaction);
            Plan plan = new Plan();
            plan.Contract = LIC_PLAN_CONTRACT;
            plan.Group = LIC_PLAN_GRP;
            plan.Original = LIC_PLAN_ORI;
            plan.Description = LIC_PLAN_DES;
            plan.Data = LIC_PLAN_DATA;
            plan.Cycle = LIC_PLAN_CYC;
            plan.Id = LIC_PLAN_ID;
            plan.Features = LIC_PLAN_FEAT;
            plan.Name = LIC_PLAN_NAME;
            CycleCount cycleCount = new CycleCount();
            cycleCount.Cycle = LIC_PLAN_CYC_CYCLE;
            cycleCount.Count = LIC_PLAN_CYC_COUNT;
            plan.FreeCycles = cycleCount;
            Quota quota = new Quota();
            quota.Target = LIC_PLAN_QUOTA_TARGET;
            quota.Limit = LIC_PLAN_QUOTA_LIMIT;
            quota.Cycle = LIC_PLAN_QUOTA_CYCLE;
            quota.Scope = LIC_PLAN_QUOTA_SCOPE;
            plan.AddQuota(quota);
            Price price1 = new Price();
            price1.Amount = LIC_PLAN_PRICE_AMOUNT;
            Currency currency1 = new Currency();
            currency1.Id = LIC_PLAN_PRICE_CURR_ID;
            currency1.Name = LIC_PLAN_PRICE_CURR_NAME;
            currency1.Data = LIC_PLAN_PRICE_CURR_DATA;
            price1.Currency = currency1;
            plan.Price = price1;
            license.Plan = plan;


            return license;
        }

        private OneSpanSign.API.License CreateTypicalAPILicense()
        {
            OneSpanSign.API.License license = new OneSpanSign.API.License();
            license.Created = LIC_CREATED;
            license.Status = LIC_STATUS;
            license.PaidUntil = LIC_PAIDUNTIL;
            OneSpanSign.API.Transaction transaction = new OneSpanSign.API.Transaction();
            transaction.Created = LIC_TRANS_CREATED;
            OneSpanSign.API.CreditCard creditCard = new OneSpanSign.API.CreditCard();
            creditCard.Cvv = LIC_TRANS_CC_CVV;
            creditCard.Type = LIC_TRANS_CC_TYPE;
            creditCard.Name = LIC_TRANS_CC_NAME;
            creditCard.Number = LIC_TRANS_CC_NUM;
            OneSpanSign.API.CcExpiration ccExpiration = new OneSpanSign.API.CcExpiration();
            ccExpiration.Month = LIC_TRANS_CC_EXP_MONTH;
            ccExpiration.Year = LIC_TRANS_CC_EXP_YEAR;
            creditCard.Expiration = ccExpiration;
            transaction.CreditCard = creditCard;
            OneSpanSign.API.Price price = new OneSpanSign.API.Price();
            price.Amount = LIC_TRANS_PRICE_AMOUNT;
            OneSpanSign.API.Currency currency = new OneSpanSign.API.Currency();
            currency.Data = LIC_TRANS_PRICE_CURR_DATA;
            currency.Name = LIC_TRANS_PRICE_CURR_NAME;
            currency.Id = LIC_TRANS_PRICE_CURR_ID;
            price.Currency = currency;
            transaction.Price = price;
            license.AddTransaction(transaction);
            OneSpanSign.API.Plan plan = new OneSpanSign.API.Plan();
            plan.Contract = LIC_PLAN_CONTRACT;
            plan.Group = LIC_PLAN_GRP;
            plan.Original = LIC_PLAN_ORI;
            plan.Description = LIC_PLAN_DES;
            plan.Data = LIC_PLAN_DATA;
            plan.Cycle = LIC_PLAN_CYC;
            plan.Id = LIC_PLAN_ID;
            plan.Features = LIC_PLAN_FEAT;
            plan.Name = LIC_PLAN_NAME;
            OneSpanSign.API.CycleCount cycleCount = new OneSpanSign.API.CycleCount();
            cycleCount.Cycle = LIC_PLAN_CYC_CYCLE;
            cycleCount.Count = LIC_PLAN_CYC_COUNT;
            plan.FreeCycles = cycleCount;
            OneSpanSign.API.Quota quota = new OneSpanSign.API.Quota();
            quota.Target = LIC_PLAN_QUOTA_TARGET;
            quota.Limit = LIC_PLAN_QUOTA_LIMIT;
            quota.Cycle = LIC_PLAN_QUOTA_CYCLE;
            quota.Scope = LIC_PLAN_QUOTA_SCOPE;
            plan.AddQuota(quota);
            OneSpanSign.API.Price price1 = new OneSpanSign.API.Price();
            price1.Amount = LIC_PLAN_PRICE_AMOUNT;
            OneSpanSign.API.Currency currency1 = new OneSpanSign.API.Currency();
            currency1.Id = LIC_PLAN_PRICE_CURR_ID;
            currency1.Name = LIC_PLAN_PRICE_CURR_NAME;
            currency1.Data = LIC_PLAN_PRICE_CURR_DATA;
            price1.Currency = currency1;
            plan.Price = price1;
            license.Plan = plan;

            return license;
        }
    }
}