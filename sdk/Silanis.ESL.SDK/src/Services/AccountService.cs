using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Services
{
    public class AccountService
    {
        private UrlTemplate template;
        private JsonSerializerSettings settings;
        private RestClient restClient;

        public AccountService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);
            this.settings = settings;
        }

        public void InviteUser( AccountMember accountMember ) {
            string path = template.UrlFor (UrlTemplate.ACCOUNT_MEMBER_PATH)
                .Build ();
            try {
                Silanis.ESL.API.Sender sender = new AccountMemberConverter( accountMember ).ToAPISender();
                string json = JsonConvert.SerializeObject (sender, settings);
                restClient.Post(path, json);              
            }
            catch (EslServerException e) {
                throw new EslServerException ("Failed to invite new account member.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Failed to invite new account member.\t" + " Exception: " + e.Message, e);
            }
        }

        public IDictionary<string, Silanis.ESL.SDK.Sender> GetSenders(){
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_PATH)
                .Build();
            try {
                string response = restClient.Get(path);
                Silanis.ESL.API.Result<Silanis.ESL.API.Sender> apiResponse = 
                    JsonConvert.DeserializeObject<Silanis.ESL.API.Result<Silanis.ESL.API.Sender>> (response, settings );
               
                IDictionary<string, Silanis.ESL.SDK.Sender> result = new Dictionary<string, Silanis.ESL.SDK.Sender>();
                foreach ( Silanis.ESL.API.Sender apiSender in apiResponse.Results ) {
                    result.Add(apiSender.Email, new SenderConverter( apiSender ).ToSDKSender() );
                }
                return result;
            }
            catch (Exception e) {
                throw new EslException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e);
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

        public void UpdateSender(SenderInfo senderInfo, string senderId){
            string path = template.UrlFor(UrlTemplate.SENDER_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try {
                Silanis.ESL.API.Sender apiPayload = new SenderConverter(senderInfo).ToAPISender();
                string json = JsonConvert.SerializeObject (apiPayload, settings);
                restClient.Post(path, json);
            }
            catch (Exception e) {
                throw new EslException("Could not update sender.\t" + " Exception: " + e.Message, e);
            }
        }
    }
}

