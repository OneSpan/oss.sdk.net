using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;

namespace SDK.Examples
{
    public class AccountRoleWithTemplatePermissionsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AccountRoleWithTemplatePermissionsExample().Run();
        }
        public String TEMPLATES_PERMISSION = "templates_layouts.templates";
        public String SHARE_TEMPLATES_PERMISSION = "templates_layouts.share_templates";
            
        public List<AccountRole> result1 = null;
        public List<AccountRole> result2 = null;
        public List<AccountRole> result3 = null;

        public String templatePermissionsRoleUid = null;
        public OneSpanSign.Sdk.AccountRole templatePermissionsAccountRole = null;

        protected OssClient ossClientWithRoleAndPermission;
        override public void Execute()
        {
            //Setup ossclient for role and permission enabled user
            ossClientWithRoleAndPermission = new OssClient(props.Get("api.key.withRolesAndPermission"), props.Get("api.url"), props.Get("webpage.url"), true);
            senderEmail = props.Get("sender.email.withRolesAndPermission");

            //Get existing roles
            result1 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            //Create a new role with Template Permissions 
            string templatePermissionsRoleName = Guid.NewGuid().ToString();
            OneSpanSign.Sdk.AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithName(templatePermissionsRoleName)
                .WithPermissions(new List<string>() { TEMPLATES_PERMISSION, SHARE_TEMPLATES_PERMISSION })
                .WithDescription("Account Role with Template Permissions")
                .WithEnabled(true)
                .Build();

            ossClientWithRoleAndPermission.AccountService.addAccountRole(accountRole);
            result2 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in result2)
            {
                if (forAccountRole.Name.Equals(templatePermissionsRoleName))
                {
                    templatePermissionsRoleUid = forAccountRole.Id;
                }
            }

            //Update the newly created role
            ossClientWithRoleAndPermission.AccountService.updateAccountRole(templatePermissionsRoleUid, accountRole);
            result2 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();

            //Get the newly created role
            templatePermissionsAccountRole = ossClientWithRoleAndPermission.AccountService.getAccountRole(templatePermissionsRoleUid);

            //Delete the newly created role
            ossClientWithRoleAndPermission.AccountService.deleteAccountRole(templatePermissionsRoleUid);
            result3 = ossClientWithRoleAndPermission.AccountService.getAccountRoles();
        }
    }
}