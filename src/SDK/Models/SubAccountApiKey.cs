using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    public class SubAccountApiKey
    {
        [JsonProperty("accountUid")] public String AccountUid { get; set; }


        [JsonProperty("accountName")] public String AccountName { get; set; }


        [JsonProperty("apiKey")] public String ApiKey { get; set; }
    }
}