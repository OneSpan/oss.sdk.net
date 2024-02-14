using System;
using NUnit.Framework;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class AccountRoleWithLayoutPermissionsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {

            AccountRoleWithLayoutPermissionsExample example = new AccountRoleWithLayoutPermissionsExample();

            example.Run();

            List<AccountRole> accountRoles1 = null;
            List<AccountRole> accountRoles2 = null;
            AccountRole layoutPermissionsAccountRole = null;
            accountRoles1 = example.result1;
            accountRoles2 = example.result2;

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in accountRoles2)
            {
                if (forAccountRole.Id.Equals(example.layoutPermissionsRoleUid))
                {
                    layoutPermissionsAccountRole = forAccountRole;
                }
            }
            // accountRoles1 has initial Account Roles
            Assert.GreaterOrEqual(accountRoles1.Count, 1);
            // accountRoles1 has the new Account Role with Layout Permissions
            Assert.GreaterOrEqual(accountRoles2.Count, 1);
            Assert.AreEqual(accountRoles1.Count + 1, accountRoles2.Count);
            
            Assert.NotNull(layoutPermissionsAccountRole);
            Assert.NotNull(example.layoutPermissionsAccountRole);
            Assert.AreEqual("Account Role with Layout Permissions", layoutPermissionsAccountRole.Description);
            
            Assert.IsTrue(layoutPermissionsAccountRole.Permissions.Contains(example.SAVE_LAYOUTS_PERMISSION));
            Assert.IsTrue(layoutPermissionsAccountRole.Permissions.Contains(example.APPLY_LAYOUTS_PERMISSION));
            Assert.IsTrue(layoutPermissionsAccountRole.Permissions.Contains(example.SHARE_LAYOUTS_PERMISSION));

            layoutPermissionsAccountRole = null;

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in example.result3)
            {
                if (forAccountRole.Id.Equals(example.layoutPermissionsRoleUid))
                {
                    layoutPermissionsAccountRole = forAccountRole;
                }
            }

            Assert.IsNull(layoutPermissionsAccountRole);
            Assert.NotNull(example.layoutPermissionsAccountRole);
        }
    }
}