using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AccountRoleBuilder
    {
        private string id;
        private string name;
        private List<string> permissions = new List<string>();
        private string description;
        private bool enabled;

        private AccountRoleBuilder()
        {
        }

        public static AccountRoleBuilder NewAccountRole()
        {
            return new AccountRoleBuilder();
        }

        public AccountRoleBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public AccountRoleBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public AccountRoleBuilder WithPermissions(List<String> permissions)
        {
            this.permissions = permissions;
            return this;
        }

        public AccountRoleBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public AccountRoleBuilder WithEnabled(Boolean enabled)
        {
            this.enabled = enabled;
            return this;
        }

        public AccountRole Build()
        {
            AccountRole result = new AccountRole();
            result.Id = id;
            result.Name = name;
            result.Description = description;
            result.Enabled = enabled;
            result.Permissions = permissions;

            return result;
        }
    }
}