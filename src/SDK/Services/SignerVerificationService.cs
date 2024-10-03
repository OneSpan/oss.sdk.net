using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class SignerVerificationService
    {
        private RestClient restClient;
        private JsonSerializerSettings settings;
        private string baseUrl;

        public SignerVerificationService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.settings = settings;
            this.baseUrl = baseUrl;
        }

        internal void CreateSignerVerification( PackageId packageId, string roleId , OneSpanSign.API.Verification verification) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.ADD_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(verification, settings);
                restClient.Post( path, json );
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create signer verification." + " Exception: " + e.Message, e);
            }
        }

        internal OneSpanSign.API.Verification GetSignerVerification( PackageId packageId, string roleId ) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.GET_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<OneSpanSign.API.Verification>(response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get signer verification." + " Exception: " + e.Message, e);
            }
        }

        internal void UpdateSignerVerification( PackageId packageId, string roleId , OneSpanSign.API.Verification verification) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.UPDATE_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(verification, settings);
                restClient.Put( path, json );
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add update verification." + " Exception: " + e.Message, e);
            }
        }

        internal void DeleteSignerVerification( PackageId packageId, string roleId ) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.DELETE_SIGNER_VERIFICATION_PATH )
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", roleId)
                .Build();

            try 
            {
                restClient.Delete( path );
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete signer verification." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add delete verification." + " Exception: " + e.Message, e);
            }
        }
    }
}

