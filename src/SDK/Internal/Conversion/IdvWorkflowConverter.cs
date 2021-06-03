using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Internal.Conversion
{
    public class IdvWorkflowConverter
    {
        private API.IdvWorkflow apiIdvWorkflow;
        private IdvWorkflow sdkIdvWorkflow;

        internal IdvWorkflowConverter(API.IdvWorkflow apiIdvWorkflow)
        {
            this.apiIdvWorkflow = apiIdvWorkflow;
        }

        internal IdvWorkflowConverter(IdvWorkflow sdkIdvWorkflow)
        {
            this.sdkIdvWorkflow = sdkIdvWorkflow;
        }

        internal API.IdvWorkflow ToAPIIdvWorkflow()
        {
            if (sdkIdvWorkflow == null)
            {
                return apiIdvWorkflow;
            }

            API.IdvWorkflow result = new API.IdvWorkflow();
            result.Id = sdkIdvWorkflow.Id;
            result.Type = sdkIdvWorkflow.Type;
            result.Tenant = sdkIdvWorkflow.Tenant;
            result.Desc = sdkIdvWorkflow.Desc;

            return result;
        }

        internal IdvWorkflow ToSDKIdvWorkflow()
        {
            if (apiIdvWorkflow == null)
            {
                return sdkIdvWorkflow;
            }

            return IdvWorkflowBuilder
                .NewIdvWorkflow(apiIdvWorkflow.Id)
                .WithType(apiIdvWorkflow.Type)
                .WithTenant(apiIdvWorkflow.Tenant)
                .WithDesc(apiIdvWorkflow.Desc)
                .Build();
        }
    }
}