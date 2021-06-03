using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class AccountRoleExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            List<AccountRole> accountRoles1 = null;
            List<AccountRole> accountRoles2 = null;
            AccountRole newAccountRole = null;
            AccountRolesExample example = new AccountRolesExample();

            example.Run();
            accountRoles1 = example.result1;
            accountRoles2 = example.result2;

            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in accountRoles2)
            {
                if (forAccountRole.Id.Equals(example.newAccountRoleId))
                {
                    newAccountRole = forAccountRole;
                }
            }

            Assert.GreaterOrEqual(accountRoles1.Count, 1);
            Assert.GreaterOrEqual(accountRoles2.Count, 1);
            Assert.AreEqual(accountRoles1.Count + 1, accountRoles2.Count);
            Assert.NotNull(newAccountRole);
            Assert.NotNull(example.newAccountRole);
            Assert.AreEqual("NEW - DESCRIPTION", newAccountRole.Description);

            newAccountRole = null;
            foreach (OneSpanSign.Sdk.AccountRole forAccountRole in example.result3)
            {
                if (forAccountRole.Id.Equals(example.newAccountRoleId))
                {
                    newAccountRole = forAccountRole;
                }
            }

            Assert.NotNull(newAccountRole);
            Assert.NotNull(example.newAccountRole);
        }
    }
}