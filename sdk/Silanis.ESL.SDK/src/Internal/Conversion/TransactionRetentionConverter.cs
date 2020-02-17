using System;
namespace Silanis.ESL.SDK
{
    internal class TransactionRetentionConverter
    {
        private TransactionRetention sdkTransactionRetention;
        private Silanis.ESL.API.TransactionRetention apiTransactionRetention;

        public TransactionRetentionConverter (Silanis.ESL.API.TransactionRetention apiTransactionRetention)
        {
            this.apiTransactionRetention = apiTransactionRetention;
        }

        public TransactionRetentionConverter (TransactionRetention sdkTransactionRetention)
        {
            this.sdkTransactionRetention = sdkTransactionRetention;
        }

        internal Silanis.ESL.API.TransactionRetention ToAPITransactionRetention ()
        {
            if (sdkTransactionRetention == null) {
                return apiTransactionRetention;
            }
            apiTransactionRetention = new Silanis.ESL.API.TransactionRetention ();
            apiTransactionRetention.Sent = sdkTransactionRetention.Sent;
            apiTransactionRetention.Draft = sdkTransactionRetention.Draft;
            apiTransactionRetention.Declined = sdkTransactionRetention.Declined;
            apiTransactionRetention.Completed = sdkTransactionRetention.Completed;
            apiTransactionRetention.Expired = sdkTransactionRetention.Expired;
            apiTransactionRetention.Archived = sdkTransactionRetention.Archived;
            apiTransactionRetention.OptedOut = sdkTransactionRetention.OptedOut;

            return apiTransactionRetention;
        }

        internal Silanis.ESL.SDK.TransactionRetention ToSDKTransactionRetention ()
        {
            if (apiTransactionRetention == null) {
                return sdkTransactionRetention;
            }
            sdkTransactionRetention = new TransactionRetention ();
            sdkTransactionRetention.Sent = apiTransactionRetention.Sent;
            sdkTransactionRetention.Draft = apiTransactionRetention.Draft;
            sdkTransactionRetention.Declined = apiTransactionRetention.Declined;
            sdkTransactionRetention.Completed = apiTransactionRetention.Completed;
            sdkTransactionRetention.Expired = apiTransactionRetention.Expired;
            sdkTransactionRetention.Archived = apiTransactionRetention.Archived;
            sdkTransactionRetention.OptedOut = apiTransactionRetention.OptedOut;

            return sdkTransactionRetention;
        }
    }
}
