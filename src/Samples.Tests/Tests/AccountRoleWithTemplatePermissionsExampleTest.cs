using System;
using NUnit.Framework;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class AccountRoleWithTemplatePermissionsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {

            AccountRoleWithTemplatePermissionsExample example = new AccountRoleWithTemplatePermissionsExample();

            example.Run();

            List<AccountRole> accountRoles1 = null;
            List<AccountRole> accountRoles2 = null;
            AccountRole templatePermissionsAccountRole = null;
            accountRoles1 = example.result1;
            accountRoles2 = example.result2;

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in accountRoles2)
            {
                if (forAccountRole.Id.Equals(example.templatePermissionsRoleUid))
                {
                    templatePermissionsAccountRole = forAccountRole;
                }
            }
            // accountRoles1 has initial Account Roles
            Assert.GreaterOrEqual(accountRoles1.Count, 1);
            // accountRoles1 has the new Account Role with Template Permissions
            Assert.GreaterOrEqual(accountRoles2.Count, 1);
            Assert.AreEqual(accountRoles1.Count + 1, accountRoles2.Count);
            
            Assert.NotNull(templatePermissionsAccountRole);
            Assert.NotNull(example.templatePermissionsAccountRole);
            Assert.AreEqual("Account Role with Template Permissions", templatePermissionsAccountRole.Description);
            
            Assert.IsTrue(templatePermissionsAccountRole.Permissions.Contains(example.TEMPLATES_PERMISSION));
            Assert.IsTrue(templatePermissionsAccountRole.Permissions.Contains(example.SHARE_TEMPLATES_PERMISSION));

            templatePermissionsAccountRole = null;

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in example.result3)
            {
                if (forAccountRole.Id.Equals(example.templatePermissionsRoleUid))
                {
                    templatePermissionsAccountRole = forAccountRole;
                }
            }

            Assert.IsNull(templatePermissionsAccountRole);
            Assert.NotNull(example.templatePermissionsAccountRole);
        }
    }
}