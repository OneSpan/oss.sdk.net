using System;
namespace OneSpanSign.Sdk
{
    internal class DataManagementPolicyConverter
    {
        private DataManagementPolicy sdkDataManagementPolicy;
        private OneSpanSign.API.DataManagementPolicy apiDataManagementPolicy;

        public DataManagementPolicyConverter (OneSpanSign.API.DataManagementPolicy apiDataManagementPolicy)
        {
            this.apiDataManagementPolicy = apiDataManagementPolicy;
        }

        public DataManagementPolicyConverter (DataManagementPolicy sdkDataManagementPolicy)
        {
            this.sdkDataManagementPolicy = sdkDataManagementPolicy;
        }

        internal OneSpanSign.API.DataManagementPolicy ToAPIDataManagementPolicy ()
        {
            if (sdkDataManagementPolicy == null) 
            {
                return apiDataManagementPolicy;
            }
            apiDataManagementPolicy = new OneSpanSign.API.DataManagementPolicy ();
            if(sdkDataManagementPolicy.TransactionRetention != null) 
            {
                apiDataManagementPolicy.TransactionRetention = new TransactionRetentionConverter(sdkDataManagementPolicy.TransactionRetention)
                    .ToAPITransactionRetention ();
            }

            return apiDataManagementPolicy;
        }

        internal OneSpanSign.Sdk.DataManagementPolicy ToSDKDataManagementPolicy ()
        {
            if (apiDataManagementPolicy == null) 
            {
                return sdkDataManagementPolicy;
            }
            sdkDataManagementPolicy = new DataManagementPolicy();
            if (apiDataManagementPolicy.TransactionRetention != null) 
            {
                sdkDataManagementPolicy.TransactionRetention = new TransactionRetentionConverter (apiDataManagementPolicy.TransactionRetention)
                    .ToSDKTransactionRetention ();
            }

            return sdkDataManagementPolicy;
        }
    }
}
