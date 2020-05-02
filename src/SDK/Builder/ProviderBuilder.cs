using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class ProviderBuilder
    {
        private string provides;
        private IDictionary<string, object> data;
        private string id;
        private string name;


        private ProviderBuilder()
        {
            data = new Dictionary<string, object>();
        }

        public static ProviderBuilder NewProvider(String name)
        {
            return new ProviderBuilder().WithName(name);
        }

        public ProviderBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ProviderBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public ProviderBuilder WithProvides(string provides)
        {
            this.provides = provides;
            return this;
        }

        public ProviderBuilder WithData(IDictionary<string, object> data)
        {
            this.data = data;
            return this;
        }

        internal Provider Build()
        {
            Provider provider = new Provider();
            provider.Name = name;
            provider.Data = data;
            provider.Id = id;
            provider.Provides = provides;
            return provider;
        }
    }
}