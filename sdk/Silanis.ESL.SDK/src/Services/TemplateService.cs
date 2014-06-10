using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Services
{
    public class TemplateService
    {
		private UrlTemplate urls;
        private JsonSerializerSettings settings;
        private RestClient restClient;
		private PackageService packageService;
        
		public TemplateService(RestClient restClient, string baseUrl, PackageService packageService, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
			urls = new UrlTemplate (baseUrl);
			this.packageService = packageService;
            this.settings = settings;
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
                Package apiResult = JsonConvert.DeserializeObject<Package> (response);
                PackageId sdkResult = new PackageId(apiResult.Id);
                Support.LogMethodExit(sdkResult);
                return sdkResult;
            } 
            catch (EslServerException e) {
                throw new EslServerException ("Could not create a template." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new EslException ("Could not create a template." + " Exception: " + e.Message, e);
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
                Package apiResult = JsonConvert.DeserializeObject<Package> (response);
                PackageId sdkResult = new PackageId(apiResult.Id);
                Support.LogMethodExit(sdkResult);
                return sdkResult;
            } 
            catch (EslServerException e) {
                throw new EslServerException ("Could not create a package from template." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Could not create a package from template." + " Exception: " + e.Message, e);
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
            catch (EslServerException e)
            {
                throw new EslServerException ("Could not create template." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
			{
				throw new EslException ("Could not create template." + " Exception: " + e.Message, e);
			}
		}

		public void Update(DocumentPackage template)
		{
			if (template.Id == null)
			{
				throw new ArgumentNullException("template.Id");
			}

			string path = urls.UrlFor(UrlTemplate.PACKAGE_ID_PATH)
				.Replace("{packageId}", template.Id.Id)
				.Build();
			Silanis.ESL.API.Package internalTemplate = template.ToAPIPackage();

			internalTemplate.Type = BasePackageType.TEMPLATE;

			try
			{
				string json = JsonConvert.SerializeObject(internalTemplate, settings);

				restClient.Post(path, json);
			}
            catch (EslServerException e)
            {
                throw new EslServerException ("Could not update template." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
			{
				throw new EslException ("Could not update template." + " Exception: " + e.Message, e);
			}
		}

		/// <summary>
		/// Deletes the document from the template.
		/// </summary>
		/// <param name="templateId">The template id.</param>
		/// <param name="document">The document to delete.</param>
		public void DeleteDocument (PackageId templateId, Document document)
		{
			DeleteDocument(templateId, document.Id);
		}

		public void DeleteDocument (PackageId templateId, string documentId)
		{
			packageService.DeleteDocument(templateId, documentId);
		}

		public void UpdateDocumentMetadata(DocumentPackage template, Document document)
		{
			packageService.UpdateDocumentMetadata(template, document);
		}
    }
}