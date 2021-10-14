using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;

namespace SDK.Examples
{
    public class UserAccountRolesExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new UserAccountRolesExample().Run();
        }
        public List<UserAccountRole> result1 = null;
        public List<UserAccountRole> result2 = null;

        protected OssClient ossClientWithRoleAndPermission;
        override public void Execute()
        {
            //NEED TO REPLACE THE PROPER VALUES
            const string USER_ID = "FakeUserId";
            const string ACCOUNT_ID = "FakeAccountId";
            const string ROLE_ID = "FakeRoleId";
            const string DESCRIPTION = "FakeDescription";
            const string SENDER_PERMSSION = "sender_admin.users";
            
            //Setup ossclient for role and permission enabled user
            ossClientWithRoleAndPermission = new OssClient(props.Get("api.key.withRolesAndPermission"), props.Get("api.url"), props.Get("webpage.url"), true);
            senderEmail = props.Get("sender.email.withRolesAndPermission");

            //Get a role for specific user
            result1 = ossClientWithRoleAndPermission.AccountService.getUserRoles(USER_ID);
            
            //Get a role for specific user under an account
            result2 = ossClientWithRoleAndPermission.AccountService.getUserRoles(USER_ID, ACCOUNT_ID);
            
            //assign AccountRole to a user
            string newAccountRoleName = Guid.NewGuid().ToString();
            OneSpanSign.Sdk.AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithName(newAccountRoleName)
                .WithPermissions(new List<string>() { SENDER_PERMSSION })
                .WithDescription(DESCRIPTION)
                .WithEnabled(true)
                .WithId(ROLE_ID)
                .Build();

            List<OneSpanSign.Sdk.AccountRole> fakeAccountRole = new List<OneSpanSign.Sdk.AccountRole>();
            fakeAccountRole.Add(accountRole);
            
            OneSpanSign.Sdk.UserAccountRole userAccountRole = UserAccountRoleBuilder.NewUserAccountRole()
                .WithAccountId(ACCOUNT_ID)
                .WithAccountRoles(fakeAccountRole)
                .WithUserId(USER_ID)
                .Build();
            
            ossClientWithRoleAndPermission.AccountService.assignAccountRoleToUser(USER_ID, userAccountRole);

        }

    }
}