namespace OneSpanSign.Sdk.Builder
{
    public class IdvWorkflowBuilder
    {
        private string id;
        private string type;
        private string tenant;
        private string desc;

        private IdvWorkflowBuilder(string id)
        {
            this.id = id;
        }

        public static IdvWorkflowBuilder NewIdvWorkflow(string id)
        {
            if (id == null)
            {
                throw new BuilderException("IdvWorkflow id cannot be null.");
            }

            return new IdvWorkflowBuilder(id);
        }

        public IdvWorkflowBuilder WithType(string type)
        {
            this.type = type;
            return this;
        }
        
        public IdvWorkflowBuilder WithTenant(string tenant)
        {
            this.tenant = tenant;
            return this;
        }
        
        public IdvWorkflowBuilder WithDesc(string desc)
        {
            this.desc = desc;
            return this;
        }
        

        public IdvWorkflow Build()
        {
            IdvWorkflow idvWorkflow = new IdvWorkflow();
            idvWorkflow.Id = id;
            idvWorkflow.Type = type;
            idvWorkflow.Tenant = tenant;
            idvWorkflow.Desc = desc;
            return idvWorkflow;
        }
    }
}