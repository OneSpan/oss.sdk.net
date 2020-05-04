using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture()]
    public class SubAccountConverterTest
    {
        private SubAccount sdkSubAccount = null;
        private OneSpanSign.API.SubAccount apiSubAccount = null;
        private SubAccountConverter converter = null;

        private static readonly string SUBACCOUNT_NAME = "subAccount_name";
        private static readonly string SUBACCOUNT_LANGUAGE = "en";
        private static readonly string SUBACCOUNT_TIMEZONE_ID = "GMT";
        private static readonly string SUBACCOUNT_PARENT_ACCOUNT_ID = "dummy_account_ID";

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiSubAccount = null;
            converter = new SubAccountConverter(apiSubAccount);
            Assert.IsNull(converter.ToSDKSubAccount());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkSubAccount = null;
            converter = new SubAccountConverter(sdkSubAccount);
            Assert.IsNull(converter.ToSDKSubAccount());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkSubAccount = CreateTypicalSDKSubAccount();
            converter = new SubAccountConverter(sdkSubAccount);
            SubAccount subAccount = converter.ToSDKSubAccount();

            Assert.IsNotNull(subAccount);
            Assert.AreEqual(sdkSubAccount, subAccount);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiSubAccount = CreateTypicalAPISubAccount();
            converter = new SubAccountConverter(apiSubAccount);
            SubAccount subAccount = converter.ToSDKSubAccount();

            Assert.IsNotNull(subAccount);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkSubAccount = null;
            converter = new SubAccountConverter(sdkSubAccount);

            Assert.IsNull(converter.ToAPISubAccount());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiSubAccount = null;
            converter = new SubAccountConverter(apiSubAccount);

            Assert.IsNull(converter.ToAPISubAccount());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiSubAccount = CreateTypicalAPISubAccount();
            converter = new SubAccountConverter(apiSubAccount);

            OneSpanSign.API.SubAccount subAccount = converter.ToAPISubAccount();

            Assert.IsNotNull(subAccount);
            Assert.AreEqual(apiSubAccount, subAccount);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkSubAccount = CreateTypicalSDKSubAccount();
            converter = new SubAccountConverter(sdkSubAccount);

            OneSpanSign.API.SubAccount subAccount = converter.ToAPISubAccount();

            Assert.IsNotNull(subAccount);
            Assert.AreEqual(SUBACCOUNT_LANGUAGE, subAccount.Language, "language was not set correctly");
            Assert.AreEqual(SUBACCOUNT_NAME, subAccount.Name, "Name was not set correctly");
            Assert.AreEqual(SUBACCOUNT_TIMEZONE_ID, subAccount.TimezoneId, "TimezoneId was not set correctly");
            Assert.AreEqual(SUBACCOUNT_PARENT_ACCOUNT_ID, subAccount.ParentAccountId,
                "ParentAccountId was not set correctly");
        }

        private SubAccount CreateTypicalSDKSubAccount()
        {
            SubAccount subAccount = new SubAccount();

            subAccount.Language = SUBACCOUNT_LANGUAGE;
            subAccount.TimezoneId = SUBACCOUNT_TIMEZONE_ID;
            subAccount.ParentAccountId = SUBACCOUNT_PARENT_ACCOUNT_ID;
            subAccount.Name = SUBACCOUNT_NAME;

            return subAccount;
        }

        private OneSpanSign.API.SubAccount CreateTypicalAPISubAccount()
        {
            OneSpanSign.API.SubAccount subAccount = new OneSpanSign.API.SubAccount();

            subAccount.Language = SUBACCOUNT_LANGUAGE;
            subAccount.TimezoneId = SUBACCOUNT_TIMEZONE_ID;
            subAccount.ParentAccountId = SUBACCOUNT_PARENT_ACCOUNT_ID;
            subAccount.Name = SUBACCOUNT_NAME;

            return subAccount;
        }
    }
}