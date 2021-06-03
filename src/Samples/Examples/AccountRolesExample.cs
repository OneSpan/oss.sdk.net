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

        override public void Execute()
        {
            result1 = ossClient.AccountService.getAccountRoles();

            string newAccountRoleName = Guid.NewGuid().ToString();
            OneSpanSign.Sdk.AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithName(newAccountRoleName)
                .WithPermissions(new List<string>() {"P1"})
                .WithDescription("DESCRIPTION")
                .WithEnabled(true)
                .Build();

            ossClient.AccountService.addAccountRole(accountRole);
            result2 = ossClient.AccountService.getAccountRoles();

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in result2)
            {
                if (forAccountRole.Name.Equals(newAccountRoleName))
                {
                    newAccountRoleId = forAccountRole.Id;
                }
            }

            accountRole.Description = "NEW - DESCRIPTION";
            ossClient.AccountService.updateAccountRole(newAccountRoleId, accountRole);
            result2 = ossClient.AccountService.getAccountRoles();

            newAccountRole = ossClient.AccountService.getAccountRole(newAccountRoleId);
            newAccountUsers = ossClient.AccountService.getAccountRoleUsers(newAccountRoleId);

            ossClient.AccountService.deleteAccountRole(newAccountRoleId);
            result3 = ossClient.AccountService.getAccountRoles();
        }
    }
}