using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    internal class AccountServiceApiClient
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;
        
        public AccountServiceApiClient(RestClient restClient, string apiUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (apiUrl);            
            this.jsonSettings = jsonSettings;
        }
        
        public void InviteUser( Silanis.ESL.API.Sender invitee ) {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_PATH).Build ();
            try {
                string json = JsonConvert.SerializeObject (invitee, jsonSettings);
                restClient.Post(path, json);              
            }
            catch (EslServerException e) {
                throw new EslServerException ("Failed to invite new account member.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Failed to invite new account member.\t" + " Exception: " + e.Message, e);
            }
        }        

        public void UpdateSender(Silanis.ESL.API.Sender apiSender, string senderId){
            string path = template.UrlFor(UrlTemplate.SENDER_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try {
                string json = JsonConvert.SerializeObject (apiSender, jsonSettings);
                restClient.Post(path, json);
            }
            catch (Exception e) {
                throw new EslException("Could not update sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void DeleteSender(string senderId){
            string path = template.UrlFor(UrlTemplate.SENDER_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try {
                restClient.Delete(path);
            }
            catch (Exception e) {
                throw new EslException("Could not delete sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public Silanis.ESL.API.Result<Silanis.ESL.API.Sender> GetSenders() {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_PATH)
                .Build();
            try {
                string response = restClient.Get(path);
                Silanis.ESL.API.Result<Silanis.ESL.API.Sender> apiResponse = 
                    JsonConvert.DeserializeObject<Silanis.ESL.API.Result<Silanis.ESL.API.Sender>> (response, jsonSettings );
               
                return apiResponse;
            }
            catch (Exception e) {
                throw new EslException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e);
            }
        }
    }
}

