using System;

namespace OneSpanSign.Sdk
{
    internal class AccountRoleConverter
    {
        private AccountRole sdkAccountRole;
        private OneSpanSign.API.AccountRole apiAccountRole;

        public AccountRoleConverter(AccountRole sdkAccountRole)
        {
            this.sdkAccountRole = sdkAccountRole;
        }

        public AccountRoleConverter(OneSpanSign.API.AccountRole apiAccountRole)
        {
            this.apiAccountRole = apiAccountRole;
        }

        public AccountRole ToSDKAccountRole()
        {
            if (sdkAccountRole != null)
            {
                return sdkAccountRole;
            }
            else if (apiAccountRole != null)
            {
                AccountRoleBuilder builder = AccountRoleBuilder.NewAccountRole().WithId(apiAccountRole.Id)
                    .WithName(apiAccountRole.Name).WithPermissions(apiAccountRole.Permissions)
                    .WithDescription(apiAccountRole.Description).WithEnabled(apiAccountRole.Enabled);
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.AccountRole ToAPIAccountRole()
        {
            if (apiAccountRole != null)
            {
                return apiAccountRole;
            }
            else if (sdkAccountRole != null)
            {
                OneSpanSign.API.AccountRole result = new OneSpanSign.API.AccountRole();
                result.Id = sdkAccountRole.Id;
                result.Name = sdkAccountRole.Name;
                result.Permissions = sdkAccountRole.Permissions;
                result.Enabled = sdkAccountRole.Enabled;
                result.Description = sdkAccountRole.Description;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}