
namespace OneSpanSign.Sdk
{
    internal class TransactionConverter
    {
        private Transaction sdkTransaction;
        private OneSpanSign.API.Transaction apiTransaction;

        public TransactionConverter(Transaction sdkTransaction)
        {
            this.sdkTransaction = sdkTransaction;
        }

        public TransactionConverter(OneSpanSign.API.Transaction apiTransaction)
        {
            this.apiTransaction = apiTransaction;
        }

        public Transaction ToSDKTransaction()
        {
            if (sdkTransaction != null)
            {
                return sdkTransaction;
            }
            else if (apiTransaction != null)
            {
                Transaction transaction = new Transaction();
                transaction.Created = apiTransaction.Created;
                transaction.Price = new PriceConverter(apiTransaction.Price).ToSDKPrice();
                transaction.CreditCard = new CreditCardConverter(apiTransaction.CreditCard).ToSDKCreditCard();
                return transaction;
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.Transaction ToAPITransaction()
        {
            if (apiTransaction != null)
            {
                return apiTransaction;
            }
            else if (sdkTransaction != null)
            {
                OneSpanSign.API.Transaction result = new OneSpanSign.API.Transaction();
                result.Created = sdkTransaction.Created;
                result.Price = new PriceConverter(sdkTransaction.Price).ToAPIPrice();
                result.CreditCard = new CreditCardConverter(sdkTransaction.CreditCard).ToAPICreditCard();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}