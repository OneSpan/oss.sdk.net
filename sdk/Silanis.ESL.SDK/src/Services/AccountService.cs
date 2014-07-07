using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Services
{
    public class AccountService
    {
        private AccountApiClient apiClient;

        internal AccountService(AccountApiClient apiClient)
        {
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
            apiSender.Id = senderId;
            apiClient.UpdateSender(apiSender, senderId);
        }
    }
}

