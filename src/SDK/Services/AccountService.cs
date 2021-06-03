using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;
using System.Collections.Generic;
using System.Collections;

namespace OneSpanSign.Sdk.Services
{
    public class AccountService
    {
        private AccountApiClient apiClient;

        internal AccountService(AccountApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Sender InviteUser(AccountMember invitee)
        {
            OneSpanSign.API.Sender apiSender = new AccountMemberConverter(invitee).ToAPISender();
            OneSpanSign.API.Sender apiResponse = apiClient.InviteUser(apiSender);
            Sender result = new SenderConverter(apiResponse).ToSDKSender();
            return result;
        }

        public void SendInvite(string senderId)
        {
            apiClient.SendInvite(senderId);
        }

        public IDictionary<string, OneSpanSign.Sdk.Sender> GetSenders(Direction direction, PageRequest request)
        {
            OneSpanSign.API.Result<OneSpanSign.API.Sender> apiResponse = apiClient.GetSenders(direction, request);

            IDictionary<string, OneSpanSign.Sdk.Sender> result = new Dictionary<string, OneSpanSign.Sdk.Sender>();
            foreach (OneSpanSign.API.Sender apiSender in apiResponse.Results)
            {
                result.Add(apiSender.Email, new SenderConverter(apiSender).ToSDKSender());
            }

            return result;
        }

        public OneSpanSign.Sdk.Sender GetSender(string senderId)
        {
            OneSpanSign.API.Sender apiResponse = apiClient.GetSender(senderId);
            Sender result = new SenderConverter(apiResponse).ToSDKSender();
            return result;
        }

        public void DeleteSender(string senderId)
        {
            apiClient.DeleteSender(senderId);
        }

        public void UpdateSender(SenderInfo senderInfo, string senderId)
        {
            OneSpanSign.API.Sender apiSender = new SenderConverter(senderInfo).ToAPISender();
            apiSender.Id = senderId;
            apiClient.UpdateSender(apiSender, senderId);
        }

        public OneSpanSign.Sdk.SenderImageSignature GetSenderImageSignature(string senderId)
        {
            OneSpanSign.API.SenderImageSignature apiResponse = apiClient.GetSenderImageSignature(senderId);
            SenderImageSignature result = new SenderImageSignatureConverter(apiResponse).ToSDKSenderImageSignature();
            return result;
        }

        public void UpdateSenderImageSignature(string fileName, byte[] fileContent, string senderId)
        {
            apiClient.UpdateSenderImageSignature(fileName, fileContent, senderId);
        }

        public void DeleteSenderImageSignature(string senderId)
        {
            apiClient.DeleteSenderImageSignature(senderId);
        }

        public IDictionary<string, OneSpanSign.Sdk.Sender> GetContacts()
        {
            IList<OneSpanSign.API.Sender> contacts = apiClient.GetContacts();

            IDictionary<string, OneSpanSign.Sdk.Sender> result = new Dictionary<string, OneSpanSign.Sdk.Sender>();
            foreach (OneSpanSign.API.Sender apiSender in contacts)
            {
                result[apiSender.Email] = new SenderConverter(apiSender).ToSDKSender();
            }

            return result;
        }

        public IList<OneSpanSign.Sdk.DelegationUser> GetDelegates(string senderId)
        {
            IList<OneSpanSign.Sdk.DelegationUser> result = new List<OneSpanSign.Sdk.DelegationUser>();
            IList<OneSpanSign.API.DelegationUser> apiDelegationUsers = apiClient.GetDelegates(senderId);
            foreach (OneSpanSign.API.DelegationUser delegationUser in apiDelegationUsers)
            {
                result.Add(new DelegationUserConverter(delegationUser).ToSDKDelegationUser());
            }
            return result;
        }

        public void UpdateDelegates(string senderId, List<string> delegateIds)
        {
            apiClient.UpdateDelegates(senderId, delegateIds);
        }

        public void updateDelegationWithDelegationUsers(string senderId, List<DelegationUser> delegates)
        {
            IList<OneSpanSign.API.DelegationUser> apiDelegates = new List<OneSpanSign.API.DelegationUser>();
            foreach (OneSpanSign.Sdk.DelegationUser delegateUser in delegates)
            {
                apiDelegates.Add(new DelegationUserConverter(delegateUser).ToAPIDelegationUser());
            }
            apiClient.UpdateDelegates(senderId, apiDelegates);
        }

        public void AddDelegate(string senderId, OneSpanSign.Sdk.DelegationUser delegationUser)
        {
            OneSpanSign.API.DelegationUser apiDelegationUser = new DelegationUserConverter(delegationUser).ToAPIDelegationUser();
            apiClient.AddDelegate(senderId, apiDelegationUser);
        }

        public void RemoveDelegate(string senderId, string delegateId)
        {
            apiClient.RemoveDelegate(senderId, delegateId);
        }

        public void ClearDelegates(string senderId)
        {
            apiClient.ClearDelegates(senderId);
        }

        public IList<VerificationType> getVerificationTypes()
        {
            return apiClient.getVerificationTypes();
        }

        public IList<Account> getSubAccounts()
        {
            IList<API.Account> apiAccounts = apiClient.getSubAccounts();
            IList<Account> accounts = new List<Account>();
            foreach (OneSpanSign.API.Account account in apiAccounts)
            {
                accounts.Add(new AccountConverter(account).ToSDKAccount());
            }
            return accounts;
        }

        public IList<AccessibleAccountResponse> getAccessibleAccounts()
        {
            IList<API.AccessibleAccountResponse> apiAccessibleAccounts = apiClient.getAccessibleAccounts();
            IList<AccessibleAccountResponse> accountResponses = new List<AccessibleAccountResponse>();
            foreach (OneSpanSign.API.AccessibleAccountResponse accountResponse in apiAccessibleAccounts)
            {
                accountResponses.Add(new AccessibleAccountResponseConverter(accountResponse).ToSDKAccessibleAccountResponse());
            }

            return accountResponses;
        }

        public Account createSubAccount(SubAccount subAccount)
        {
            OneSpanSign.API.SubAccount apiSubAccount = new SubAccountConverter(subAccount).ToAPISubAccount();
            OneSpanSign.API.Account account = apiClient.createSubAccount(apiSubAccount);
            return new AccountConverter(account).ToSDKAccount();
        }

        public void updateSubAccount(SubAccount subAccount, String accountId)
        {
            OneSpanSign.API.SubAccount apiSubAccount = new SubAccountConverter(subAccount).ToAPISubAccount();
            apiClient.updateSubAccount(apiSubAccount, accountId);
        }
    }
}

