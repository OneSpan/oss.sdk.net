using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture()]
    public class ExternalConverterTest
    {

        private OneSpanSign.Sdk.External sdkExternal1 = null;
        private OneSpanSign.Sdk.External sdkExternal2 = null;
        private OneSpanSign.API.External apiExternal1 = null;
        private OneSpanSign.API.External apiExternal2 = null;
        private ExternalConverter converter = null;

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkExternal1 = null;
            converter = new ExternalConverter(sdkExternal1);
            Assert.IsNull(converter.ToAPIExternal());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiExternal1 = null;
            converter = new ExternalConverter(apiExternal1);
            Assert.IsNull(converter.ToSDKExternal());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkExternal1 = null;
            converter = new ExternalConverter(sdkExternal1);
            Assert.IsNull(converter.ToSDKExternal());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiExternal1 = null;
            converter = new ExternalConverter(apiExternal1);
            Assert.IsNull(converter.ToAPIExternal());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkExternal1 = CreateTypicalSDKExternal();
            converter = new ExternalConverter(sdkExternal1);
            sdkExternal2 = converter.ToSDKExternal();
            Assert.IsNotNull(sdkExternal2);
            Assert.AreEqual(sdkExternal2, sdkExternal1);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiExternal1 = CreateTypicalAPIExternal();
            converter = new ExternalConverter(apiExternal1);
            apiExternal2 = converter.ToAPIExternal();
            Assert.IsNotNull(apiExternal2);
            Assert.AreEqual(apiExternal2, apiExternal1);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiExternal1 = CreateTypicalAPIExternal();
            sdkExternal1 = new ExternalConverter(apiExternal1).ToSDKExternal();

            Assert.IsNotNull(sdkExternal1);
            Assert.AreEqual(apiExternal1.Provider, sdkExternal1.Provider);
            Assert.AreEqual(apiExternal1.ProviderName, sdkExternal1.ProviderName);
            Assert.AreEqual(apiExternal1.Id, sdkExternal1.Id);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkExternal1 = CreateTypicalSDKExternal();
            apiExternal1 = new ExternalConverter(sdkExternal1).ToAPIExternal();

            Assert.IsNotNull(apiExternal1);
            Assert.AreEqual(sdkExternal1.Provider, apiExternal1.Provider);
            Assert.AreEqual(sdkExternal1.ProviderName, apiExternal1.ProviderName);
            Assert.AreEqual(sdkExternal1.Id, apiExternal1.Id);
        }

        private OneSpanSign.Sdk.External CreateTypicalSDKExternal()
        {
            OneSpanSign.Sdk.External sdkExternal = new OneSpanSign.Sdk.External();
            sdkExternal.Id = "sdkExternalId";
            sdkExternal.Provider = "sdkExternalProvider";
            sdkExternal.ProviderName = "sdkExternalProviderName";

            return sdkExternal;
        }

        private OneSpanSign.API.External CreateTypicalAPIExternal()
        {
            OneSpanSign.API.External apiExternal = new OneSpanSign.API.External();

            apiExternal.Id = "apiExternalId";
            apiExternal.Provider = "apiExternalProvider";
            apiExternal.ProviderName = "apiExternalProviderName";

            return apiExternal;
        }
    }
}

