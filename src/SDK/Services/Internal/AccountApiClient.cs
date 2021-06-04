using System;
using System.Text;
using System.IO;
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
            template = new UrlTemplate(apiUrl);
            this.jsonSettings = jsonSettings;
        }

        public OneSpanSign.API.Sender InviteUser(OneSpanSign.API.Sender invitee)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_PATH).Build();
            try
            {
                string json = JsonConvert.SerializeObject(invitee, jsonSettings);
                string response = restClient.Post(path, json);
                OneSpanSign.API.Sender apiResponse =
                    JsonConvert.DeserializeObject<OneSpanSign.API.Sender>(response, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Failed to invite new account member.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to invite new account member.\t" + " Exception: " + e.Message, e);
            }
        }

        public void SendInvite(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_INVITE_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try
            {
                restClient.Post(path, null);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Failed to send invite to sender.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to send invite to sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void UpdateSenderImageSignature(string fileName, byte[] fileContent, string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_SIGNATURE_IMAGE_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try
            {
                string boundary = GenerateBoundary();
                byte[] bytes = new byte[fileName.Length * sizeof(char)];
                System.Buffer.BlockCopy(fileName.ToCharArray(), 0, bytes, 0, bytes.Length);
                byte[] content = CreateMultipartContent(fileName, fileContent, boundary);
                restClient.PostMultipartFile(path, content, boundary, Converter.ToString(bytes));
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update sender signature image.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update sender signature image." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.SenderImageSignature GetSenderImageSignature(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_SIGNATURE_IMAGE_PATH)
                    .Replace("{senderUid}", senderId)
                    .Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<OneSpanSign.API.SenderImageSignature>(response,
                            jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get sender signature image.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get sender signature image." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteSenderImageSignature(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_SIGNATURE_IMAGE_PATH)
                    .Replace("{senderUid}", senderId)
                    .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete sender signature image.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete sender signature image." + " Exception: " + e.Message, e);
            }
        }

        public void UpdateSender(OneSpanSign.API.Sender apiSender, string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_ID_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(apiSender, jsonSettings);
                apiSender.Id = senderId;
                restClient.Post(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update sender.\t" + " Exception: " + e.Message, e.ServerError,
                    e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void DeleteSender(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_ID_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete sender.\t" + " Exception: " + e.Message, e.ServerError,
                    e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Result<OneSpanSign.API.Sender> GetSenders(Direction direction, PageRequest request)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_LIST_PATH)
                .Replace("{dir}", DirectionUtility.getDirection(direction))
                .Replace("{from}", request.From.ToString())
                .Replace("{to}", request.To.ToString())
                .Build();
            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.Sender> apiResponse =
                    JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.Sender>>(response,
                        jsonSettings);

                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to retrieve Account Members List.\t" + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Sender GetSender(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_MEMBER_ID_PATH)
                .Replace("{senderUid}", senderId)
                .Build();
            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Sender apiResponse =
                    JsonConvert.DeserializeObject<OneSpanSign.API.Sender>(response, jsonSettings);

                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Failed to retrieve Sender from Account.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to retrieve Sender from Account.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.API.DelegationUser> GetDelegates(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.DELEGATES_PATH)
                .Replace("{senderId}", senderId)
                .Build();

            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.DelegationUser>>(stringResponse,
                    jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Failed to retrieve delegate users from Sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to retrieve delegate users from Sender.\t" + " Exception: " + e.Message,
                    e);
            }
        }

        public void UpdateDelegates<T>(string senderId, IList<T> delegateIds)
        {
            string path = template.UrlFor(UrlTemplate.DELEGATES_PATH)
                .Replace("{senderId}", senderId)
                .Build();

            try
            {
                string json = JsonConvert.SerializeObject(delegateIds, jsonSettings);
                restClient.Put(path, json);
            }

            catch (OssServerException e)
            {
                throw new OssServerException("Failed to update delegates of the Sender.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to update delegates of the Sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void AddDelegate(string senderId, OneSpanSign.API.DelegationUser delegationUser)
        {
            string path = template.UrlFor(UrlTemplate.DELEGATE_ID_PATH)
                .Replace("{senderId}", senderId)
                .Replace("{delegateId}", delegationUser.Id)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(delegationUser, jsonSettings);
                restClient.Post(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Failed to add delegate into the sender.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to add delegate into the sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void RemoveDelegate(string senderId, string delegateId)
        {
            string path = template.UrlFor(UrlTemplate.DELEGATE_ID_PATH)
                .Replace("{senderId}", senderId)
                .Replace("{delegateId}", delegateId)
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Failed to remove delegate from the sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to remove delegate from the sender.\t" + " Exception: " + e.Message, e);
            }
        }

        public void ClearDelegates(string senderId)
        {
            string path = template.UrlFor(UrlTemplate.DELEGATES_PATH)
                .Replace("{senderId}", senderId)
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Failed to clear all delegates from the sender.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to clear all delegates from the sender.\t" + " Exception: " + e.Message,
                    e);
            }
        }

        public IList<OneSpanSign.API.Sender> GetContacts()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CONTACTS_PATH)
                .Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.Sender>>(response, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Failed to retrieve contacts.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Failed to retrieve contacts.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<VerificationType> getVerificationTypes()
        {
            String path = template.UrlFor(UrlTemplate.ACCOUNT_VERIFICATION_TYPE_PATH)
                .Replace("{accountId}", "dummyAccountId")
                .Build();

            try
            {
                string response = restClient.Get(path);
                Result<VerificationType> result =
                    JsonConvert.DeserializeObject<Result<VerificationType>>(response, jsonSettings);
                return result.Results;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get verification types.\t" + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get verification types.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.API.Account> getSubAccounts()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SUBACCOUNTS_PATH).Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.Account>>(response, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get subAccounts.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get subAccounts.\t" + " Exception: " + e.Message, e);
            }
        }
        
        public IList<OneSpanSign.API.SubAccountApiKey> getSubAccountApiKey()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SUBACCOUNTS_SUBACCOUNTAPIKEYS_PATH).Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.SubAccountApiKey>>(response, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get subAccountApiKey.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get subAccountApiKey.\t" + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.API.AccessibleAccountResponse> getAccessibleAccounts()
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SUBACCOUNTS_ACCESSIBLEACCOUNTS_PATH).Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.AccessibleAccountResponse>>(response, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get accessibleAccounts.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get accessibleAccounts.\t" + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Account createSubAccount(OneSpanSign.API.SubAccount subAccount)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SUBACCOUNTS_PATH).Build();
            try
            {
                string payload = JsonConvert.SerializeObject(subAccount, jsonSettings);
                string response = restClient.Post(path, payload);
                return JsonConvert.DeserializeObject<OneSpanSign.API.Account>(response, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create subAccount.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create subAccount.\t" + " Exception: " + e.Message, e);
            }
        }

        public void updateSubAccount(OneSpanSign.API.SubAccount subAccount, string accountId)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_SUBACCOUNTS_ID_PATH)
                    .Replace("{accountId}", accountId)
                    .Build();
            try
            {
                string payload = JsonConvert.SerializeObject(subAccount, jsonSettings);
                restClient.Put(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update subAccount.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update subAccount.\t" + " Exception: " + e.Message, e);
            }
        }


        private string GenerateBoundary()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        private byte[] CreateMultipartContent(string fileName, byte[] fileBytes, string boundary)
        {

            Encoding encoding = Encoding.UTF8;
            Stream formDataStream = new MemoryStream();
            string data = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                                         boundary, "file", fileName, MimeTypeUtil.GetMIMEType(fileName));
            formDataStream.Write(encoding.GetBytes(data), 0, encoding.GetByteCount(data));
            formDataStream.Write(fileBytes, 0, fileBytes.Length);

            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            //Dump the stream
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
    }
}