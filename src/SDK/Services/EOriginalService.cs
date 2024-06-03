using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;
using System.Collections.Generic;
using System.Collections;
using OneSpanSign.Sdk.Internal.Conversion;

namespace OneSpanSign.Sdk.Services
{
    public class EOriginalService
    {
        private RestClient restClient;
        private UrlTemplate template;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneSpanSign.Sdk.PackageService"/> class.
        /// </summary>
        /// <param name="apiToken">API token.</param>
        /// <param name="baseUrl">Base URL.</param>
        public EOriginalService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            template = new UrlTemplate(baseUrl);
            this.settings = settings;
            reportService = new ReportService(restClient, baseUrl, settings);
        }

        internal void UpdateVaultingData(PackageId packageId, Package package)
        {
            string path = template.UrlFor(UrlTemplate.E_ORIGINAL_VAULTING_DATA_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            try
            {
                restClient.Put(path, JsonConvert.SerializeObject(package, settings));
                restClient.GetBytes(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to update VaultingData." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to update VaultingData." + " Exception: " + e.Message, e);
            }
        }
        
        internal void GetVaultingData(PackageId packageId)
        {
            string path = template.UrlFor(UrlTemplate.E_ORIGINAL_VAULTING_DATA_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            try
            {
                restClient.Get(path);
                restClient.GetBytes(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to get VaultingData." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to get VaultingData." + " Exception: " + e.Message, e);
            }
        }
        internal void Revault(PackageId packageId)
        {
            string path = template.UrlFor(UrlTemplate.E_ORIGINAL_REVAULT_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            try
            {
                restClient.Post(path, null);
                restClient.GetBytes(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to revault package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to revault package." + " Exception: " + e.Message, e);
            }
        }
    }
}

