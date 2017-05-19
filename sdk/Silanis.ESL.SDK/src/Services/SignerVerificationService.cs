using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
    public class SignerVerificationService
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings settings;

        public SignerVerificationService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.template = new UrlTemplate( baseUrl );
            this.restClient = restClient;
            this.settings = settings;
        }

        internal void CreateSignerVerification( PackageId packageId, string roleId , Silanis.ESL.API.Verification verification) 
        {
            string path = template.UrlFor( UrlTemplate.ADD_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(verification, settings);
                restClient.Post( path, json );
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not create signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not create signer verification." + " Exception: " + e.Message, e);
            }
        }

        internal Silanis.ESL.API.Verification GetSignerVerification( PackageId packageId, string roleId ) 
        {
            string path = template.UrlFor( UrlTemplate.GET_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<Silanis.ESL.API.Verification>(response, settings);
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not get signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not get signer verification." + " Exception: " + e.Message, e);
            }
        }

        internal void UpdateSignerVerification( PackageId packageId, string roleId , Silanis.ESL.API.Verification verification) 
        {
            string path = template.UrlFor( UrlTemplate.UPDATE_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(verification, settings);
                restClient.Put( path, json );
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not update signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not add update verification." + " Exception: " + e.Message, e);
            }
        }

        internal void DeleteSignerVerification( PackageId packageId, string roleId ) 
        {
            string path = template.UrlFor( UrlTemplate.DELETE_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                restClient.Delete( path );
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not delete signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not add delete verification." + " Exception: " + e.Message, e);
            }
        }
    }
}

