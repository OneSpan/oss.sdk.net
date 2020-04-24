using System;

namespace OneSpanSign.Sdk
{
    internal class PriceConverter
    {
        private Price sdkPrice;
        private OneSpanSign.API.Price apiPrice;

        public PriceConverter( Price sdkPrice )
        {
            this.sdkPrice = sdkPrice;
        }

        public PriceConverter( OneSpanSign.API.Price apiPrice ) 
        {
            this.apiPrice = apiPrice;
        }

        public Price ToSDKPrice() {
            if (sdkPrice != null)
            {
                return sdkPrice;
            }
            else if (apiPrice != null)
            {
                return PriceBuilder.NewPrice()
                    .WithAmount(apiPrice.Amount)
                    .WithCurrency(apiPrice.Currency.Id, apiPrice.Currency.Name, apiPrice.Currency.Data)
                    .Build();
            }
            else
            {
                return null;
            }
        }
        
        public OneSpanSign.API.Price ToAPIPrice() {
            if (apiPrice != null)
            {
                return apiPrice;
            }
            else if (sdkPrice != null)
            {
                OneSpanSign.API.Price result = new OneSpanSign.API.Price();
                result.Amount = sdkPrice.Amount;
                result.Currency = new API.Currency();
                result.Currency.Data = sdkPrice.Currency.Data;
                result.Currency.Id = sdkPrice.Currency.Id;
                result.Currency.Name = sdkPrice.Currency.Name;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

