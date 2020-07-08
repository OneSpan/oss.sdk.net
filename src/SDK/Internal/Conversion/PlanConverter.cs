
namespace OneSpanSign.Sdk
{
    internal class PlanConverter
    {
        private Plan sdkPlan;
        private OneSpanSign.API.Plan apiPlan;

        public PlanConverter(Plan sdkPlan)
        {
            this.sdkPlan = sdkPlan;
        }

        public PlanConverter(OneSpanSign.API.Plan apiPlan)
        {
            this.apiPlan = apiPlan;
        }

        public Plan ToSDKPlan()
        {
            if (sdkPlan != null)
            {
                return sdkPlan;
            }
            else if (apiPlan != null)
            {
                PlanBuilder builder = PlanBuilder.NewPlan(apiPlan.Name)
                    .WithContract(apiPlan.Contract)
                    .WithData(apiPlan.Data)
                    .WithId(apiPlan.Id)
                    .WithCycle(apiPlan.Cycle)
                    .WithDescription(apiPlan.Description)
                    .WithFeatures(apiPlan.Features)
                    .WithGroup(apiPlan.Group)
                    .WithOriginal(apiPlan.Original)
                    .WithPrice(new PriceConverter(apiPlan.Price).ToSDKPrice());
                if (apiPlan.FreeCycles != null)
                {
                    builder.WithFreeCycles(apiPlan.FreeCycles.Count, apiPlan.FreeCycles.Cycle);
                }

                if (apiPlan.Quotas != null && apiPlan.Quotas.Count > 0)
                {
                    foreach (API.Quota quota in apiPlan.Quotas)
                    {
                        builder.WithQuota(quota.Cycle, quota.Limit, quota.Scope, quota.Target);
                    }
                }

                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.Plan ToAPIPlan()
        {
            if (apiPlan != null)
            {
                return apiPlan;
            }
            else if (sdkPlan != null)
            {
                OneSpanSign.API.Plan result = new OneSpanSign.API.Plan();
                result.Name = sdkPlan.Name;
                result.Id = sdkPlan.Id;
                result.Contract = sdkPlan.Contract;
                result.Cycle = sdkPlan.Cycle;
                result.Data = sdkPlan.Data;
                result.Features = sdkPlan.Features;
                result.Description = sdkPlan.Description;
                result.Group = sdkPlan.Group;
                result.Original = sdkPlan.Original;
                result.Price = new PriceConverter(sdkPlan.Price).ToAPIPrice();
                result.FreeCycles = new API.CycleCount();
                result.FreeCycles.Count = sdkPlan.FreeCycles.Count;
                result.FreeCycles.Cycle = sdkPlan.FreeCycles.Cycle;
                foreach (Quota quota in sdkPlan.Quotas)
                {
                    API.Quota apiQuota = new API.Quota();
                    apiQuota.Cycle = quota.Cycle;
                    apiQuota.Limit = quota.Limit;
                    apiQuota.Scope = quota.Scope;
                    apiQuota.Target = quota.Target;
                    result.Quotas.Add(apiQuota);
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