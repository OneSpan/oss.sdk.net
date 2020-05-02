
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class AccountProvidersConverterTest
    {
        private AccountProviders sdkAccountProviders = null;
        private OneSpanSign.API.AccountProviders apiAccountProviders = null;
        private AccountProvidersConverter converter = null;

        private static readonly string ACC_PROV_DOC_ID = "doc_provider_id";
        private static readonly string ACC_PROV_USR_ID = "usr_provider_id";
        private static readonly string ACC_PROV_DOC_NAME = "doc_provider_name";
        private static readonly string ACC_PROV_USR_NAME = "usr_provider_name";
        private static readonly string ACC_PROV_DOC_PROVIDES = "doc_provider_provides";
        private static readonly string ACC_PROV_USR_PROVIDES = "usr_provider_provides";

        private static readonly IDictionary<string, object> ACC_PROV_DOC_DATA = new Dictionary<string, object>()
            {{"provider_doc_0_data_0_key", "provider_doc_0_data_0_value"}};

        private static readonly IDictionary<string, object> ACC_PROV_USR_DATA = new Dictionary<string, object>()
            {{"provider_usr_0_data_0_key", "provider_usr_0_data_0_value"}};

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiAccountProviders = null;
            converter = new AccountProvidersConverter(apiAccountProviders);
            Assert.IsNull(converter.ToSDKAccountProviders());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkAccountProviders = null;
            converter = new AccountProvidersConverter(sdkAccountProviders);
            Assert.IsNull(converter.ToSDKAccountProviders());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkAccountProviders = CreateTypicalSDKAccountProviders();
            converter = new AccountProvidersConverter(sdkAccountProviders);
            AccountProviders accountProviders = converter.ToSDKAccountProviders();

            Assert.IsNotNull(accountProviders);
            Assert.AreEqual(sdkAccountProviders, accountProviders);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiAccountProviders = CreateTypicalAPIAccountProviders();
            converter = new AccountProvidersConverter(apiAccountProviders);
            AccountProviders accountProviders = converter.ToSDKAccountProviders();

            Assert.IsNotNull(accountProviders);
            Assert.AreEqual(1, accountProviders.Documents.Count);
            Assert.AreEqual(ACC_PROV_DOC_NAME, accountProviders.Documents[0].Name);
            Assert.AreEqual(1, accountProviders.Users.Count);
            Assert.AreEqual(ACC_PROV_USR_NAME, accountProviders.Users[0].Name);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkAccountProviders = null;
            converter = new AccountProvidersConverter(sdkAccountProviders);

            Assert.IsNull(converter.ToAPIAccountProviders());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiAccountProviders = null;
            converter = new AccountProvidersConverter(apiAccountProviders);

            Assert.IsNull(converter.ToAPIAccountProviders());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiAccountProviders = CreateTypicalAPIAccountProviders();
            converter = new AccountProvidersConverter(apiAccountProviders);

            OneSpanSign.API.AccountProviders accountProviders = converter.ToAPIAccountProviders();

            Assert.IsNotNull(accountProviders);
            Assert.AreEqual(apiAccountProviders, accountProviders);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkAccountProviders = CreateTypicalSDKAccountProviders();
            converter = new AccountProvidersConverter(sdkAccountProviders);

            OneSpanSign.API.AccountProviders accountProviders = converter.ToAPIAccountProviders();

            Assert.IsNotNull(accountProviders);
            Assert.AreEqual(1, accountProviders.Documents.Count);
            Assert.AreEqual(ACC_PROV_DOC_NAME, accountProviders.Documents[0].Name);
            Assert.AreEqual(1, accountProviders.Users.Count);
            Assert.AreEqual(ACC_PROV_USR_NAME, accountProviders.Users[0].Name);
        }

        private AccountProviders CreateTypicalSDKAccountProviders()
        {
            AccountProviders accountProviders = new AccountProviders();
            Provider provider = new Provider();
            provider.Id = ACC_PROV_DOC_ID;
            provider.Name = ACC_PROV_DOC_NAME;
            provider.Data = ACC_PROV_DOC_DATA;
            provider.Provides = ACC_PROV_DOC_PROVIDES;
            accountProviders.AddDocument(provider);
            Provider provider1 = new Provider();
            provider1.Id = ACC_PROV_USR_ID;
            provider1.Name = ACC_PROV_USR_NAME;
            provider1.Data = ACC_PROV_USR_DATA;
            provider1.Provides = ACC_PROV_USR_PROVIDES;
            accountProviders.AddUser(provider1);

            return accountProviders;
        }

        private OneSpanSign.API.AccountProviders CreateTypicalAPIAccountProviders()
        {
            OneSpanSign.API.AccountProviders accountProviders = new OneSpanSign.API.AccountProviders();
            OneSpanSign.API.Provider provider = new OneSpanSign.API.Provider();
            provider.Id = ACC_PROV_DOC_ID;
            provider.Name = ACC_PROV_DOC_NAME;
            provider.Data = ACC_PROV_DOC_DATA;
            provider.Provides = ACC_PROV_DOC_PROVIDES;
            accountProviders.AddDocument(provider);
            OneSpanSign.API.Provider provider1 = new OneSpanSign.API.Provider();
            provider1.Id = ACC_PROV_USR_ID;
            provider1.Name = ACC_PROV_USR_NAME;
            provider1.Data = ACC_PROV_USR_DATA;
            provider1.Provides = ACC_PROV_USR_PROVIDES;
            accountProviders.AddUser(provider1);

            return accountProviders;
        }
    }
}