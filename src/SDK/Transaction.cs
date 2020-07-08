
using System;

namespace OneSpanSign.Sdk
{
    public class Transaction
    {
        public Nullable<DateTime> Created { get; set; }


        public CreditCard CreditCard { get; set; }


        public Price Price { get; set; }
    }
}