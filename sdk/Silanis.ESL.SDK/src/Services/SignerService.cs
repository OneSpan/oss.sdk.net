using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class SignerService
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings settings;
    
        public SignerService(RestClient restClient, string baseUrl, JsonSerializerSettings settings )
        {
            this.restClient = restClient;
            this.settings = settings;
            this.template = new UrlTemplate (baseUrl);
        }
        
        public string AddSigner( PackageId packageId, Signer signer )
        {
            Support.LogMethodEntry(packageId,signer);
            
            Role apiPayload = new SignerConverter( signer ).ToAPIRole( System.Guid.NewGuid().ToString());
            
            string path = template.UrlFor (UrlTemplate.ADD_SIGNER_PATH)
                    .Replace( "{packageId}", packageId.Id )
                    .Build ();
            try {
                string json = JsonConvert.SerializeObject (apiPayload, settings);
                string response = restClient.Post(path, json);              
                Role apiRole = JsonConvert.DeserializeObject<Role> (response);
                Support.LogMethodExit(apiRole.Id);
                return apiRole.Id;
            } catch (Exception e) {
                throw new EslException ("Could not add signer." + " Exception: " + e.Message);
            }
        }

        public Signer GetSigner(PackageId packageId, string signerId)
        {
            Support.LogMethodEntry(packageId,signerId);
            string path = template.UrlFor (UrlTemplate.GET_SIGNER_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{roleId}", signerId)
                .Build ();

            try {
                string response = restClient.Get(path);
                Silanis.ESL.API.Role apiRole = JsonConvert.DeserializeObject<Silanis.ESL.API.Role> (response, settings);
                return SignerBuilder.NewSignerFromAPIRole(apiRole).Build();
            } catch (Exception e) {
                throw new EslException ("Could not retrieve signer." + " Exception: " + e.Message);
            }
        }
                
        public void UpdateSigner( PackageId packageId, Signer signer )
        {
            Support.LogMethodEntry(packageId,signer);
            
            Role apiPayload = new SignerConverter( signer ).ToAPIRole( System.Guid.NewGuid().ToString());
            
            string path = template.UrlFor (UrlTemplate.UPDATE_SIGNER_PATH)
                    .Replace( "{packageId}", packageId.Id )
                    .Replace( "{roleId}", signer.RoleId )
                    .Build ();
            try {
                string json = JsonConvert.SerializeObject (apiPayload, settings);
                string response = restClient.Put(path, json);              
                Support.LogMethodExit();
            } catch (Exception e) {
                throw new EslException ("Could not update signer." + " Exception: " + e.Message);
            }
        }
        
        public void RemoveSigner( PackageId packageId, string signerId )
        {
            Support.LogMethodEntry(packageId,signerId);
            
            string path = template.UrlFor (UrlTemplate.REMOVE_SIGNER_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{roleId}", signerId)
                .Build ();

            try {
                restClient.Delete(path);
                return;
            } catch (Exception e) {
                throw new EslException ("Could not delete signer." + " Exception: " + e.Message);
            }
        }
    }
}

