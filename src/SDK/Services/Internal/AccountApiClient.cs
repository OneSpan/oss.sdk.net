using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class AccountApiClient
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;
        
        public AccountApiClient(RestClient restClient, string apiUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (apiUrl);            
            this.jsonSettings = jsonSettings;
        }
        
        public OneSpanSign.API.Sender InviteUser( OneSpanSign.API.Sender invitee ) {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_PATH).Build ();
            try {
                string json = JsonConvert.SerializeObject (invitee, jsonSettings);
                string response = restClient.Post(path, json);
                OneSpanSign.API.Sender apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Sender> (response, jsonSettings );
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to invite new account member.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to invite new account member.\t" + " Exception: " + e.Message, e);
            }
        }

        public void SendInvite( string senderId ) {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_INVITE_PATH)
                .Replace("{senderUid}", senderId)
                .Build ();
            try {
                restClient.Post(path, null);
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to send invite to sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to send invite to sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void UpdateSender(OneSpanSign.API.Sender apiSender, string senderId){
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_ID_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try {
                string json = JsonConvert.SerializeObject (apiSender, jsonSettings);
                apiSender.Id = senderId;
                restClient.Post(path, json);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not update sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not update sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void DeleteSender(string senderId){
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_ID_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try {
                restClient.Delete(path);
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not delete sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not delete sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Result<OneSpanSign.API.Sender> GetSenders(Direction direction, PageRequest request) {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_LIST_PATH)
                .Replace("{dir}", DirectionUtility.getDirection(direction))
                .Replace("{from}", request.From.ToString())
                .Replace("{to}", request.To.ToString())
                .Build();
            try {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.Sender> apiResponse = 
                    JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.Sender>> (response, jsonSettings );
               
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Sender GetSender(string senderId) {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_ID_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try {
                string response = restClient.Get(path);
                OneSpanSign.API.Sender apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Sender> (response, jsonSettings );

                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException("Failed to retrieve Sender from Account.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to retrieve Sender from Account.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.API.DelegationUser> GetDelegates(string senderId) {
            string path = template.UrlFor(UrlTemplate.DELEGATES_PATH)
                .Replace("{senderId}", senderId)
                .Build();

            try {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.DelegationUser>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e) {
                    throw new OssServerException("Failed to retrieve delegate users from Sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                    throw new OssException("Failed to retrieve delegate users from Sender.\t" + " Exception: " + e.Message, e);
            }

        }

        public void UpdateDelegates<T>(string senderId, IList<T> delegateIds) {
            string path = template.UrlFor(UrlTemplate.DELEGATES_PATH)
                .Replace("{senderId}", senderId)
                .Build();

            try {
                string json = JsonConvert.SerializeObject(delegateIds, jsonSettings);
                restClient.Put(path, json);
            }

            catch (OssServerException e) {
                throw new OssServerException("Failed to update delegates of the Sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to update delegates of the Sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void AddDelegate(string senderId, OneSpanSign.API.DelegationUser delegationUser) {
            string path = template.UrlFor(UrlTemplate.DELEGATE_ID_PATH)
                .Replace("{senderId}", senderId)
                .Replace("{delegateId}", delegationUser.Id)
                .Build();
            try {
                string json = JsonConvert.SerializeObject(delegationUser, jsonSettings);
                restClient.Post(path, json);
            }
            catch (OssServerException e) {
                throw new OssServerException("Failed to add delegate into the sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to add delegate into the sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void RemoveDelegate(string senderId, string delegateId) {
            string path = template.UrlFor(UrlTemplate.DELEGATE_ID_PATH)
                .Replace("{senderId}", senderId)
                .Replace("{delegateId}", delegateId)
                .Build();
            try {
                restClient.Delete(path);
            }
            catch (OssServerException e) {
                throw new OssServerException("Failed to remove delegate from the sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to remove delegate from the sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void ClearDelegates(string senderId) {
            string path = template.UrlFor(UrlTemplate.DELEGATES_PATH)
                .Replace("{senderId}", senderId)
                .Build();
            try {
                restClient.Delete(path);
            } 
            catch (OssServerException e) {
                throw new OssServerException("Failed to clear all delegates from the sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to clear all delegates from the sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.API.Sender> GetContacts() {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CONTACTS_PATH)
                .Build();
            try {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.Sender>> (response, jsonSettings);
            }
            catch (OssServerException e) {
                throw new OssServerException("Failed to retrieve contacts.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Failed to retrieve contacts.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<VerificationType> getVerificationTypes() {
            String path = template.UrlFor(UrlTemplate.ACCOUNT_VERIFICATION_TYPE_PATH)
                .Replace("{accountId}", "dummyAccountId")
                .Build();
            
            try {
                string response = restClient.Get(path);
                Result<VerificationType> result = JsonConvert.DeserializeObject<Result<VerificationType>> (response, jsonSettings);
                return result.Results;
            } catch (OssServerException e) {
                throw new OssServerException("Could not get verification types.\t" + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new OssException("Could not get verification types.\t" + " Exception: " + e.Message, e);
            }
        }
    }
}

