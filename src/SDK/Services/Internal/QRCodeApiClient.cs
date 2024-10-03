using System;
using Newtonsoft.Json;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class QRCodeApiClient
    {
        private RestClient restClient;
        private JsonSerializerSettings settings;
        private string baseUrl;

        public QRCodeApiClient(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.settings = settings;
            this.baseUrl = baseUrl;
        }
            
        public string AddQRCode(string packageId, string documentId, OneSpanSign.API.Field apiField)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.QRCODE_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Build();

            string json = JsonConvert.SerializeObject(apiField, settings);

            try
            {
                string response = restClient.Post(path, json);
                OneSpanSign.API.Field result = JsonConvert.DeserializeObject<OneSpanSign.API.Field>(response, settings);
                return result.Id;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not add QR code to document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add QR code to document." + " Exception: " + e.Message, e);
            }
        }

        public void ModifyQRCode(string packageId, string documentId, OneSpanSign.API.Field apiField)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.QRCODE_ID_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Replace("{fieldId}", apiField.Id)
                .Build();

            string json = JsonConvert.SerializeObject(apiField, settings);

            try
            {
                restClient.Put(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not modify QR code in document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not modify QR code in document." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Field GetQRCode(string packageId, string documentId, string fieldId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.QRCODE_ID_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Replace("{fieldId}", fieldId)
                .Build();

            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<OneSpanSign.API.Field>(response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get QR code from document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get QR code from document." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteQRCode(string packageId, string documentId, string fieldId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.QRCODE_ID_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Replace("{fieldId}", fieldId)
                .Build();

            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete QR code from document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete QR code from document." + " Exception: " + e.Message, e);
            }
        }

        public void UpdateQRCodes(string packageId, string documentId, IList<OneSpanSign.API.Field> qrCodeList)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.QRCODE_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Build();

            try
            {
                string json = JsonConvert.SerializeObject (qrCodeList, settings);
                restClient.Put(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update QR codes in document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update QR codes in document." + " Exception: " + e.Message, e);
            }
        }
    }
}

