using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class AccountRoleConverterTest
    {
        [Test()]
        public void ConvertNullSDKToAPI()
        {
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter((AccountRole) null);
            Assert.IsNull(accountRoleConverter.ToAPIAccountRole());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter((OneSpanSign.API.AccountRole) null);
            Assert.IsNull(accountRoleConverter.ToSDKAccountRole());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter((AccountRole) null);
            Assert.IsNull(accountRoleConverter.ToSDKAccountRole());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter((OneSpanSign.API.AccountRole) null);
            Assert.IsNull(accountRoleConverter.ToAPIAccountRole());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            AccountRole sdkAccountRole = buildSdkAccountRole();
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter(sdkAccountRole);
            Assert.AreEqual(sdkAccountRole, accountRoleConverter.ToSDKAccountRole());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            OneSpanSign.API.AccountRole apiAccountRole = buildApiAccountRole();
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter(apiAccountRole);
            Assert.AreEqual(apiAccountRole, accountRoleConverter.ToAPIAccountRole());
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            OneSpanSign.API.AccountRole apiAccountRole = buildApiAccountRole();
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter(apiAccountRole);
            AccountRole accountRole = accountRoleConverter.ToSDKAccountRole();
            Assert.AreEqual("ID2", accountRole.Id);
            Assert.AreEqual("NAME2", accountRole.Name);
            Assert.AreEqual(new List<string>() {"P2"}, accountRole.Permissions);
            Assert.AreEqual("DESC2", accountRole.Description);
            Assert.IsFalse(accountRole.Enabled);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            AccountRole sdkAccountRole = buildSdkAccountRole();
            AccountRoleConverter accountRoleConverter = new AccountRoleConverter(sdkAccountRole);
            OneSpanSign.API.AccountRole accountRole = accountRoleConverter.ToAPIAccountRole();
            Assert.AreEqual("ID", accountRole.Id);
            Assert.AreEqual("NAME", accountRole.Name);
            Assert.AreEqual(new List<string>() {"P1"}, accountRole.Permissions);
            Assert.AreEqual("DESC", accountRole.Description);
            Assert.IsTrue(accountRole.Enabled);
        }

        private AccountRole buildSdkAccountRole()
        {
            return new AccountRole("ID", "NAME", new List<String>() {"P1"}, "DESC", true);
        }

        private OneSpanSign.API.AccountRole buildApiAccountRole()
        {
            return new OneSpanSign.API.AccountRole("ID2", "NAME2", new List<String>() {"P2"}, "DESC2", false);
        }
    }
}