using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    public class SigningService
    {
        private RestClient restClient;
        private JsonSerializerSettings settings;
        private string baseUrl;

        public SigningService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.baseUrl = baseUrl;
            this.restClient = restClient;
            this.settings = settings;
        }

        internal void SignDocument( PackageId packageId, SignedDocument signedDocument ) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.SIGN_DOCUMENT_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(signedDocument, settings);
                restClient.Post( path, json );
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not sign document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not sign document." + " Exception: " + e.Message, e);
            }
        }

        internal void SignDocuments( PackageId packageId, OneSpanSign.API.SignedDocuments documents ) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.SIGN_DOCUMENTS_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(documents, settings);
                restClient.Post( path, json );
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not sign documents." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not sign documents." + " Exception: " + e.Message, e);
            }
        }

        internal void SignDocuments( PackageId packageId, OneSpanSign.API.SignedDocuments documents, string signerSessionId ) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.SIGN_DOCUMENTS_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(documents, settings);
                restClient.Post( path, json, signerSessionId );
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not sign documents." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not sign documents." + " Exception: " + e.Message, e);
            }
        }

        internal SignedDocument ConvertToSignedDocument (OneSpanSign.API.Document document)
        {
            SignedDocument signedDocument = new SignedDocument ();

            signedDocument.Id = document.Id;
            signedDocument.Name = document.Name;
            signedDocument.Description = document.Description;
            signedDocument.Approvals = document.Approvals;
            signedDocument.External = document.External;
            signedDocument.Index = document.Index;
            signedDocument.Extract = document.Extract;
            signedDocument.ExtractionTypes = document.ExtractionTypes;
            signedDocument.Fields = document.Fields;
            signedDocument.Data = document.Data;
            signedDocument.SignedHash = document.SignedHash;
            signedDocument.Pages = document.Pages;
            signedDocument.Size = document.Size;
            signedDocument.Status = document.Status;
            signedDocument.SignerVerificationToken = document.SignerVerificationToken;
            signedDocument.Tagged = document.Tagged;

            return signedDocument;
        }
    }
}

