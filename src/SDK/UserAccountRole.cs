using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class UserAccountRole
    {
        public UserAccountRole()
        {
        }

        public UserAccountRole(string accountId, List<AccountRole> accountRoles, string userId)
        {
            this.AccountId = accountId;
            this.AccountRoles = accountRoles;
            this.UserId = userId;
        }
        public string AccountId { get; set; }

        public string UserId { get; set; }

        public List<AccountRole> AccountRoles { get; set; }

    }
}