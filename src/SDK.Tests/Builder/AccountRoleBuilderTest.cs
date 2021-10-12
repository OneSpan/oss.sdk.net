using System;
using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    public class AccountRoleBuilderTest
    {
        public AccountRoleBuilderTest()
        {
        }

        [Test]
        public void buildWithSpecifiedValues()
        {
            DocumentPackageAttributesBuilder documentPackageAttributesBuilder = new DocumentPackageAttributesBuilder()
                .WithAttribute("First Name", "Adam")
                .WithAttribute("Last Name", "Smith");
            DocumentPackageAttributes documentPackageAttributes = documentPackageAttributesBuilder.Build();
            List<string> permissions = new List<string>() { "P1", "P2" };

            AccountRole accountRole = AccountRoleBuilder.NewAccountRole()
                .WithId("ID")
                .WithName("A")
                .WithPermissions(permissions)
                .WithDescription("D")
                .WithEnabled(true)
                .Build();

            Assert.AreEqual("ID", accountRole.Id);
            Assert.AreEqual("A", accountRole.Name);
            Assert.AreEqual(permissions, accountRole.Permissions);
            Assert.AreEqual("D", accountRole.Description);
            Assert.IsTrue(accountRole.Enabled);
        }
    }
}