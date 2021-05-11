using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class IdvWorkflowConfigConverterTest
    {
        private IdvWorkflowConfiguration apiIdvWorkflowConfiguration1 = null;
        private IdvWorkflowConfiguration apiIdvWorkflowConfiguration2 = null;
        private IdvWorkflowConfig sdkIdvWorkflowConfig1 = null;
        private IdvWorkflowConfig sdkIdvWorkflowConfig2 = null;
        private IdvWorkflowConfigConverter converter = null;

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkIdvWorkflowConfig1 = null;
            converter = new IdvWorkflowConfigConverter(sdkIdvWorkflowConfig1);
            Assert.IsNull(converter.ToAPIIdvWorkflowConfiguration());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiIdvWorkflowConfiguration1 = null;
            converter = new IdvWorkflowConfigConverter(apiIdvWorkflowConfiguration1);
            Assert.IsNull(converter.ToSDKIdvWorkflowConfig());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkIdvWorkflowConfig1 = null;
            converter = new IdvWorkflowConfigConverter(sdkIdvWorkflowConfig1);
            Assert.IsNull(converter.ToSDKIdvWorkflowConfig());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiIdvWorkflowConfiguration1 = null;
            converter = new IdvWorkflowConfigConverter(apiIdvWorkflowConfiguration1);
            Assert.IsNull(converter.ToAPIIdvWorkflowConfiguration());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkIdvWorkflowConfig1 = CreateTypicalSDKIdvWorkflowConfig();
            sdkIdvWorkflowConfig2 = new IdvWorkflowConfigConverter(sdkIdvWorkflowConfig1).ToSDKIdvWorkflowConfig();

            Assert.IsNotNull(sdkIdvWorkflowConfig2);
            Assert.AreEqual(sdkIdvWorkflowConfig2.Id, sdkIdvWorkflowConfig1.Id);
            Assert.AreEqual(sdkIdvWorkflowConfig2.Type, sdkIdvWorkflowConfig1.Type);
            Assert.AreEqual(sdkIdvWorkflowConfig2.Tenant, sdkIdvWorkflowConfig1.Tenant);
            Assert.AreEqual(sdkIdvWorkflowConfig2.Desc, sdkIdvWorkflowConfig1.Desc);
            Assert.AreEqual(sdkIdvWorkflowConfig2.SkipWhenAccessingSignedDocuments, sdkIdvWorkflowConfig1.SkipWhenAccessingSignedDocuments);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiIdvWorkflowConfiguration1 = CreateTypicalAPIIdvWorkflowConfiguration();
            apiIdvWorkflowConfiguration2 = new IdvWorkflowConfigConverter(apiIdvWorkflowConfiguration1).ToAPIIdvWorkflowConfiguration();

            Assert.IsNotNull(apiIdvWorkflowConfiguration2);
            Assert.AreEqual(apiIdvWorkflowConfiguration2.Id, apiIdvWorkflowConfiguration1.Id);
            Assert.AreEqual(apiIdvWorkflowConfiguration2.Type, apiIdvWorkflowConfiguration1.Type);
            Assert.AreEqual(apiIdvWorkflowConfiguration2.Tenant, apiIdvWorkflowConfiguration1.Tenant);
            Assert.AreEqual(apiIdvWorkflowConfiguration2.Desc, apiIdvWorkflowConfiguration1.Desc);
            Assert.AreEqual(apiIdvWorkflowConfiguration2.SkipWhenAccessingSignedDocuments, apiIdvWorkflowConfiguration1.SkipWhenAccessingSignedDocuments);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiIdvWorkflowConfiguration1 = CreateTypicalAPIIdvWorkflowConfiguration();
            sdkIdvWorkflowConfig1 = new IdvWorkflowConfigConverter(apiIdvWorkflowConfiguration1).ToSDKIdvWorkflowConfig();

            Assert.IsNotNull(sdkIdvWorkflowConfig1);
            Assert.AreEqual(sdkIdvWorkflowConfig1.Id, apiIdvWorkflowConfiguration1.Id);
            Assert.AreEqual(sdkIdvWorkflowConfig1.Type, apiIdvWorkflowConfiguration1.Type);
            Assert.AreEqual(sdkIdvWorkflowConfig1.Tenant, apiIdvWorkflowConfiguration1.Tenant);
            Assert.AreEqual(sdkIdvWorkflowConfig1.Desc, apiIdvWorkflowConfiguration1.Desc);
            Assert.AreEqual(sdkIdvWorkflowConfig1.SkipWhenAccessingSignedDocuments, apiIdvWorkflowConfiguration1.SkipWhenAccessingSignedDocuments);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkIdvWorkflowConfig1 = CreateTypicalSDKIdvWorkflowConfig();
            apiIdvWorkflowConfiguration1 = new IdvWorkflowConfigConverter(sdkIdvWorkflowConfig1).ToAPIIdvWorkflowConfiguration();

            Assert.IsNotNull(apiIdvWorkflowConfiguration1);
            Assert.AreEqual(apiIdvWorkflowConfiguration1.Id, sdkIdvWorkflowConfig1.Id);
            Assert.AreEqual(apiIdvWorkflowConfiguration1.Type, sdkIdvWorkflowConfig1.Type);
            Assert.AreEqual(apiIdvWorkflowConfiguration1.Tenant, sdkIdvWorkflowConfig1.Tenant);
            Assert.AreEqual(apiIdvWorkflowConfiguration1.Desc, sdkIdvWorkflowConfig1.Desc);
            Assert.AreEqual(apiIdvWorkflowConfiguration1.SkipWhenAccessingSignedDocuments, sdkIdvWorkflowConfig1.SkipWhenAccessingSignedDocuments);
        }

        private IdvWorkflowConfig CreateTypicalSDKIdvWorkflowConfig()
        {
            return IdvWorkflowConfigBuilder.NewIdvWorkflowConfig("id")
                .WithType("sdkType")
                .WithTenant("sdkTenant")
                .WithDesc("sdkDesc")
                .EnableSkipWhenAccessingSignedDocuments()
                .Build();
        }

        private IdvWorkflowConfiguration CreateTypicalAPIIdvWorkflowConfiguration()
        {
            IdvWorkflowConfiguration idvWorkflowConfiguration = new IdvWorkflowConfiguration();
            idvWorkflowConfiguration.Id = "id";
            idvWorkflowConfiguration.Type = "apiType";
            idvWorkflowConfiguration.Tenant = "apiTenant";
            idvWorkflowConfiguration.Desc = "apiDesc";
            idvWorkflowConfiguration.SkipWhenAccessingSignedDocuments = true;
            return idvWorkflowConfiguration;
        }
    }
}