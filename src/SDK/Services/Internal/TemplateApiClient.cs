using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class TemplateApiClient
    {
        private JsonSerializerSettings settings;
        private RestClient restClient;
        private string baseUrl;
        
        internal TemplateApiClient(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.settings = settings;
            this.baseUrl = baseUrl;
        }
        
        internal string CreateTemplateFromPackage(string originalPackageId, OneSpanSign.API.Package delta)
        {
            delta.Type = "TEMPLATE";
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.CLONE_PACKAGE_PATH).Replace("{packageId}", originalPackageId)
                .Build ();
            try {
                string deltaJson = JsonConvert.SerializeObject (delta, settings);
                string response = restClient.Post(path, deltaJson);              
                OneSpanSign.API.Package apiResult = JsonConvert.DeserializeObject<OneSpanSign.API.Package> (response);
                return apiResult.Id;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Could not create a template." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new OssException ("Could not create a template." + " Exception: " + e.Message, e);
            }
        }
        
        internal string CreatePackageFromTemplate(string templateId, OneSpanSign.API.Package delta)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.CLONE_PACKAGE_PATH).Replace("{packageId}", templateId)
                .Build ();
            try {
                string deltaJson = JsonConvert.SerializeObject (delta, settings);
                string response = restClient.Post(path, deltaJson);              
                OneSpanSign.API.Package apiResult = JsonConvert.DeserializeObject<OneSpanSign.API.Package> (response);
                return apiResult.Id;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Could not create a package from template." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Could not create a package from template." + " Exception: " + e.Message, e);
            }
        }
        
        internal string CreateTemplate(OneSpanSign.API.Package template)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.PACKAGE_PATH).Build();

            try
            {
                string json = JsonConvert.SerializeObject(template, settings);
                string response = restClient.Post(path, json);
                OneSpanSign.API.Package apiPackage = JsonConvert.DeserializeObject<OneSpanSign.API.Package>(response);
                return apiPackage.Id;
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not create template." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not create template." + " Exception: " + e.Message, e);
            }
        }

        internal Placeholder AddPlaceholder(PackageId templateId, Placeholder placeholder)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ROLE_PATH)
                .Replace("{packageId}", templateId.Id)
                    .Build();
            Role apiPayload = new Role();
            apiPayload.Id = placeholder.Id;
            apiPayload.Name = placeholder.Name;

            try
            {
                string json = JsonConvert.SerializeObject(apiPayload, settings);
                string response = restClient.Post(path, json);
                OneSpanSign.API.Role apiRole = JsonConvert.DeserializeObject<OneSpanSign.API.Role>(response);
                return new Placeholder(apiRole.Id);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not add placeholder." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not add placeholder." + " Exception: " + e.Message, e);
            }
        }
        
        internal Placeholder UpdatePlaceholder(PackageId templateId, Placeholder placeholder)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.ROLE_ID_PATH)
                .Replace("{packageId}", templateId.Id)
                .Replace("{roleId}", placeholder.Id)
                .Build();
            Role apiPayload = new Role();
            apiPayload.Id = placeholder.Id;
            apiPayload.Name = placeholder.Name;

            try {
                string json = JsonConvert.SerializeObject(apiPayload, settings);
                string response = restClient.Put(path, json);
                OneSpanSign.API.Role apiRole = JsonConvert.DeserializeObject<OneSpanSign.API.Role>(response);
                return new Placeholder(apiRole.Id, apiRole.Name);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not update the placeholder." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not update the placeholder." + " Exception: " + e.Message, e);
            }
        }
        
        public void Update(OneSpanSign.API.Package apiTemplate)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.PACKAGE_ID_PATH)
                .Replace("{packageId}", apiTemplate.Id)
                .Build();

            try
            {
                string json = JsonConvert.SerializeObject(apiTemplate, settings);
                restClient.Post(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not update template." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not update template." + " Exception: " + e.Message, e);
            }
        }
    }
}

