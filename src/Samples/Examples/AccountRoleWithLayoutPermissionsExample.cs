using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;

namespace SDK.Examples
{
    public class AccountRoleWithLayoutPermissionsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AccountRoleWithLayoutPermissionsExample().Run();
        }
        public String SAVE_LAYOUTS_PERMISSION = "templates_layouts.save_layouts";
        public String APPLY_LAYOUTS_PERMISSION = "templates_layouts.apply_layouts";
        public String SHARE_LAYOUTS_PERMISSION = "templates_layouts.share_layouts";
            
        public List<AccountRole> result1 = null;
        public List<AccountRole> result2 = null;
        public List<AccountRole> result3 = null;

        public String layoutPermissionsRoleUid = null;
        public OneSpanSign.Sdk.AccountRole layoutPermissionsAccountRole = null;

        protected OssClient ossClientWithRoleAndPermission;
        override public void Execute()
        {
            //Setup ossclient for role and permission enabled user
            ossClientWithRoleAndPermission = new OssClient(props.Get("api.key.withRolesAndPermission"), props.Get("api.url"), props.Get("webpage.url"), true);
            senderEmail = props.Get("sender.email.withRolesAndPermission");

            //Get existing roles
            result1 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            //Create a new role with Layout Permissions 
            string layoutPermissionsRoleName = Guid.NewGuid().ToString();
            OneSpanSign.Sdk.AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithName(layoutPermissionsRoleName)
                .WithPermissions(new List<string>() { SAVE_LAYOUTS_PERMISSION, APPLY_LAYOUTS_PERMISSION, SHARE_LAYOUTS_PERMISSION })
                .WithDescription("Account Role with Layout Permissions")
                .WithEnabled(true)
                .Build();

            ossClientWithRoleAndPermission.AccountService.addAccountRole(accountRole);
            result2 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in result2)
            {
                if (forAccountRole.Name.Equals(layoutPermissionsRoleName))
                {
                    layoutPermissionsRoleUid = forAccountRole.Id;
                }
            }

            //Update the newly created role
            ossClientWithRoleAndPermission.AccountService.updateAccountRole(layoutPermissionsRoleUid, accountRole);
            result2 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            //Get the newly created role
            layoutPermissionsAccountRole = ossClientWithRoleAndPermission.AccountService.getAccountRole(layoutPermissionsRoleUid);

            //Delete the newly created role
            ossClientWithRoleAndPermission.AccountService.deleteAccountRole(layoutPermissionsRoleUid);
            result3 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();
        }
    }
}