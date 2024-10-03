using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class SignatureImageService
    {
        private JsonSerializerSettings settings;
        private RestClient client;
        private string baseUrl;

        public SignatureImageService(RestClient client, string baseUrl, JsonSerializerSettings settings)
        {
            this.client = client;
            this.baseUrl = baseUrl;
            this.settings = settings;
        }

        public DownloadedFile GetSignatureImageForSender(string senderId, SignatureImageFormat format) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor( UrlTemplate.SIGNATURE_IMAGE_FOR_SENDER_PATH)
                .Replace("{senderId}", senderId)
                .Build();
            try 
            {
                return client.GetBytes(path, AcceptType(format));
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download signature image for sender." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download signature image for sender." + " Exception: " + e.Message, e);
            }
        }

        public DownloadedFile GetSignatureImageForPackageRole(PackageId packageId, string signerId, SignatureImageFormat format) 
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SIGNATURE_IMAGE_FOR_PACKAGE_ROLE_PATH)
                .Replace("{packageId}", packageId.Id)
                .Replace("{roleId}", signerId)
                .Build();
            try 
            {
                return client.GetBytes(path, AcceptType(format));
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download signature image for package signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download signature image for package signer." + " Exception: " + e.Message, e);
            }
        }

        private string AcceptType(SignatureImageFormat format)
        {
            switch (format)
            {
                case SignatureImageFormat.PNG:
                    return "image/png";
                case SignatureImageFormat.JPG:
                    return "image/jpeg";
                case SignatureImageFormat.GIF:
                    return "image/gif";
                default:
                    throw new OssException("Unknown SignatureImageFormat: " + format, null);
            }
        }
    }
}

