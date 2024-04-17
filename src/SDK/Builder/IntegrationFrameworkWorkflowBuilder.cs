namespace OneSpanSign.Sdk
{
    public class IntegrationFrameworkWorkflowBuilder
    {
        private string name;
        private Connector connector;

        private IntegrationFrameworkWorkflowBuilder()
        {
        }

        public static IntegrationFrameworkWorkflowBuilder NewIntegrationFrameworkWorkflow()
        {
            return new IntegrationFrameworkWorkflowBuilder();
        }

        public IntegrationFrameworkWorkflowBuilder WithName(string name) {
            this.name = name;
            return this;
        }

        public IntegrationFrameworkWorkflowBuilder WithConnector(Connector connector) {
            this.connector = connector;
            return this;
        }

        public IntegrationFrameworkWorkflow Build() {
            IntegrationFrameworkWorkflow result = new IntegrationFrameworkWorkflow();
            result.Name = name;
            result.Connector = connector;
            
            return result;
        }
    }
}