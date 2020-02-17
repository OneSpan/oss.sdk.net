using System;
namespace Silanis.ESL.SDK
{
    public class DataManagementPolicyBuilder
    {
        private TransactionRetention transactionRetention;

        private DataManagementPolicyBuilder ()
        {
        }

        public static DataManagementPolicyBuilder NewDataManagementPolicy ()
        {
            return new DataManagementPolicyBuilder ();
        }

        public DataManagementPolicyBuilder WithTransactionRetention (TransactionRetention transactionRetention)
        {
            this.transactionRetention = transactionRetention;
            return this;
        }

        public DataManagementPolicy Build ()
        {
            DataManagementPolicy result = new DataManagementPolicy ();
            result.TransactionRetention = transactionRetention;
            return result;
        }
    }
}
