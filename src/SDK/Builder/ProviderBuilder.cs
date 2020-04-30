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


		private ProviderBuilder ()
		{
			data = new Dictionary<string, object>();
		}

		public static ProviderBuilder NewProvider( String name )
		{
			return new ProviderBuilder().WithName(name);
		}

		public ProviderBuilder WithName(string value)
		{
			name = value;
			return this;
		}

		public ProviderBuilder WithId( string value )
		{
			id = value;
			return this;
		}

		public ProviderBuilder WithProvides( string value )
		{
			provides = value;
			return this;
		}

		public ProviderBuilder WithData(IDictionary<string, object> value)
		{
			data = value;
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

