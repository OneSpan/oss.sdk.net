using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
    public class AccountEmailReminderSettings
    {
        [JsonProperty("startInDaysDelay")]
        public Nullable<int> StartInDaysDelay
        {
            get; set;
        }

        [JsonProperty("intervalInDays")]
        public Nullable<int> IntervalInDays
        {
            get; set;
        }

        [JsonProperty("repetitionsCount")]
        public Nullable<int> RepetitionsCount
        {
            get; set;
        }

    }
}