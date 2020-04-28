using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class AccessibleAccountResponse
    {
        [JsonProperty("accountUid")]
        public String AccountUid { get; set; }
        
        [JsonProperty("accountName")]
        public String AccountName { get; set; }
    }
}