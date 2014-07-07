using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    internal class AttachmentRequirementApiClient
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;

        public AttachmentRequirementApiClient(RestClient restClient, string apiUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (apiUrl);            
            this.jsonSettings = jsonSettings;
        }
        
        public void AcceptAttachment(string packageId, Role role)
        {
            string path = template.UrlFor(UrlTemplate.UPDATE_SIGNER_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{roleId}", role.Id)
                .Build();

            try {
                string json = JsonConvert.SerializeObject(role, jsonSettings);
                restClient.Put(path, json);
            } 
            catch (EslServerException e) {
                throw new EslServerException("Could not accept attachment for signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not accept attachment for signer." + " Exception: " + e.Message,e);
            }
        }
        
        public void RejectAttachment(string packageId, Role role)
        {
            string path = template.UrlFor(UrlTemplate.UPDATE_SIGNER_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{roleId}", role.Id)
                .Build();
                
            try {
                string json = JsonConvert.SerializeObject(role, jsonSettings);
                restClient.Put(path, json);              
            } 
            catch (EslServerException e) {
                throw new EslServerException("Could not reject attachment for signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not reject attachment for signer." + " Exception: " + e.Message,e);
            }
        }
        
        public byte[] DownloadAttachments(string packageId, string attachmentId)
        {
            string path = template.UrlFor(UrlTemplate.ATTACHMENT_REQUIREMENT_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{attachmentId}", attachmentId)
                .Build();

            try {
                return restClient.GetBytes(path);
            }
            catch (EslServerException e) {
                throw new EslServerException("Could not download the pdf attachment." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not download the pdf attachment." + " Exception: " + e.Message,e);
            }
        }

    }
}

