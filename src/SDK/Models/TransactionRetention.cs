using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
    internal class TransactionRetention
    {

        [JsonProperty ("draft")]
        public Nullable<Int32> Draft 
        {
            get; set;
        }

        [JsonProperty ("sent")]
        public Nullable<Int32> Sent 
        {
            get; set;
        }

        [JsonProperty ("completed")]
        public Nullable<Int32> Completed 
        {
            get; set;
        }

        [JsonProperty ("archived")]
        public Nullable<Int32> Archived 
        {
            get; set;
        }

        [JsonProperty ("declined")]
        public Nullable<Int32> Declined 
        {
            get; set;
        }

        [JsonProperty ("optedOut")]
        public Nullable<Int32> OptedOut 
        {
            get; set;
        }

        [JsonProperty ("expired")]
        public Nullable<Int32> Expired 
        {
            get; set;
        }
        [JsonProperty ("lifetimeTotal")]
        public Nullable<Int32> LifetimeTotal 
        {
            get; set;
        }
        [JsonProperty ("lifetimeUntilCompletion")]
        public Nullable<Int32> LifetimeUntilCompletion 
        {
            get; set;
        }
    }
}
