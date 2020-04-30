using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class PriceBuilder
	{
		private Nullable<Int32> amount;
		private Currency currency;


		private PriceBuilder ()
		{
		}

		public static PriceBuilder NewPrice()
		{
			return new PriceBuilder();
		}

		public PriceBuilder WithAmount(Nullable<Int32> value)
		{
			amount = value;
			return this;
		}

		public PriceBuilder WithCurrency( Currency value )
		{
			currency = value;
			return this;
		}

		public PriceBuilder WithCurrency( string id, string name, IDictionary<string, object> data)
		{
			currency = new Currency();
			currency.Id = id;
			currency.Data = data;
			currency.Name = name;
			return this;
		}
		
		internal Price Build()
		{
			Price price = new Price();
			price.Amount = amount;
			price.Currency = currency;
			return price;
		}

	}
}

