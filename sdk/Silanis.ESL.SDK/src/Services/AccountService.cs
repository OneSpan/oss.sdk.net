using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Services
{
    public class AccountService
    {
//        private UrlTemplate template;
//        private JsonSerializerSettings settings;
//        private RestClient restClient;
        private AccountServiceApiClient apiClient;

        internal AccountService(RestClient restClient, string baseUrl, JsonSerializerSettings settings, AccountServiceApiClient apiClient)
        {
//            this.restClient = restClient;
//            template = new UrlTemplate (baseUrl);
//            this.settings = settings;
            this.apiClient = apiClient;
        }

        public void InviteUser(AccountMember invitee)
        {
            Silanis.ESL.API.Sender apiSender = new AccountMemberConverter( invitee ).ToAPISender();
            apiClient.InviteUser( apiSender );
        }

        public IDictionary<string, Silanis.ESL.SDK.Sender> GetSenders()
        {
            Silanis.ESL.API.Result<Silanis.ESL.API.Sender> apiResponse = apiClient.GetSenders();
            
            IDictionary<string, Silanis.ESL.SDK.Sender> result = new Dictionary<string, Silanis.ESL.SDK.Sender>();
            foreach ( Silanis.ESL.API.Sender apiSender in apiResponse.Results ) {
                result.Add(apiSender.Email, new SenderConverter( apiSender ).ToSDKSender() );
            }
            
            return result;
        }

        public void DeleteSender(string senderId)
        {
            apiClient.DeleteSender( senderId );
        }

        public void UpdateSender(SenderInfo senderInfo, string senderId)
        {
            Silanis.ESL.API.Sender apiSender = new SenderConverter(senderInfo).ToAPISender();
            apiClient.UpdateSender(apiSender, senderId);
//=======
//        public IDictionary<string, Silanis.ESL.SDK.Sender> GetSenders(){
//            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_PATH)
//                .Build();
//            try {
//                string response = restClient.Get(path);
//                Silanis.ESL.API.Result<Silanis.ESL.API.Sender> apiResponse = 
//                    JsonConvert.DeserializeObject<Silanis.ESL.API.Result<Silanis.ESL.API.Sender>> (response, settings );
//               
//                IDictionary<string, Silanis.ESL.SDK.Sender> result = new Dictionary<string, Silanis.ESL.SDK.Sender>();
//                foreach ( Silanis.ESL.API.Sender apiSender in apiResponse.Results ) {
//                    result.Add(apiSender.Email, new SenderConverter( apiSender ).ToSDKSender() );
//                }
//                return result;
//            }
//            catch (EslServerException e) {
//                throw new EslServerException ("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e.ServerError, e);
//            }
//            catch (Exception e) {
//                throw new EslException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e);
//            }
//        }
//
//        public void DeleteSender(string senderId){
//            string path = template.UrlFor(UrlTemplate.SENDER_PATH)
//                .Replace("{senderUid}", senderId)
//                .Build();
//            try {
//                restClient.Delete(path);
//            }
//            catch (EslServerException e) {
//                throw new EslServerException("Could not delete sender.\t" + " Exception: " + e.Message, e.ServerError, e);
//            }
//            catch (Exception e) {
//                throw new EslException("Could not delete sender.\t" + " Exception: " + e.Message, e);
//            }
//        }
//
//        public void UpdateSender(SenderInfo senderInfo, string senderId){
//            string path = template.UrlFor(UrlTemplate.SENDER_PATH)
//                .Replace("{senderUid}", senderId)
//                .Build();
//            try {
//                Silanis.ESL.API.Sender apiPayload = new SenderConverter(senderInfo).ToAPISender();
//                string json = JsonConvert.SerializeObject (apiPayload, settings);
//                restClient.Post(path, json);
//            }
//            catch (EslServerException e) {
//                throw new EslServerException("Could not update sender.\t" + " Exception: " + e.Message, e.ServerError, e);
//            }
//            catch (Exception e) {
//                throw new EslException("Could not update sender.\t" + " Exception: " + e.Message, e);
//            }
//>>>>>>> dev
        }
    }
}

