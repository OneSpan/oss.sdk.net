using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class PlanConverterTest
    {
        private Plan sdkPlan = null;
        private OneSpanSign.API.Plan apiPlan = null;
        private PlanConverter converter = null;

        private static readonly string PLAN_NAME = "plan_name";
        private static readonly string PLAN_ID = "plan_id";
        private static readonly string PLAN_CONTRACT = "plan_contract";
        private static readonly string PLAN_DES = "plan_description";
        private static readonly string PLAN_GRP = "plan_group";
        private static readonly string PLAN_CYC = "plan_cycle";
        private static readonly string PLAN_ORI = "plan_original";
        private static readonly Int32 PLAN_CYC_COUNT = 1;
        private static readonly string PLAN_CYC_CYCLE = "plan_cycle_freeCycle";

        private static readonly IDictionary<string, object> PLAN_DATA = new Dictionary<string, object>()
            {{"plan_data_0_key", "plan_data_0_value"}};

        private static readonly IDictionary<string, object> PLAN_FEAT = new Dictionary<string, object>()
            {{"plan_feature_0_key", "plan_feature_0_value"}};

        private static readonly string PLAN_QUOTA_CYCLE = "quota_cycle";
        private static readonly Int32 PLAN_QUOTA_LIMIT = 1;
        private static readonly string PLAN_QUOTA_SCOPE = "quota_scope";
        private static readonly string PLAN_QUOTA_TARGET = "quota_target";
        private static readonly Int32 PLAN_PRICE_AMOUNT = 2000;
        private static readonly string PLAN_PRICE_CURR_ID = "plan_price_currency_id";
        private static readonly string PLAN_PRICE_CURR_NAME = "plan_price_currency_name";

        private static readonly IDictionary<string, object> PLAN_PRICE_CURR_DATA =
            new Dictionary<string, object>() {{"plan_price_data_0_key", "plan_price_data_0_value"}};


        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiPlan = null;
            converter = new PlanConverter(apiPlan);
            Assert.IsNull(converter.ToSDKPlan());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkPlan = null;
            converter = new PlanConverter(sdkPlan);
            Assert.IsNull(converter.ToSDKPlan());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkPlan = CreateTypicalSDKPlan();
            converter = new PlanConverter(sdkPlan);
            Plan plan = converter.ToSDKPlan();

            Assert.IsNotNull(plan);
            Assert.AreEqual(sdkPlan, plan);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiPlan = CreateTypicalAPIPlan();
            converter = new PlanConverter(apiPlan);
            Plan plan = converter.ToSDKPlan();

            Assert.IsNotNull(plan);
            Assert.AreEqual(PLAN_CONTRACT, plan.Contract);
            Assert.AreEqual(PLAN_PRICE_AMOUNT, plan.Price.Amount);
            Assert.AreEqual(PLAN_CYC_COUNT, plan.FreeCycles.Count);
            Assert.AreEqual(1, plan.Quotas.Count);
            Assert.AreEqual(PLAN_QUOTA_SCOPE, plan.Quotas[0].Scope);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkPlan = null;
            converter = new PlanConverter(sdkPlan);

            Assert.IsNull(converter.ToAPIPlan());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiPlan = null;
            converter = new PlanConverter(apiPlan);

            Assert.IsNull(converter.ToAPIPlan());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiPlan = CreateTypicalAPIPlan();
            converter = new PlanConverter(apiPlan);

            OneSpanSign.API.Plan plan = converter.ToAPIPlan();

            Assert.IsNotNull(plan);
            Assert.AreEqual(apiPlan, plan);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkPlan = CreateTypicalSDKPlan();
            converter = new PlanConverter(sdkPlan);

            OneSpanSign.API.Plan plan = converter.ToAPIPlan();

            Assert.IsNotNull(plan);
            Assert.AreEqual(PLAN_CONTRACT, plan.Contract);
            Assert.AreEqual(PLAN_PRICE_AMOUNT, plan.Price.Amount);
            Assert.AreEqual(PLAN_CYC_COUNT, plan.FreeCycles.Count);
            Assert.AreEqual(1, plan.Quotas.Count);
            Assert.AreEqual(PLAN_QUOTA_SCOPE, plan.Quotas[0].Scope);
        }

        private Plan CreateTypicalSDKPlan()
        {
            Plan plan = new Plan();
            plan.Contract = PLAN_CONTRACT;
            plan.Group = PLAN_GRP;
            plan.Original = PLAN_ORI;
            plan.Description = PLAN_DES;
            plan.Data = PLAN_DATA;
            plan.Cycle = PLAN_CYC;
            plan.Id = PLAN_ID;
            plan.Features = PLAN_FEAT;
            plan.Name = PLAN_NAME;
            CycleCount cycleCount = new CycleCount();
            cycleCount.Cycle = PLAN_CYC_CYCLE;
            cycleCount.Count = PLAN_CYC_COUNT;
            plan.FreeCycles = cycleCount;
            Quota quota = new Quota();
            quota.Target = PLAN_QUOTA_TARGET;
            quota.Limit = PLAN_QUOTA_LIMIT;
            quota.Cycle = PLAN_QUOTA_CYCLE;
            quota.Scope = PLAN_QUOTA_SCOPE;
            plan.AddQuota(quota);
            Price price1 = new Price();
            price1.Amount = PLAN_PRICE_AMOUNT;
            Currency currency1 = new Currency();
            currency1.Id = PLAN_PRICE_CURR_ID;
            currency1.Name = PLAN_PRICE_CURR_NAME;
            currency1.Data = PLAN_PRICE_CURR_DATA;
            price1.Currency = currency1;
            plan.Price = price1;

            return plan;
        }

        private OneSpanSign.API.Plan CreateTypicalAPIPlan()
        {
            OneSpanSign.API.Plan plan = new OneSpanSign.API.Plan();
            plan.Contract = PLAN_CONTRACT;
            plan.Group = PLAN_GRP;
            plan.Original = PLAN_ORI;
            plan.Description = PLAN_DES;
            plan.Data = PLAN_DATA;
            plan.Cycle = PLAN_CYC;
            plan.Id = PLAN_ID;
            plan.Features = PLAN_FEAT;
            plan.Name = PLAN_NAME;
            OneSpanSign.API.CycleCount cycleCount = new OneSpanSign.API.CycleCount();
            cycleCount.Cycle = PLAN_CYC_CYCLE;
            cycleCount.Count = PLAN_CYC_COUNT;
            plan.FreeCycles = cycleCount;
            OneSpanSign.API.Quota quota = new OneSpanSign.API.Quota();
            quota.Target = PLAN_QUOTA_TARGET;
            quota.Limit = PLAN_QUOTA_LIMIT;
            quota.Cycle = PLAN_QUOTA_CYCLE;
            quota.Scope = PLAN_QUOTA_SCOPE;
            plan.AddQuota(quota);
            OneSpanSign.API.Price price1 = new OneSpanSign.API.Price();
            price1.Amount = PLAN_PRICE_AMOUNT;
            OneSpanSign.API.Currency currency1 = new OneSpanSign.API.Currency();
            currency1.Id = PLAN_PRICE_CURR_ID;
            currency1.Name = PLAN_PRICE_CURR_NAME;
            currency1.Data = PLAN_PRICE_CURR_DATA;
            price1.Currency = currency1;
            plan.Price = price1;

            return plan;
        }
    }
}