using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
    public class SignatureImageService
    {
        private UrlTemplate template;
        private JsonSerializerSettings settings;
        private RestClient client;

        public SignatureImageService(RestClient client, string baseUrl, JsonSerializerSettings settings)
        {
            this.client = client;
            template = new UrlTemplate(baseUrl);
            this.settings = settings;
        }

        public DownloadedFile GetSignatureImageForSender(string senderId, string acceptType) 
        {
            string path = template.UrlFor( UrlTemplate.SIGNATURE_IMAGE_FOR_SENDER_PATH)
                .Replace("{senderId}", senderId)
                .Build();
            try 
            {
                return client.GetBytes(path, acceptType);
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not download signature image for sender." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not download signature image for sender." + " Exception: " + e.Message, e);
            }
        }

        public DownloadedFile GetSignatureImageForPackageRole(PackageId packageId, string signerId, string acceptType) 
        {
            string path = template.UrlFor(UrlTemplate.SIGNATURE_IMAGE_FOR_PACKAGE_ROLE_PATH)
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", signerId)
                .Build();
            try 
            {
                return client.GetBytes(path, acceptType);
            }
            catch (EslServerException e)
            {
                throw new EslServerException("Could not download signature image for package signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new EslException("Could not download signature image for package signer." + " Exception: " + e.Message, e);
            }
        }
    }
}

