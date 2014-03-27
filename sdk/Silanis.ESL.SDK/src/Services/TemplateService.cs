using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class TemplateService
    {
        private UrlTemplate template;
        private JsonSerializerSettings settings;
        private RestClient restClient;
        
        public TemplateService(RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);

            settings = new JsonSerializerSettings ();
            settings.NullValueHandling = NullValueHandling.Ignore;
        }
        
        internal PackageId CreateTemplateFromPackage(PackageId originalPackageId, Package delta)
        {
            Support.LogMethodEntry(originalPackageId, delta);
            delta.Type = BasePackageType.TEMPLATE;
            string path = template.UrlFor (UrlTemplate.CLONE_PACKAGE_PATH).Replace("{packageId}", originalPackageId.Id)
                .Build ();
            try {
                string deltaJson = JsonConvert.SerializeObject (delta, settings);
                string response = restClient.Post(path, deltaJson);              
                PackageId result = JsonConvert.DeserializeObject<PackageId> (response);
                Support.LogMethodExit(result);
                return result;
            } catch (Exception e) {
                throw new EslException ("Could not create a new package." + " Exception: " + e.Message);
            }
        }
    }
}

