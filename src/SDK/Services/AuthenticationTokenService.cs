using System;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.API;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AuthenticationTokenService
    {
		private RestClient restClient;
        private string baseUrl;

		public AuthenticationTokenService (RestClient restClient, string baseUrl)
		{
			this.restClient = restClient;
            this.baseUrl = baseUrl;
		}

        [Obsolete("call CreateUserAuthenticationToken instead")]
        public OneSpanSign.Sdk.AuthenticationToken CreateAuthenticationToken ()
        {
            string userAuthenticationToken = CreateUserAuthenticationToken();
            OneSpanSign.Sdk.AuthenticationToken authenticationToken = new OneSpanSign.Sdk.AuthenticationToken(userAuthenticationToken);
            return authenticationToken;
        }

        public string CreateUserAuthenticationToken ()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.USER_AUTHENTICATION_TOKEN_PATH).Build ();

            try {
                string response = restClient.Post(path, "");              
                return JsonConvert.DeserializeObject<OneSpanSign.API.AuthenticationToken> (response).Value;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Could not create an authentication token for a user." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Could not create an authentication token for a user." + " Exception: " + e.Message, e);
            }
        }

        public string CreateSenderAuthenticationToken (PackageId packageId)
        {
            try {
                string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.SENDER_AUTHENTICATION_TOKEN_PATH).Build ();
                SenderAuthenticationToken senderAuthenticationToken = new SenderAuthenticationToken();
                senderAuthenticationToken.PackageId = packageId.Id;
                string serializedObject = JsonConvert.SerializeObject(senderAuthenticationToken);
                string response = restClient.Post(path, serializedObject);              
                return JsonConvert.DeserializeObject<SenderAuthenticationToken> (response).Value;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Could not create an authentication token for a sender." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Could not create an authentication token for a sender." + " Exception: " + e.Message, e);
            }
        }

        public string CreateSignerAuthenticationToken (PackageId packageId, string signerId)
        {
            return CreateSignerAuthenticationToken(packageId, signerId, null);
        }

        public string CreateSignerAuthenticationToken (PackageId packageId, string signerId, IDictionary<string, string> fields)
        {
            try {
                string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.SIGNER_AUTHENTICATION_TOKEN_MULTI_USE_PATH).Build ();
                SignerAuthenticationToken signerAuthenticationToken = new SignerAuthenticationToken();
                signerAuthenticationToken.PackageId = packageId.Id;
                signerAuthenticationToken.SignerId = signerId;
                signerAuthenticationToken.SessionFields = fields;

                string serializedObject = JsonConvert.SerializeObject(signerAuthenticationToken);
                string response = restClient.Post(path, serializedObject);              
                return JsonConvert.DeserializeObject<SignerAuthenticationToken> (response).Value;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Could not create a signer authentication token." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Could not create a signer authentication token." + " Exception: " + e.Message, e);
            }
        }

        public string CreateSignerAuthenticationTokenForSingleUse (PackageId packageId, string signerId, IDictionary<string, string> fields)
        {
            try {
                string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.SIGNER_AUTHENTICATION_TOKEN_SINGLE_USE_PATH).Build ();
                SignerAuthenticationToken signerAuthenticationToken = new SignerAuthenticationToken();
                signerAuthenticationToken.PackageId = packageId.Id;
                signerAuthenticationToken.SignerId = signerId;
                signerAuthenticationToken.SessionFields = fields;

                string serializedObject = JsonConvert.SerializeObject(signerAuthenticationToken);
                string response = restClient.Post(path, serializedObject);              
                return JsonConvert.DeserializeObject<SignerAuthenticationToken> (response).Value;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Could not create a signer authentication token for for single use." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Could not create a signer authentication token for single use." + " Exception: " + e.Message, e);
            }
        }
    }
}