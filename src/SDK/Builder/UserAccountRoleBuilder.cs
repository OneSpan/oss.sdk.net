using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
    public class UserAccountRoleBuilder
    {
        private string accountId;
        private List<AccountRole> accountRoles = new List<AccountRole>();
        private string userId;

        private UserAccountRoleBuilder()
        {
        }

        public static UserAccountRoleBuilder NewUserAccountRole()
        {
            return new UserAccountRoleBuilder();
        }

        public UserAccountRoleBuilder WithAccountId(string accountId)
        {
            this.accountId = accountId;
            return this;
        }

        public UserAccountRoleBuilder WithAccountRoles(List<AccountRole> accountRoles)
        {
            this.accountRoles = accountRoles;
            return this;
        }

        public UserAccountRoleBuilder WithUserId(string userId)
        {
            this.userId = userId;
            return this;
        }

        public UserAccountRole Build()
        {
            UserAccountRole result = new UserAccountRole();
            result.AccountId = accountId;
            result.AccountRoles = accountRoles;
            result.UserId = userId;

            return result;
        }
    }
}