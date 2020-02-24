using System;
namespace Silanis.ESL.SDK
{
    internal class DataManagementPolicyConverter
    {
        private DataManagementPolicy sdkDataManagementPolicy;
        private Silanis.ESL.API.DataManagementPolicy apiDataManagementPolicy;

        public DataManagementPolicyConverter (Silanis.ESL.API.DataManagementPolicy apiDataManagementPolicy)
        {
            this.apiDataManagementPolicy = apiDataManagementPolicy;
        }

        public DataManagementPolicyConverter (DataManagementPolicy sdkDataManagementPolicy)
        {
            this.sdkDataManagementPolicy = sdkDataManagementPolicy;
        }

        internal Silanis.ESL.API.DataManagementPolicy ToAPIDataManagementPolicy ()
        {
            if (sdkDataManagementPolicy == null) {
                return apiDataManagementPolicy;
            }
            apiDataManagementPolicy = new Silanis.ESL.API.DataManagementPolicy ();
            if(sdkDataManagementPolicy.TransactionRetention != null) {
                apiDataManagementPolicy.TransactionRetention = new TransactionRetentionConverter(sdkDataManagementPolicy.TransactionRetention)
                    .ToAPITransactionRetention ();
            }

            return apiDataManagementPolicy;
        }

        internal Silanis.ESL.SDK.DataManagementPolicy ToSDKDataManagementPolicy ()
        {
            if (apiDataManagementPolicy == null) {
                return sdkDataManagementPolicy;
            }
            sdkDataManagementPolicy = new DataManagementPolicy();
            if (apiDataManagementPolicy.TransactionRetention != null) {
                sdkDataManagementPolicy.TransactionRetention = new TransactionRetentionConverter (apiDataManagementPolicy.TransactionRetention)
                    .ToSDKTransactionRetention ();
            }

            return sdkDataManagementPolicy;
        }
    }
}
