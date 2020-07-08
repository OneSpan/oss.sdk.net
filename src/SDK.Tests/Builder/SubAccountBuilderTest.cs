using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture]
    public class SubAccountBuilderTest
    {
        private static readonly string SUBACCOUNT_NAME = "subAccount_name";
        private static readonly string SUBACCOUNT_LANGUAGE = "en";
        private static readonly string SUBACCOUNT_TIMEZONE_ID = "GMT";
        private static readonly string SUBACCOUNT_PARENT_ACCOUNT_ID = "dummy_account_ID";

        [Test]
        public void withSpecifiedValues()
        {
            SubAccountBuilder subAccountBuilder = SubAccountBuilder.NewSubAccount()
                .WithName(SUBACCOUNT_NAME)
                .WithLanguage(SUBACCOUNT_LANGUAGE)
                .WithTimezoneId(SUBACCOUNT_TIMEZONE_ID)
                .WithParentAccountId(SUBACCOUNT_PARENT_ACCOUNT_ID);

            SubAccount result = subAccountBuilder.Build();

            Assert.IsNotNull(result);
            Assert.AreEqual(SUBACCOUNT_LANGUAGE, result.Language, "language was not set correctly");
            Assert.AreEqual(SUBACCOUNT_NAME, result.Name, "Name was not set correctly");
            Assert.AreEqual(SUBACCOUNT_TIMEZONE_ID, result.TimezoneId, "TimezoneId was not set correctly");
            Assert.AreEqual(SUBACCOUNT_PARENT_ACCOUNT_ID, result.ParentAccountId,
                "ParentAccountId was not set correctly");
        }
    }
}