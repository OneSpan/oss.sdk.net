using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class SubAccountApiKeyConverterTest
    {
        private SubAccountApiKey sdkSubAccountApiKey = null;
        private OneSpanSign.API.SubAccountApiKey apiSubAccountApiKey = null;
        private SubAccountApiKeyConverter converter = null;

        private static readonly string SUBACCOUNT_ACCOUNTUID = "uid";
        private static readonly string SUBACCOUNT_ACCOUNTNAME = "name";
        private static readonly string SUBACCOUNT_APIKEY = "apikey";

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiSubAccountApiKey = null;
            converter = new SubAccountApiKeyConverter(apiSubAccountApiKey);
            Assert.IsNull(converter.ToSdkSubAccountApiKey());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkSubAccountApiKey = null;
            converter = new SubAccountApiKeyConverter(sdkSubAccountApiKey);
            Assert.IsNull(converter.ToSdkSubAccountApiKey());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkSubAccountApiKey = CreateTypicalSDKSubAccountApiKey();
            converter = new SubAccountApiKeyConverter(sdkSubAccountApiKey);
            SubAccountApiKey subAccountApiKey = converter.ToSdkSubAccountApiKey();

            Assert.IsNotNull(subAccountApiKey);
            Assert.AreEqual(sdkSubAccountApiKey, subAccountApiKey);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiSubAccountApiKey = CreateTypicalAPISubAccountApiKey();
            converter = new SubAccountApiKeyConverter(apiSubAccountApiKey);
            SubAccountApiKey subAccountApiKey = converter.ToSdkSubAccountApiKey();

            Assert.IsNotNull(subAccountApiKey);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkSubAccountApiKey = null;
            converter = new SubAccountApiKeyConverter(sdkSubAccountApiKey);

            Assert.IsNull(converter.ToAPISubAccountApiKey());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiSubAccountApiKey = null;
            converter = new SubAccountApiKeyConverter(apiSubAccountApiKey);

            Assert.IsNull(converter.ToAPISubAccountApiKey());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiSubAccountApiKey = CreateTypicalAPISubAccountApiKey();
            converter = new SubAccountApiKeyConverter(apiSubAccountApiKey);

            OneSpanSign.API.SubAccountApiKey subAccount = converter.ToAPISubAccountApiKey();

            Assert.IsNotNull(subAccount);
            Assert.AreEqual(apiSubAccountApiKey, subAccount);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkSubAccountApiKey = CreateTypicalSDKSubAccountApiKey();
            converter = new SubAccountApiKeyConverter(sdkSubAccountApiKey);

            OneSpanSign.API.SubAccountApiKey subAccountApiKey = converter.ToAPISubAccountApiKey();

            Assert.IsNotNull(subAccountApiKey);
            Assert.AreEqual(SUBACCOUNT_ACCOUNTUID, subAccountApiKey.AccountUid);
            Assert.AreEqual(SUBACCOUNT_ACCOUNTNAME, subAccountApiKey.AccountName);
            Assert.AreEqual(SUBACCOUNT_APIKEY, subAccountApiKey.ApiKey);

        }

        private SubAccountApiKey CreateTypicalSDKSubAccountApiKey()
        {
            SubAccountApiKey subAccountApiKey = new SubAccountApiKey();

            subAccountApiKey.AccountUid = SUBACCOUNT_ACCOUNTUID;
            subAccountApiKey.AccountName = SUBACCOUNT_ACCOUNTNAME;
            subAccountApiKey.ApiKey = SUBACCOUNT_APIKEY;

            return subAccountApiKey;
        }

        private OneSpanSign.API.SubAccountApiKey CreateTypicalAPISubAccountApiKey()
        {
            OneSpanSign.API.SubAccountApiKey subAccountApiKey = new OneSpanSign.API.SubAccountApiKey();

            subAccountApiKey.AccountUid = SUBACCOUNT_ACCOUNTUID;
            subAccountApiKey.AccountName = SUBACCOUNT_ACCOUNTNAME;
            subAccountApiKey.ApiKey = SUBACCOUNT_APIKEY;

            return subAccountApiKey;
        }
    }
}