using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class SigningService
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings settings;

        public SigningService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.template = new UrlTemplate( baseUrl );
            this.restClient = restClient;
            this.settings = settings;
        }

        internal void SignDocument( PackageId packageId, SignedDocument signedDocument ) 
        {
            string path = template.UrlFor( UrlTemplate.SIGN_DOCUMENT_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(signedDocument, settings);
                restClient.Post( path, json );
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not sign document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not sign document." + " Exception: " + e.Message, e);
            }
        }

        internal void SignDocuments( PackageId packageId, Silanis.ESL.API.SignedDocuments documents ) 
        {
            string path = template.UrlFor( UrlTemplate.SIGN_DOCUMENTS_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(documents, settings);
                restClient.Post( path, json );
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not sign documents." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not sign documents." + " Exception: " + e.Message, e);
            }
        }

        internal void SignDocuments( PackageId packageId, Silanis.ESL.API.SignedDocuments documents, string signerSessionId ) 
        {
            string path = template.UrlFor( UrlTemplate.SIGN_DOCUMENTS_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try 
            {
                string json = JsonConvert.SerializeObject(documents, settings);
                restClient.Post( path, json, signerSessionId );
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not sign documents." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not sign documents." + " Exception: " + e.Message, e);
            }
        }

        internal SignedDocument ConvertToSignedDocument (Silanis.ESL.API.Document document)
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

