using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class IdvWorkflowConverterTest
    {
        private IdvWorkflow apiIdvWorkflow1 = null;
        private IdvWorkflow apiIdvWorkflow2 = null;
        private OneSpanSign.Sdk.IdvWorkflow sdkIdvWorkflow1 = null;
        private OneSpanSign.Sdk.IdvWorkflow sdkIdvWorkflow2 = null;
        private IdvWorkflowConverter converter = null;

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkIdvWorkflow1 = null;
            converter = new IdvWorkflowConverter(sdkIdvWorkflow1);
            Assert.IsNull(converter.ToAPIIdvWorkflow());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiIdvWorkflow1 = null;
            converter = new IdvWorkflowConverter(apiIdvWorkflow1);
            Assert.IsNull(converter.ToSDKIdvWorkflow());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkIdvWorkflow1 = null;
            converter = new IdvWorkflowConverter(sdkIdvWorkflow1);
            Assert.IsNull(converter.ToSDKIdvWorkflow());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiIdvWorkflow1 = null;
            converter = new IdvWorkflowConverter(apiIdvWorkflow1);
            Assert.IsNull(converter.ToAPIIdvWorkflow());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkIdvWorkflow1 = CreateTypicalSDKIdvWorkflow();
            sdkIdvWorkflow2 = new IdvWorkflowConverter(sdkIdvWorkflow1).ToSDKIdvWorkflow();

            Assert.IsNotNull(sdkIdvWorkflow2);
            Assert.AreEqual(sdkIdvWorkflow2.Id, sdkIdvWorkflow1.Id);
            Assert.AreEqual(sdkIdvWorkflow2.Type, sdkIdvWorkflow1.Type);
            Assert.AreEqual(sdkIdvWorkflow2.Tenant, sdkIdvWorkflow1.Tenant);
            Assert.AreEqual(sdkIdvWorkflow2.Desc, sdkIdvWorkflow1.Desc);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiIdvWorkflow1 = CreateTypicalAPIIdvWorkflow();
            apiIdvWorkflow2 = new IdvWorkflowConverter(apiIdvWorkflow1).ToAPIIdvWorkflow();

            Assert.IsNotNull(apiIdvWorkflow2);
            Assert.AreEqual(apiIdvWorkflow2.Id, apiIdvWorkflow1.Id);
            Assert.AreEqual(apiIdvWorkflow2.Type, apiIdvWorkflow1.Type);
            Assert.AreEqual(apiIdvWorkflow2.Tenant, apiIdvWorkflow1.Tenant);
            Assert.AreEqual(apiIdvWorkflow2.Desc, apiIdvWorkflow1.Desc);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiIdvWorkflow1 = CreateTypicalAPIIdvWorkflow();
            sdkIdvWorkflow1 = new IdvWorkflowConverter(apiIdvWorkflow1).ToSDKIdvWorkflow();

            Assert.IsNotNull(sdkIdvWorkflow1);
            Assert.AreEqual(sdkIdvWorkflow1.Id, apiIdvWorkflow1.Id);
            Assert.AreEqual(sdkIdvWorkflow1.Type, apiIdvWorkflow1.Type);
            Assert.AreEqual(sdkIdvWorkflow1.Tenant, apiIdvWorkflow1.Tenant);
            Assert.AreEqual(sdkIdvWorkflow1.Desc, apiIdvWorkflow1.Desc);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkIdvWorkflow1 = CreateTypicalSDKIdvWorkflow();
            apiIdvWorkflow1 = new IdvWorkflowConverter(sdkIdvWorkflow1).ToAPIIdvWorkflow();

            Assert.IsNotNull(apiIdvWorkflow1);
            Assert.AreEqual(apiIdvWorkflow1.Id, sdkIdvWorkflow1.Id);
            Assert.AreEqual(apiIdvWorkflow1.Type, sdkIdvWorkflow1.Type);
            Assert.AreEqual(apiIdvWorkflow1.Tenant, sdkIdvWorkflow1.Tenant);
            Assert.AreEqual(apiIdvWorkflow1.Desc, sdkIdvWorkflow1.Desc);
        }

        private OneSpanSign.Sdk.IdvWorkflow CreateTypicalSDKIdvWorkflow()
        {
            return IdvWorkflowBuilder.NewIdvWorkflow("id")
                .WithType("sdkType")
                .WithTenant("sdkTenant")
                .WithDesc("sdkDesc")
                .Build();
        }

        private IdvWorkflow CreateTypicalAPIIdvWorkflow()
        {
            IdvWorkflow idvWorkflow = new IdvWorkflow();
            idvWorkflow.Id = "id";
            idvWorkflow.Type = "apiType";
            idvWorkflow.Tenant = "apiTenant";
            idvWorkflow.Desc = "apiDesc";
            return idvWorkflow;
        }
    }
}