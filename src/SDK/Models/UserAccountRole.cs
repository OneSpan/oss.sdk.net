using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class UserAccountRole
    {
        public UserAccountRole()
        {
        }

        public UserAccountRole(string accountId, List<OneSpanSign.API.AccountRole> accountRoles, string userId)
        {
            this.AccountId = accountId;
            this.AccountRoles = accountRoles;
            this.UserId = userId;
        }

        [JsonProperty("userId")] public string UserId { get; set; }
        [JsonProperty("accountId")] public string AccountId { get; set; }
        [JsonProperty("accountRoles")] public List<OneSpanSign.API.AccountRole> AccountRoles { get; set; }

    }
}