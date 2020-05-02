using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class CompanyBuilder
    {
        private Address address;
        private IDictionary<string, object> data;
        private string id;
        private string name;


        private CompanyBuilder()
        {
            data = new Dictionary<string, object>();
        }

        public static CompanyBuilder NewCompany(string name)
        {
            return new CompanyBuilder().WithName(name);
        }

        public CompanyBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public CompanyBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public CompanyBuilder WithAddress(Address address)
        {
            this.address = address;
            return this;
        }

        public CompanyBuilder WithData(IDictionary<string, object> data)
        {
            this.data = data;
            return this;
        }

        internal Company Build()
        {
            Company company = new Company();
            company.Name = name;
            company.Data = data;
            company.Id = id;
            company.Address = address;
            return company;
        }
    }
}