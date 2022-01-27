using System.Collections.Generic;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal.Conversion;

namespace OneSpanSign.Sdk.Services
{
    public class AccountConfigService
    {
        private AccountConfigClient apiClient;

        internal AccountConfigService(AccountConfigClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Handover GetHandoverUrl(string language)
        {
            API.Handover apiHandover = apiClient.GetHandoverUrl(language);
            return new HandoverConverter(apiHandover).ToSDKHandover(language);
        }

        public Handover CreateHandoverUrl(Handover handover)
        {
            API.Handover apiHandover = new HandoverConverter(handover).ToAPIHandover();
            apiHandover = apiClient.CreateHandoverUrl(handover.Language, apiHandover);
            return new HandoverConverter(apiHandover).ToSDKHandover(handover.Language);
        }

        public Handover UpdateHandoverUrl(Handover handover)
        {
            API.Handover apiHandover = new HandoverConverter(handover).ToAPIHandover();
            apiHandover = apiClient.UpdateHandoverUrl(handover.Language, apiHandover);
            return new HandoverConverter(apiHandover).ToSDKHandover(handover.Language);
        }

        public void DeleteHandoverUrl(string language)
        {
            apiClient.DeleteHandoverUrl(language);
        }
        
        public IList<string> CreateDeclineReasons(IList<string> declineReasons, string language)
        {
           return apiClient.CreateDeclineReasons(declineReasons, language);
           
        }
        
        public IList<string> UpdateDeclineReasons(IList<string> declineReasons, string language)
        {
            return apiClient.UpdateDeclineReasons(declineReasons, language);
        }

        public IList<string> GetDeclineReasons(string language)
        {
            return apiClient.GetDeclineReasons(language);
        }
        
        public void DeleteDeclineReasons(string language)
        {
            apiClient.DeleteDeclineReasons(language);
        }
        
        public IList<IdvWorkflowConfig> GetIdvWorkflowConfigs()
        {
            IList<IdvWorkflowConfiguration> idvWorkflowConfigurations = apiClient.GetIdvWorkflowConfigs();
            IList<IdvWorkflowConfig> idvWorkflowConfigs = new List<IdvWorkflowConfig>();
            foreach (IdvWorkflowConfiguration idvWorkflowConfiguration in idvWorkflowConfigurations) 
            {
                idvWorkflowConfigs.Add(new IdvWorkflowConfigConverter(idvWorkflowConfiguration).ToSDKIdvWorkflowConfig());
            }
            return idvWorkflowConfigs;
        }

        public IList<IdvWorkflowConfig> CreateIdvWorkflowConfigs(IList<IdvWorkflowConfig> input)
        {
            IList<IdvWorkflowConfiguration> idvWorkflowConfigurations = new List<IdvWorkflowConfiguration>();
            foreach (IdvWorkflowConfig idvWorkflowConfig in input) 
            {
                idvWorkflowConfigurations.Add(new IdvWorkflowConfigConverter(idvWorkflowConfig).ToAPIIdvWorkflowConfiguration());
            }
            
            idvWorkflowConfigurations = apiClient.CreateIdvWorkflowConfigs(idvWorkflowConfigurations);
            
            IList<IdvWorkflowConfig> result = new List<IdvWorkflowConfig>();
            foreach (IdvWorkflowConfiguration idvWorkflowConfiguration in idvWorkflowConfigurations) 
            {
                result.Add(new IdvWorkflowConfigConverter(idvWorkflowConfiguration).ToSDKIdvWorkflowConfig());
            }

            return result;
        }

        public IList<IdvWorkflowConfig> UpdateIdvWorkflowConfigs(IList<IdvWorkflowConfig> input)
        {
            IList<IdvWorkflowConfiguration> idvWorkflowConfigurations = new List<IdvWorkflowConfiguration>();
            foreach (IdvWorkflowConfig idvWorkflowConfig in input) 
            {
                idvWorkflowConfigurations.Add(new IdvWorkflowConfigConverter(idvWorkflowConfig).ToAPIIdvWorkflowConfiguration());
            }
            
            idvWorkflowConfigurations = apiClient.UpdateIdvWorkflowConfigs(idvWorkflowConfigurations);
            
            IList<IdvWorkflowConfig> result = new List<IdvWorkflowConfig>();
            foreach (IdvWorkflowConfiguration idvWorkflowConfiguration in idvWorkflowConfigurations) 
            {
                result.Add(new IdvWorkflowConfigConverter(idvWorkflowConfiguration).ToSDKIdvWorkflowConfig());
            }

            return result;
        }

        public void DeleteIdvWorkflowConfigs()
        {
            apiClient.DeleteIdvWorkflowConfigs();
        }
        
        public AccountSettings GetAccountSettings()
        {
            return apiClient.GetAccountSettings();
        }

        public void PatchAccountSettings(AccountSettings accountSettings)
        {
            apiClient.PatchAccountSettings(accountSettings);
        }
        
        public void DeleteAccountSettings()
        {
            apiClient.DeleteAccountSettings();;
        }
        
        public AccountFeatureSettings GetAccountFeatureSettings()
        {
            return apiClient.GetAccountFeatureSettings();
        }

        public void PatchAccountFeatureSettings(AccountFeatureSettings accountFeatureSettings)
        {
            apiClient.PatchAccountFeatureSettings(accountFeatureSettings);
        }
        
        public void DeleteAccountFeatureSettings()
        {
            apiClient.DeleteAccountFeatureSettings();;
        }
        
        public AccountPackageSettings GetAccountPackageSettings()
        {
            return apiClient.GetAccountPackageSettings();
        }

        public void PatchAccountPackageSettings(AccountPackageSettings accountPackageSettings)
        {
            apiClient.PatchAccountPackageSettings(accountPackageSettings);
        }
        
        public void DeleteAccountPackageSettings()
        {
            apiClient.DeleteAccountPackageSettings();;
        }
    }
}