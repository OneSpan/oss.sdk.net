using System;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
    internal class ExpiryTimeConfiguration
    {
        [JsonProperty ("maximumRemainingDays")]
        public Nullable<Int32> MaximumRemainingDays {
            get; set;
        }

        [JsonProperty ("remainingDays")]
        public Nullable<Int32> RemainingDays {
            get; set;
        }
    }
}
