using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.Sdk.Internal.Conversion;

namespace OneSpanSign.Sdk
{
    internal class AccountConfigClient
    {
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;
        private string baseUrl;

        public AccountConfigClient(RestClient restClient, string apiUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            this.jsonSettings = jsonSettings;
            this.baseUrl = apiUrl;
        }

        public API.Handover GetHandoverUrl(string language)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.HANDOVER_URL_PATH)
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.HANDOVER_URL_PATH)
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.HANDOVER_URL_PATH)
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.HANDOVER_URL_PATH)
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language).Build();

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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
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

        public AccountSettings GetAccountSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountSettings>(stringResponse, jsonSettings);
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountSettings,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });

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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SETTINGS_PATH).Build();

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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_FEATURE_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountFeatureSettings>(stringResponse, jsonSettings);
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_FEATURE_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountFeatureSettings,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_FEATURE_SETTINGS_PATH).Build();

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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_PACKAGE_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountPackageSettings>(stringResponse, jsonSettings);
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_PACKAGE_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountPackageSettings,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_PACKAGE_SETTINGS_PATH).Build();

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
        
        public AccountDesignerSettings GetAccountDesignerSettings() 
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_DESIGNER_SETTINGS_PATH).Build();
            try
            {
                String stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountDesignerSettings>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the account designer settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the account designer settings.", e);
            }
        }

        public void PatchAccountDesignerSettings(AccountDesignerSettings accountDesignerSettings)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_DESIGNER_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountDesignerSettings,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save the account designer settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save the account designer settings.", e);
            }
        }

        public void DeleteAccountDesignerSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_DESIGNER_SETTINGS_PATH).Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the account designer settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the account designer settings.", e);
            }
        }

        public AccountEmailReminderSettings GetAccountEmailReminderSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_EMAIL_REMINDER_SETTINGS_PATH).Build();
            try
            {
                String stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountEmailReminderSettings>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the account email reminder settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the account email reminder settings.", e);
            }
        }

        public void PatchAccountEmailReminderSettings(AccountEmailReminderSettings accountEmailReminderSettings)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_EMAIL_REMINDER_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountEmailReminderSettings,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save the account email reminder settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save the account email reminder settings.", e);
            }
        }

        public void DeleteAccountEmailReminderSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_EMAIL_REMINDER_SETTINGS_PATH).Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the account email reminder settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the account email reminder settings.", e);
            }
        }

        public AccountUploadSettings GetAccountUploadSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_UPLOAD_SETTINGS_PATH).Build();
            try
            {
                string stringResponse = restClient.Get(path);
                List<string> listFromJson = JsonConvert.DeserializeObject<List<String>>(stringResponse, jsonSettings);
                AccountUploadSettings accountUploadSettings = new AccountUploadSettings();
                accountUploadSettings.AllowedFileTypes = listFromJson;
                return new AccountUploadSettingsConverter(accountUploadSettings).ToSDKAccountUploadSettings();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the account upload settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the account upload settings.", e);
            }
        }

        public void UpdateAccountUploadSettings(AccountUploadSettings accountUploadSettings)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_UPLOAD_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountUploadSettings.AllowedFileTypes,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
            try
            {
                string json = JsonConvert.SerializeObject(accountUploadSettings.AllowedFileTypes);
                restClient.Put(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save the account upload settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save the account upload settings.", e);
            }
        }

        public void DeleteAccountUploadSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_UPLOAD_SETTINGS_PATH).Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the account upload settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the account upload settings.", e);
            }
        }

        public AccountSystemSettingProperties GetAccountSystemSettingProperties()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SYSTEM_SETTING_PROPERTIES_PATH).Build();
            try
            {
                String stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<AccountSystemSettingProperties>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the account system settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the account system settings.", e);
            }
        }

        public void PatchAccountSystemSettingProperties(AccountSystemSettingProperties accountSystemSettingProperties)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SYSTEM_SETTING_PROPERTIES_PATH).Build();
            string payload = JsonConvert.SerializeObject(accountSystemSettingProperties,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save the account system settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save the account system settings.", e);
            }
        }

        public void DeleteAccountSystemSettingProperties()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SYSTEM_SETTING_PROPERTIES_PATH).Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the account system settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the account system settings.", e);
            }
        }
  
        public SignatureLayout GetSignatureLayout()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SIGNATURE_LAYOUT_PATH).Build();
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_SIGNATURE_LAYOUT_PATH).Build();
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
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.IF_WORKFLOW_CONFIGS_PATH).Build();
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
        
        public SupportedLanguages GetAccountLimitSupportedLanguagesSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_LIMIT_SUPPORTED_LANGUAGES_SETTINGS_PATH).Build();
            try
            {
                String stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<SupportedLanguages>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the account limit supported languages settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the account limit supported languages settings.", e);
            }
        }

        public void SaveAccountLimitSupportedLanguagesSettings(SupportedLanguages supportedLanguages)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_LIMIT_SUPPORTED_LANGUAGES_SETTINGS_PATH).Build();
            string payload = JsonConvert.SerializeObject(supportedLanguages,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not save the account limit supported languages settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not save the account limit supported languages settings.", e);
            }
        }
        
        public void DeleteAccountLimitSupportedLanguagesSettings()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ACCOUNT_LIMIT_SUPPORTED_LANGUAGES_SETTINGS_PATH).Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the account limit supported languages settings.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the account limit supported languages settings.", e);
            }
        }
    }
}