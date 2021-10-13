using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class UserAccountRoleConverter
    {
        private UserAccountRole sdkUserAccountRole;
        private OneSpanSign.API.UserAccountRole apiUserAccountRole;

        public UserAccountRoleConverter(UserAccountRole sdkUserAccountRole)
        {
            this.sdkUserAccountRole = sdkUserAccountRole;
        }

        public UserAccountRoleConverter(OneSpanSign.API.UserAccountRole apiUserAccountRole)
        {
            this.apiUserAccountRole = apiUserAccountRole;
        }

        public UserAccountRole ToSDKUserAccountRole()
        {
            if (sdkUserAccountRole != null)
            {
                return sdkUserAccountRole;
            }
            else if (apiUserAccountRole != null)
            {

                List<AccountRole> sdkAccountRoles = new List<AccountRole>();
                
                foreach (OneSpanSign.API.AccountRole apiAccountRole in apiUserAccountRole.AccountRoles)
                {
                    sdkAccountRoles.Add(new AccountRoleConverter(apiAccountRole).ToSDKAccountRole());
                }
                
                return UserAccountRoleBuilder.NewUserAccountRole()
                    .WithAccountId(apiUserAccountRole.AccountId)
                    .WithAccountRoles(sdkAccountRoles).WithUserId(apiUserAccountRole.UserId).Build();

            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.UserAccountRole ToAPIUserAccountRole()
        {
            if (apiUserAccountRole != null)
            {
                return apiUserAccountRole;
            }
            else if (sdkUserAccountRole != null)
            {
                List<OneSpanSign.API.AccountRole> apiAccountRoles = new List<OneSpanSign.API.AccountRole>();
                
                foreach (OneSpanSign.Sdk.AccountRole apiAccountRole in sdkUserAccountRole.AccountRoles)
                {
                    apiAccountRoles.Add(new AccountRoleConverter(apiAccountRole).ToAPIAccountRole());
                }
                
                OneSpanSign.API.UserAccountRole apiUserAccountRole = new OneSpanSign.API.UserAccountRole();
                apiUserAccountRole.AccountId = sdkUserAccountRole.AccountId;
                apiUserAccountRole.AccountRoles = apiAccountRoles;
                apiUserAccountRole.UserId = sdkUserAccountRole.UserId;
                
                return apiUserAccountRole;
            }
            else
            {
                return null;
            }
        }

    }
}