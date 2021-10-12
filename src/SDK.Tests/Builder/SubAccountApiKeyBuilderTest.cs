using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture]
    public class SubAccountApiKeyBuilderTest
    {
        private static readonly string SUBACCOUNT_ACCOUNTUID = "uid";
        private static readonly string SUBACCOUNT_ACCOUNTNAME = "name";
        private static readonly string SUBACCOUNT_APIKEY = "apikey";

        [Test]
        public void withSpecifiedValues()
        {
            SubAccountApiKeyBuilder subAccountApiKeyBuilder = SubAccountApiKeyBuilder.NewSubAccountApiKey()
                .WithAccountUid(SUBACCOUNT_ACCOUNTUID)
                .WithAccountName(SUBACCOUNT_ACCOUNTNAME)
                .WithApiKey(SUBACCOUNT_APIKEY);

            SubAccountApiKey result = subAccountApiKeyBuilder.Build();

            Assert.IsNotNull(result);
            Assert.AreEqual(SUBACCOUNT_ACCOUNTUID, result.AccountUid);
            Assert.AreEqual(SUBACCOUNT_ACCOUNTNAME, result.AccountName);
            Assert.AreEqual(SUBACCOUNT_APIKEY, result.ApiKey);
        }
    }
}