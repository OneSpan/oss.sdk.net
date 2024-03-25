using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    internal class AccountConfigClient
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;

        public AccountConfigClient(RestClient restClient, string apiUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            this.template = new UrlTemplate(apiUrl);
            this.jsonSettings = jsonSettings;
        }

        public API.Handover GetHandoverUrl(string language)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                string stringResponse = restClient.Get(path);
                API.Handover apiResponse = JsonConvert.DeserializeObject<API.Handover>(stringResponse, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get handover url.", e);
            }
        }

        public API.Handover CreateHandoverUrl(string language, API.Handover handover)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(handover, jsonSettings);
                string stringResponse = restClient.Post(path, json);

                API.Handover apiResponse = JsonConvert.DeserializeObject<API.Handover>(stringResponse, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create handover url.", e);
            }
        }

        public API.Handover UpdateHandoverUrl(string language, API.Handover handover)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(handover, jsonSettings);
                string stringResponse = restClient.Put(path, json);

                API.Handover apiResponse = JsonConvert.DeserializeObject<API.Handover>(stringResponse, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update handover url.", e);
            }
        }

        public void DeleteHandoverUrl(string language)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete handover url.", e);
            }
        }

        public IList<string> CreateDeclineReasons(IList<string> declineReasons, string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language).Build();

            try
            {
                string serializeObject = JsonConvert.SerializeObject(declineReasons);
                string stringResponse = restClient.Post(path, serializeObject);
                return JsonConvert.DeserializeObject<IList<string>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not create decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw new OssException("Could not create decline reasons for account." + " Exception: " + e.Message, e);
            }
        }

        public IList<string> UpdateDeclineReasons(IList<string> declineReasons, string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
                .Build();

            try
            {
                string serializeObject = JsonConvert.SerializeObject(declineReasons);
                string stringResponse = restClient.Put(path, serializeObject);
                return JsonConvert.DeserializeObject<IList<string>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not update decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw new OssException("Could not update decline reasons for account." + " Exception: " + e.Message, e);
            }
        }

        public IList<string> GetDeclineReasons(string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
                .Build();

            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<string>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not get decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw new OssException("Could not get decline reasons for account." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteDeclineReasons(string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
                .Build();

            try
            {
                string stringResponse = restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not get decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw new OssException("Could not get decline reasons for account." + " Exception: " + e.Message, e);
            }
        }


        public IList<IdvWorkflowConfiguration> GetIdvWorkflowConfigs()
        {
            string path = template.UrlFor(UrlTemplate.IDV_WORKFLOW_CONFIGS_PATH)
                .Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<IdvWorkflowConfiguration>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get IdvWorkflow Configs.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get IdvWorkflow Configs.", e);
            }
        }

        public IList<IdvWorkflowConfiguration> CreateIdvWorkflowConfigs(IList<IdvWorkflowConfiguration> idvWorkflowConfigurations)
        {
            string path = template.UrlFor(UrlTemplate.IDV_WORKFLOW_CONFIGS_PATH)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(idvWorkflowConfigurations);
                string stringResponse = restClient.Post(path, json);

                return JsonConvert.DeserializeObject<IList<IdvWorkflowConfiguration>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create IdvWorkflow Configs.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create IdvWorkflow Configs.", e);
            }
        }

        public IList<IdvWorkflowConfiguration> UpdateIdvWorkflowConfigs(IList<IdvWorkflowConfiguration> idvWorkflowConfigurations)
        {
            string path = template.UrlFor(UrlTemplate.IDV_WORKFLOW_CONFIGS_PATH)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(idvWorkflowConfigurations);
                string stringResponse = restClient.Put(path, json);

                return JsonConvert.DeserializeObject<IList<IdvWorkflowConfiguration>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update IdvWorkflow Configs.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update IdvWorkflow Configs.", e);
            }
        }

        public void DeleteIdvWorkflowConfigs()
        {
            string path = template.UrlFor(UrlTemplate.IDV_WORKFLOW_CONFIGS_PATH)
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete IdvWorkflow Configs.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete IdvWorkflow Configs.", e);
            }
        }
        
        public AccountSettings GetAccountSettings()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountSettings> (stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get Account Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get Account Settings.", e);
            }
        }
        
        public void PatchAccountSettings(AccountSettings accountSettings)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountSettings, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver (), Formatting = Formatting.Indented ,NullValueHandling = NullValueHandling.Ignore});
            
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save Account Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save Account Settings.", e);
            }
        }
        
        public void DeleteAccountSettings()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SETTINGS_PATH).Build();

            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete Account Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete Account Settings.", e);
            }
        }
        
        public AccountFeatureSettings GetAccountFeatureSettings()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_FEATURE_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountFeatureSettings> (stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get Account Feature Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get Account Feature Settings.", e);
            }
        }
        
        public void PatchAccountFeatureSettings(AccountFeatureSettings accountFeatureSettings)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_FEATURE_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountFeatureSettings, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver (), Formatting = Formatting.Indented ,NullValueHandling = NullValueHandling.Ignore});
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save Account Feature Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save Account Feature Settings.", e);
            }
        }
        
        public void DeleteAccountFeatureSettings()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_FEATURE_SETTINGS_PATH).Build();

            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete Account Feature Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete Account Feature Settings.", e);
            }
        }
        
        public AccountPackageSettings GetAccountPackageSettings()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_PACKAGE_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountPackageSettings> (stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get Account Package Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get Account Package Settings.", e);
            }
        }
        
        public void PatchAccountPackageSettings(AccountPackageSettings accountPackageSettings)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_PACKAGE_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountPackageSettings, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver (), Formatting = Formatting.Indented ,NullValueHandling = NullValueHandling.Ignore});
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save Account Package Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save Account Package Settings.", e);
            }
        }
        
        public void DeleteAccountPackageSettings()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_PACKAGE_SETTINGS_PATH).Build();

            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete Account Package Settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete Account Package Settings.", e);
            }
        }

        public SignatureLayout GetSignatureLayout()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SIGNATURE_LAYOUT_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<SignatureLayout> (stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get Account Signature Layout.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get Account Signature Layout.", e);
            }
        }
        
        public void PatchSignatureLayout(SignatureLayout signatureLayout)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SIGNATURE_LAYOUT_PATH).Build();
            string payload = JsonConvert.SerializeObject(signatureLayout, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver (), Formatting = Formatting.Indented ,NullValueHandling = NullValueHandling.Ignore});
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save Account Signature Layout.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save Account Signature Layout.", e);
            }
        }

        public IList<IntegrationFrameworkWorkflow> GetIfWorkflowsConfigs()
        {
            string path = template.UrlFor(UrlTemplate.IF_WORKFLOW_CONFIGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return IntegrationFrameworkWorkflowConverter.ToSDKList(JsonConvert.DeserializeObject<IList<OneSpanSign.API.IntegrationFrameworkWorkflow>>(stringResponse, jsonSettings));
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get IfWorkflows Configs.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get IfWorkflows Configs.", e);
            }
        }
        
    }
}