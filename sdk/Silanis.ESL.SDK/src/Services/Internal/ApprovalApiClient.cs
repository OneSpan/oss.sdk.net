using System;
using Newtonsoft.Json;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    internal class ApprovalApiClient
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;

        public ApprovalApiClient(RestClient restClient, string baseUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);
            this.jsonSettings = jsonSettings;
        }        
        
        public void DeleteApproval(string packageId, string documentId, string approvalId)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_ID_PATH)
                .Replace("{packageId}", packageId)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", approvalId)
                    .Build();
            try {
                restClient.Delete(path);
            }
            catch (EslServerException e) {
                throw new EslServerException("Could not delete signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not delete signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public string AddApproval(PackageId packageId, string documentId, Approval approval)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Build();

            try {
                string json = JsonConvert.SerializeObject (approval, jsonSettings);
                string response = restClient.Post(path, json);
                Silanis.ESL.API.Approval apiApproval = JsonConvert.DeserializeObject<Silanis.ESL.API.Approval> (response, jsonSettings);
                return apiApproval.Id;
            }
            catch (EslServerException e) {
                throw new EslServerException("Could not add signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not add signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public void ModifyApproval(PackageId packageId, string documentId, string approvalId, Approval approval)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", approvalId)
                    .Build();

            try {
                string json = JsonConvert.SerializeObject (approval, jsonSettings);
                restClient.Put(path, json);
            }
            catch (EslServerException e) {
                throw new EslServerException("Could not add signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not add signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public Approval GetApproval(PackageId packageId, string documentId, string approvalId)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", approvalId)
                    .Build();

            try {
                string response = restClient.Get(path);
                Silanis.ESL.API.Approval apiApproval = JsonConvert.DeserializeObject<Silanis.ESL.API.Approval> (response, jsonSettings);
                return apiApproval;
            }
            catch (EslServerException e) {
                throw new EslServerException("Could not add signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not add signature.\t" + " Exception: " + e.Message, e);
            }
        }

    }
}

