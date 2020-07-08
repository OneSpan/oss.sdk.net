using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class PlanBuilder
    {
        private string contract;
        private string cycle;
        private IDictionary<string, object> data;
        private string description;
        private IDictionary<string, object> features;
        private CycleCount freeCycles;
        private string group;
        private string id;
        private string name;
        private string original;
        private Price price;
        private IList<Quota> quotas;


        private PlanBuilder()
        {
            quotas = new List<Quota>();
            data = new Dictionary<string, object>();
            features = new Dictionary<string, object>();
        }

        public static PlanBuilder NewPlan(string name)
        {
            return new PlanBuilder().WithName(name);
        }

        public PlanBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public PlanBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public PlanBuilder WithContract(string contract)
        {
            this.contract = contract;
            return this;
        }

        public PlanBuilder WithCycle(string cycle)
        {
            this.cycle = cycle;
            return this;
        }

        public PlanBuilder WithData(IDictionary<string, object> data)
        {
            this.data = data;
            return this;
        }

        public PlanBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public PlanBuilder WithFeatures(IDictionary<string, object> features)
        {
            this.features = features;
            return this;
        }

        public PlanBuilder WithFreeCycles(CycleCount cycleCount)
        {
            freeCycles = cycleCount;
            return this;
        }

        public PlanBuilder WithFreeCycles(Nullable<Int32> count, string cycle)
        {
            freeCycles = new CycleCount();
            freeCycles.Count = count;
            freeCycles.Cycle = cycle;
            return this;
        }

        public PlanBuilder WithGroup(string group)
        {
            this.group = group;
            return this;
        }

        public PlanBuilder WithOriginal(string original)
        {
            this.original = original;
            return this;
        }

        public PlanBuilder WithPrice(Price price)
        {
            this.price = price;
            return this;
        }

        public PlanBuilder WithQuota(Quota quota)
        {
            quotas.Add(quota);
            return this;
        }

        public PlanBuilder WithQuota(string cycle, Nullable<Int32> limit, string scope, string target)
        {
            Quota quota = new Quota();
            quota.Cycle = cycle;
            quota.Limit = limit;
            quota.Scope = scope;
            quota.Target = target;
            quotas.Add(quota);
            return this;
        }

        internal Plan Build()
        {
            Plan plan = new Plan();
            plan.Contract = contract;
            plan.Data = data;
            plan.Name = name;
            plan.Id = id;
            plan.Cycle = cycle;
            plan.Description = description;
            plan.Price = price;
            plan.Features = features;
            plan.FreeCycles = freeCycles;
            plan.Group = group;
            plan.Original = original;
            foreach (Quota quota in quotas)
            {
                plan.Quotas.Add(quota);
            }

            return plan;
        }
    }
}