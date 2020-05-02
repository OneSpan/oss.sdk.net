using System;

namespace OneSpanSign.Sdk
{
    public class CreditCardBuilder
    {
        private string cvv;
        private CcExpiration expiration;
        private string number;
        private string name;
        private string type;


        private CreditCardBuilder()
        {
        }

        public static CreditCardBuilder NewCreditCard()
        {
            return new CreditCardBuilder();
        }

        public CreditCardBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public CreditCardBuilder WithType(string type)
        {
            this.type = type;
            return this;
        }

        public CreditCardBuilder WithNumber(string number)
        {
            this.number = number;
            return this;
        }

        public CreditCardBuilder WithCvv(string cvv)
        {
            this.cvv = cvv;
            return this;
        }

        public CreditCardBuilder WithExpiration(CcExpiration expiration)
        {
            this.expiration = expiration;
            return this;
        }

        public CreditCardBuilder WithExpiration(Nullable<Int32> month, Nullable<Int32> year)
        {
            expiration = new CcExpiration();
            expiration.Month = month;
            expiration.Year = year;
            return this;
        }

        internal CreditCard Build()
        {
            CreditCard creditCard = new CreditCard();
            creditCard.Name = name;
            creditCard.Type = type;
            creditCard.Number = number;
            creditCard.Cvv = cvv;
            creditCard.Expiration = expiration;
            return creditCard;
        }
    }
}