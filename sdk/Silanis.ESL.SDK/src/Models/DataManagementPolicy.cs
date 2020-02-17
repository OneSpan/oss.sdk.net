using System;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
    internal class DataManagementPolicy
    {
        [JsonProperty ("transactionRetention")]
        public TransactionRetention TransactionRetention {
            get; set;
        }
    }
}
