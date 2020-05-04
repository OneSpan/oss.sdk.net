using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class LicenseBuilder
    {
        private Nullable<DateTime> created;
        private Nullable<DateTime> paidUntil;
        private Plan plan;
        private string status;
        private IList<Transaction> transactions;


        private LicenseBuilder()
        {
            transactions = new List<Transaction>();
        }

        public static LicenseBuilder NewLicense()
        {
            return new LicenseBuilder();
        }

        public LicenseBuilder CreatedOn(Nullable<DateTime> created)
        {
            this.created = created;
            return this;
        }

        public LicenseBuilder WithPaidUntil(Nullable<DateTime> paidUntil)
        {
            this.paidUntil = paidUntil;
            return this;
        }

        public LicenseBuilder WithPlan(Plan plan)
        {
            this.plan = plan;
            return this;
        }

        public LicenseBuilder WithStatus(string status)
        {
            this.status = status;
            return this;
        }

        public LicenseBuilder WithTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            return this;
        }

        public LicenseBuilder WithTransaction(Nullable<DateTime> created, CreditCard creditCard, Price price)
        {
            Transaction transaction = new Transaction();
            transaction.Created = created;
            transaction.CreditCard = creditCard;
            transaction.Price = price;
            transactions.Add(transaction);
            return this;
        }

        internal License Build()
        {
            License license = new License();
            license.Created = created;
            license.PaidUntil = paidUntil;
            license.Status = status;
            license.Plan = plan;
            foreach (Transaction transaction in transactions)
            {
                license.Transactions.Add(transaction);
            }

            return license;
        }
    }
}