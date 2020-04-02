using System;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Services
{
    public class DataRetentionSettingsService
    {
        private UrlTemplate template;
        private RestClient restClient;

        public DataRetentionSettingsService (RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);
        }

        /// <summary>
        /// Gets the expiry time configuration.
        /// 
        /// </summary>
        /// <returns>Expiry time configuration.</returns>
        public ExpiryTimeConfiguration GetExpiryTimeConfiguration ()
        {
            String path = template.UrlFor (UrlTemplate.EXPIRY_TIME_CONFIGURATION_PATH)
                                  .Build ();
            String stringResponse;
            try 
            {
                stringResponse = restClient.Get (path);
            } 
            catch (OssServerException e) 
            {
                throw new OssServerException ("Could not get expiryTimeConfiguration." + " Exception: " + e.Message, e.ServerError, e);
            } 
            catch (Exception e) 
            {
                throw new OssException ("Could not get expiryTimeConfiguration." + " Exception: " + e.Message, e);
            }

            OneSpanSign.API.ExpiryTimeConfiguration expiryTimeConfiguration = JsonConvert.DeserializeObject<OneSpanSign.API.ExpiryTimeConfiguration> (stringResponse);
            ExpiryTimeConfigurationConverter converter = new ExpiryTimeConfigurationConverter (expiryTimeConfiguration);
            return converter.ToSDKExpiryTimeConfiguration();
        }

        /// <summary>
        /// Update expiry time configuration for account.
        /// </summary>
        /// <param name="expiryTimeConfiguration">expiryTimeConfiguration.</param>
        public void SetExpiryTimeConfiguration (ExpiryTimeConfiguration expiryTimeConfiguration)
        {
            String path = template.UrlFor (UrlTemplate.EXPIRY_TIME_CONFIGURATION_PATH)
                                  .Build ();
            ExpiryTimeConfigurationConverter converter = new ExpiryTimeConfigurationConverter (expiryTimeConfiguration);
            String expiryTimeConfigurationJson = JsonConvert.SerializeObject(converter.ToAPIExpiryTimeConfiguration ());

            try 
            {
                restClient.Put (path, expiryTimeConfigurationJson);
            } 
            catch (OssServerException e) 
            {
                throw new OssServerException ("Could not update expiryTimeConfiguration" + " Exception: " + e.Message, e.ServerError, e);
            } 
            catch (Exception e) 
            {
                throw new OssException ("Could not update expiryTimeConfiguration" + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Gets the data management policy.
        /// 
        /// </summary>
        /// <returns>Data Management Policy.</returns>
        public DataManagementPolicy GetDataManagementPolicy ()
        {
            String path = template.UrlFor (UrlTemplate.DATA_MANAGEMENT_POLICY_PATH)
                                  .Build ();
            String stringResponse;
            try 
            {
                stringResponse = restClient.Get (path);
            } 
            catch (OssServerException e) 
            {
                throw new OssServerException ("Could not get dataManagementPolicy." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) 
            {
                throw new OssException ("Could not get dataManagementPolicy." + " Exception: " + e.Message, e);
            }

            OneSpanSign.API.DataManagementPolicy dataManagementPolicy = JsonConvert.DeserializeObject<OneSpanSign.API.DataManagementPolicy> (stringResponse);
            DataManagementPolicyConverter converter = new DataManagementPolicyConverter (dataManagementPolicy);
            return converter.ToSDKDataManagementPolicy ();
        }

        /// <summary>
        /// Update data management policy for account.
        /// </summary>
        /// <param name="dataManagementPolicy">dataManagementPolicy.</param>
        public void SetDataManagementPolicy (DataManagementPolicy dataManagementPolicy)
        {
            String path = template.UrlFor (UrlTemplate.DATA_MANAGEMENT_POLICY_PATH)
                                  .Build ();
            DataManagementPolicyConverter converter = new DataManagementPolicyConverter (dataManagementPolicy);
            String dataManagementPolicyJson = JsonConvert.SerializeObject (converter.ToAPIDataManagementPolicy ());

            try 
            {
                restClient.Put (path, dataManagementPolicyJson);
            } 
            catch (OssServerException e) 
            {
                throw new OssServerException ("Could not update dataManagementPolicy" + " Exception: " + e.Message, e.ServerError, e);
            } 
            catch (Exception e) 
            {
                throw new OssException ("Could not update dataManagementPolicy" + " Exception: " + e.Message, e);
            }
        }
    }
}
