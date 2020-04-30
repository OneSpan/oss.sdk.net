using System;

namespace OneSpanSign.Sdk
{
    internal class CreditCardConverter
    {
        private CreditCard sdkCreditCard;
        private OneSpanSign.API.CreditCard apiCreditCard;

        public CreditCardConverter( CreditCard sdkCreditCard )
        {
            this.sdkCreditCard = sdkCreditCard;
        }

        public CreditCardConverter( OneSpanSign.API.CreditCard apiCreditCard ) 
        {
            this.apiCreditCard = apiCreditCard;
        }

        public CreditCard ToSDKCreditCard() {
            if (sdkCreditCard != null)
            {
                return sdkCreditCard;
            }
            else if (apiCreditCard != null)
            {
                CreditCardBuilder builder = CreditCardBuilder.NewCreditCard()
                    .WithName(apiCreditCard.Name)
                    .WithCvv(apiCreditCard.Cvv)
                    .WithNumber(apiCreditCard.Number)
                    .WithType(apiCreditCard.Type)
                    .WithExpiration(apiCreditCard.Expiration.Month, apiCreditCard.Expiration.Year);
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.CreditCard ToAPICreditCard() {
            if (apiCreditCard != null)
            {
                return apiCreditCard;
            }
            else if (sdkCreditCard != null)
            {
                OneSpanSign.API.CreditCard result = new OneSpanSign.API.CreditCard();
                result.Name = sdkCreditCard.Name;
                result.Type = sdkCreditCard.Type;
                result.Number = sdkCreditCard.Number;
                result.Cvv = sdkCreditCard.Cvv;
                result.Expiration = new API.CcExpiration();
                result.Expiration.Month = apiCreditCard.Expiration.Month;
                result.Expiration.Year = apiCreditCard.Expiration.Year;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

