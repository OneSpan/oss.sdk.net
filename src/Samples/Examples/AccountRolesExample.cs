using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;

namespace SDK.Examples
{
    public class AccountRolesExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AccountRolesExample().Run();
        }

        public List<AccountRole> result1 = null;
        public List<AccountRole> result2 = null;
        public List<AccountRole> result3 = null;
        public string newAccountRoleId = null;
        public OneSpanSign.Sdk.AccountRole newAccountRole = null;
        public List<String> newAccountUsers = null;
        protected OssClient ossClientWithRoleAndPermission;
        override public void Execute()
        {
            //Setup ossclient for role and permission enabled user
            ossClientWithRoleAndPermission = new OssClient(props.Get("api.key.withRolesAndPermission"), props.Get("api.url"), props.Get("webpage.url"), true);
            base.senderEmail = props.Get("sender.email.withRolesAndPermission");
            
            //Create a role
            result1 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            string newAccountRoleName = Guid.NewGuid().ToString();
            OneSpanSign.Sdk.AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithName(newAccountRoleName)
                .WithPermissions(new List<string>() {"sender_admin.users"})
                .WithDescription("DESCRIPTION")
                .WithEnabled(true)
                .Build();

            ossClientWithRoleAndPermission.AccountService.addAccountRole(accountRole);
            result2 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in result2)
            {
                if (forAccountRole.Name.Equals(newAccountRoleName))
                {
                    newAccountRoleId = forAccountRole.Id;
                }
            }
            
            //Update a role
            accountRole.Description = "NEW - DESCRIPTION";
            ossClientWithRoleAndPermission.AccountService.updateAccountRole(newAccountRoleId, accountRole);
            result2 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();
            
            //Get a role
            newAccountRole = ossClientWithRoleAndPermission.AccountService.getAccountRole(newAccountRoleId);
            
            //Get users who have the role
            newAccountUsers = ossClientWithRoleAndPermission.AccountService.getAccountRoleUsers(newAccountRoleId);
            
            //Delete a role
            ossClientWithRoleAndPermission.AccountService.deleteAccountRole(newAccountRoleId);
            result3 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();
        }
        
    }
}