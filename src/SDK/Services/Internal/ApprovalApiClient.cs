using System;
using Newtonsoft.Json;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.API;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
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
            catch (OssServerException e) {
                throw new OssServerException("Could not delete signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not delete signature.\t" + " Exception: " + e.Message, e);
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
                OneSpanSign.API.Approval apiApproval = JsonConvert.DeserializeObject<OneSpanSign.API.Approval> (response, jsonSettings);
                return apiApproval.Id;
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not add signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not add signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public void ModifyApproval(PackageId packageId, string documentId, Approval approval)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", approval.Id)
                    .Build();

            try {
                string json = JsonConvert.SerializeObject (approval, jsonSettings);
                restClient.Put(path, json);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not modify signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not modify signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public void UpdateApprovals(PackageId packageId, string documentId, IList<Approval> approvalList)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_PATH)
                .Replace("{packageId}", packageId.Id)
                .Replace("{documentId}", documentId)
                .Build();

            try {
                string json = JsonConvert.SerializeObject (approvalList, jsonSettings);
                restClient.Put(path, json);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not update signatures.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not update signatures.\t" + " Exception: " + e.Message, e);
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
                OneSpanSign.API.Approval apiApproval = JsonConvert.DeserializeObject<OneSpanSign.API.Approval> (response, jsonSettings);
                return apiApproval;
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not get signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not get signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public string AddField(PackageId packageId, string documentId, SignatureId signatureId, OneSpanSign.API.Field field)
        {
            string path = template.UrlFor(UrlTemplate.FIELD_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", signatureId.Id)
                    .Build();

            try {
                string json = JsonConvert.SerializeObject (field, jsonSettings);
                string response = restClient.Post(path, json);
                OneSpanSign.API.Field apiField = JsonConvert.DeserializeObject<OneSpanSign.API.Field> (response, jsonSettings);
                return apiField.Id;
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not add field to signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not add field to signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public void ModifyField(PackageId packageId, string documentId, SignatureId signatureId, OneSpanSign.API.Field field)
        {
            string path = template.UrlFor(UrlTemplate.FIELD_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", signatureId.Id)
                    .Replace("{fieldId}", field.Id)
                    .Build();

            try {
                string json = JsonConvert.SerializeObject (field, jsonSettings);
                restClient.Put(path, json);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not modify field from signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not modify field from signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public void ModifyConditionalField (PackageId packageId, string documentId, SignatureId signatureId, OneSpanSign.API.ConditionalField field)
        {
            string path = template.UrlFor (UrlTemplate.CONDITIONAL_FIELD_PATH)
                .Replace ("{packageId}", packageId.Id)
                    .Replace ("{documentId}", documentId)
                    .Replace ("{approvalId}", signatureId.Id)
                    .Replace ("{fieldId}", field.Id)
                    .Build ();

            try 
            {
                string json = JsonConvert.SerializeObject (field, jsonSettings);
                restClient.Put (path, json);
            } 
            catch (OssServerException e) 
            {
                throw new OssServerException ("Could not modify conditional field from signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            } 
            catch (Exception e) 
            {
                throw new OssException ("Could not modify conditional field from signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Field GetField(PackageId packageId, string documentId, SignatureId signatureId, string fieldId)
        {
            string path = template.UrlFor(UrlTemplate.FIELD_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", signatureId.Id)
                    .Replace("{fieldId}", fieldId)
                    .Build();

            try {
                string response = restClient.Get(path);
                OneSpanSign.API.Field apiField = JsonConvert.DeserializeObject<OneSpanSign.API.Field> (response, jsonSettings);
                return apiField;
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not get field from signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not get field from signature.\t" + " Exception: " + e.Message, e);
            }

        }

        public void DeleteField(PackageId packageId, string documentId, SignatureId signatureId, string fieldId)
        {
            string path = template.UrlFor(UrlTemplate.FIELD_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", signatureId.Id)
                    .Replace("{fieldId}", fieldId)
                    .Build();

            try {
                restClient.Delete(path);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not delete field from signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not delete field from signature.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<Approval> GetAllSignableApprovals(PackageId packageId, string documentId, string signerId)
        {
            string path = template.UrlFor(UrlTemplate.SIGNABLE_APPROVAL_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{documentId}", documentId)
                    .Replace("{signerId}", signerId)
                    .Build();
            IList<Approval> response = null;

            try {
                string stringResponse = restClient.Get(path);
                response = JsonConvert.DeserializeObject<IList<Approval>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not get all signable signatures.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not get all signable signatures.\t" + " Exception: " + e.Message, e);
            }

            return response;
        }
    }
}

