using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Internal.Conversion
{
    public class IdvWorkflowConfigConverter
    {
        private IdvWorkflowConfiguration apiIdvWorkflowConfiguration;
        private IdvWorkflowConfig sdkIdvWorkflowConfig;

        internal IdvWorkflowConfigConverter(IdvWorkflowConfiguration apiIdvWorkflowConfiguration)
        {
            this.apiIdvWorkflowConfiguration = apiIdvWorkflowConfiguration;
        }


        internal IdvWorkflowConfigConverter(IdvWorkflowConfig sdkIdvWorkflowConfig)
        {
            this.sdkIdvWorkflowConfig = sdkIdvWorkflowConfig;
        }

        internal IdvWorkflowConfiguration ToAPIIdvWorkflowConfiguration()
        {
            if (sdkIdvWorkflowConfig == null)
            {
                return apiIdvWorkflowConfiguration;
            }

            IdvWorkflowConfiguration result = new IdvWorkflowConfiguration();
            result.Id = sdkIdvWorkflowConfig.Id;
            result.Type = sdkIdvWorkflowConfig.Type;
            result.Tenant = sdkIdvWorkflowConfig.Tenant;
            result.Desc = sdkIdvWorkflowConfig.Desc;
            result.SkipWhenAccessingSignedDocuments = sdkIdvWorkflowConfig.SkipWhenAccessingSignedDocuments;

            return result;
        }

        internal IdvWorkflowConfig ToSDKIdvWorkflowConfig()
        {
            if (apiIdvWorkflowConfiguration == null)
            {
                return sdkIdvWorkflowConfig;
            }

            IdvWorkflowConfig idvWorkflowConfig = IdvWorkflowConfigBuilder
                .NewIdvWorkflowConfig(apiIdvWorkflowConfiguration.Id)
                .WithType(apiIdvWorkflowConfiguration.Type)
                .WithTenant(apiIdvWorkflowConfiguration.Tenant)
                .WithDesc(apiIdvWorkflowConfiguration.Desc)
                .Build();
            idvWorkflowConfig.SkipWhenAccessingSignedDocuments =
                apiIdvWorkflowConfiguration.SkipWhenAccessingSignedDocuments;
            
            return idvWorkflowConfig;
        }
    }
}