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
            //Setup ossclient for role and permission enabled user
            ossClientWithRoleAndPermission = new OssClient(props.Get("api.key.withRolesAndPermission"), props.Get("api.url"), props.Get("webpage.url"), true);
            senderEmail = props.Get("sender.email.withRolesAndPermission");

            //Get a role for specific user
            string userId1 = "qzyGltUv6N87";
            result1 = ossClientWithRoleAndPermission.AccountService.getUserRoles(userId1);
            
            //Get a role for specific user under an account
            string userId2 = "qzyGltUv6N87";
            string accountId = "FakeAccountId";
            result2 = ossClientWithRoleAndPermission.AccountService.getUserRoles(userId2, accountId);
            
            //assign AccountRole to a user
            string newAccountRoleName = Guid.NewGuid().ToString();
            OneSpanSign.Sdk.AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithName(newAccountRoleName)
                .WithPermissions(new List<string>() { "sender_admin.users" })
                .WithDescription("DESCRIPTION")
                .WithEnabled(true)
                .Build();

            List<OneSpanSign.Sdk.AccountRole> fakeAccountRole = new List<OneSpanSign.Sdk.AccountRole>();
            fakeAccountRole.Add(accountRole);
            
            OneSpanSign.Sdk.UserAccountRole userAccountRole = UserAccountRoleBuilder.NewUserAccountRole()
                .WithAccountId("FakeAccountId")
                .WithAccountRoles(fakeAccountRole)
                .WithUserId("FakeUserId")
                .Build();

        }

    }
}