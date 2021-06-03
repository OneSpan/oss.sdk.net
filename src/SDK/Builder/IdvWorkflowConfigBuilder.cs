namespace OneSpanSign.Sdk.Builder
{
    public class IdvWorkflowConfigBuilder
    {
        private string id;
        private string type;
        private string tenant;
        private string desc;
        private bool skipWhenAccessingSignedDocuments;

        private IdvWorkflowConfigBuilder(string id)
        {
            this.id = id;
        }

        public static IdvWorkflowConfigBuilder NewIdvWorkflowConfig(string id)
        {
            if (id == null)
            {
                throw new BuilderException("IdvWorkflowConfig id cannot be null.");
            }

            return new IdvWorkflowConfigBuilder(id);
        }

        public IdvWorkflowConfigBuilder WithType(string type)
        {
            this.type = type;
            return this;
        }

        public IdvWorkflowConfigBuilder WithTenant(string tenant)
        {
            this.tenant = tenant;
            return this;
        }

        public IdvWorkflowConfigBuilder WithDesc(string desc)
        {
            this.desc = desc;
            return this;
        }

        public IdvWorkflowConfigBuilder EnableSkipWhenAccessingSignedDocuments()
        {
            this.skipWhenAccessingSignedDocuments = true;
            return this;
        }

        public IdvWorkflowConfigBuilder DisableSkipWhenAccessingSignedDocuments()
        {
            this.skipWhenAccessingSignedDocuments = false;
            return this;
        }

        public IdvWorkflowConfig Build()
        {
            IdvWorkflowConfig idvWorkflowConfig = new IdvWorkflowConfig();
            idvWorkflowConfig.Id = id;
            idvWorkflowConfig.Type = type;
            idvWorkflowConfig.Tenant = tenant;
            idvWorkflowConfig.Desc = desc;
            idvWorkflowConfig.SkipWhenAccessingSignedDocuments = skipWhenAccessingSignedDocuments;
            return idvWorkflowConfig;
        }
    }
}