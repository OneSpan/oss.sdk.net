using NUnit.Framework;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Test.Builder
{
    public class IdvWorkflowConfigBuilderTest
    {
        [Test]
        public void BuildTest()
        {
            string id = "id";
            string type = "type";
            string tenant = "tenant";
            string desc = "desc";

            IdvWorkflowConfig idvWorkflowConfig = IdvWorkflowConfigBuilder.NewIdvWorkflowConfig(id)
                .WithType(type)
                .WithTenant(tenant)
                .WithDesc(desc)
                .EnableSkipWhenAccessingSignedDocuments()
                .Build();

            Assert.AreEqual(id, idvWorkflowConfig.Id);
            Assert.AreEqual(type, idvWorkflowConfig.Type);
            Assert.AreEqual(tenant, idvWorkflowConfig.Tenant);
            Assert.AreEqual(desc, idvWorkflowConfig.Desc);
            Assert.IsTrue(idvWorkflowConfig.SkipWhenAccessingSignedDocuments);
        }
    }
}