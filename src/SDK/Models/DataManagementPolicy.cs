using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
    internal class DataManagementPolicy
    {
        [JsonProperty ("transactionRetention")]
        public TransactionRetention TransactionRetention 
        {
            get; set;
        }
    }
}
