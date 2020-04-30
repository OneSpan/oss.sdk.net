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


		private CompanyBuilder ()
		{
			data = new Dictionary<string, object>();
		}

		public static CompanyBuilder NewCompany( String name )
		{
			return new CompanyBuilder().WithName(name);
		}

		public CompanyBuilder WithName(string value)
		{
			name = value;
			return this;
		}

		public CompanyBuilder WithId( string value )
		{
			id = value;
			return this;
		}

		public CompanyBuilder WithAddress( Address value )
		{
			address = value;
			return this;
		}

		public CompanyBuilder WithData(IDictionary<string, object> value)
		{
			data = value;
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

