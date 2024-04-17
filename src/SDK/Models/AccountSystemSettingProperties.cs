using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class AccountSystemSettingProperties
    {
        [JsonProperty("senderLoginMaxFailedAttempts")]
        public Nullable<int> SenderLoginMaxFailedAttempts
        {
            get; set;
        }
        
        [JsonProperty("loginSessionTimeout")]
        public Nullable<int> LoginSessionTimeout
        {
            get; set;
        }

        [JsonProperty("sessionTimeoutWarning")]
        public Nullable<int> SessionTimeoutWarning
        {
            get; set;
        }
        [JsonProperty("orderLastNameFirstName")]
        public Nullable<bool> OrderLastNameFirstName
        {
            get; set;
        }

 
    }
}