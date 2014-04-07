using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class TemplateService
    {
		private UrlTemplate urls;
        private JsonSerializerSettings settings;
        private RestClient restClient;
        
        public TemplateService(RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
			urls = new UrlTemplate (baseUrl);

            settings = new JsonSerializerSettings ();
            settings.NullValueHandling = NullValueHandling.Ignore;
        }
        
        internal PackageId CreateTemplateFromPackage(PackageId originalPackageId, Package delta)
        {
            Support.LogMethodEntry(originalPackageId, delta);
            delta.Type = BasePackageType.TEMPLATE;
			string path = urls.UrlFor (UrlTemplate.CLONE_PACKAGE_PATH).Replace("{packageId}", originalPackageId.Id)
                .Build ();
            try {
                string deltaJson = JsonConvert.SerializeObject (delta, settings);
                string response = restClient.Post(path, deltaJson);              
                PackageId result = JsonConvert.DeserializeObject<PackageId> (response);
                Support.LogMethodExit(result);
                return result;
            } catch (Exception e) {
                throw new EslException ("Could not create a template." + " Exception: " + e.Message);
            }
        }
        
        internal PackageId CreatePackageFromTemplate(PackageId templateId, Package delta)
        {
            Support.LogMethodEntry(templateId, delta);
			string path = urls.UrlFor (UrlTemplate.CLONE_PACKAGE_PATH).Replace("{packageId}", templateId.Id)
                .Build ();
            try {
                string deltaJson = JsonConvert.SerializeObject (delta, settings);
                string response = restClient.Post(path, deltaJson);              
                PackageId result = JsonConvert.DeserializeObject<PackageId> (response);
                Support.LogMethodExit(result);
                return result;
            } catch (Exception e) {
                throw new EslException ("Could not create a package from template." + " Exception: " + e.Message);
            }
        }

		internal PackageId CreateTemplate(Package template)
		{
			template.Type = BasePackageType.TEMPLATE;
			string path = urls.UrlFor(UrlTemplate.PACKAGE_PATH).Build();

			try
			{
				string json = JsonConvert.SerializeObject(template, settings);
				string response = restClient.Post(path, json);

				return JsonConvert.DeserializeObject<PackageId>(response);
			}
			catch (Exception e)
			{
				throw new EslException ("Could not create template." + " Exception: " + e.Message);
			}
		}
    }
}

