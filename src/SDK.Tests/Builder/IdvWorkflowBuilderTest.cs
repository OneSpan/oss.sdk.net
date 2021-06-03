using NUnit.Framework;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Test.Builder
{
    public class IdvWorkflowBuilderTest
    {
        [Test]
        public void BuildTest()
        {
            string id = "id";
            string type = "type";
            string tenant = "tenant";
            string desc = "desc";

            IdvWorkflow idvWorkflow = IdvWorkflowBuilder.NewIdvWorkflow(id)
                .WithType(type)
                .WithTenant(tenant)
                .WithDesc(desc)
                .Build();

            Assert.AreEqual(id, idvWorkflow.Id);
            Assert.AreEqual(type, idvWorkflow.Type);
            Assert.AreEqual(tenant, idvWorkflow.Tenant);
            Assert.AreEqual(desc, idvWorkflow.Desc);
        }
    }
}