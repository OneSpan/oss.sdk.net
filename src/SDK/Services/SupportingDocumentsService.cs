using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.Sdk.Models;

namespace OneSpanSign.Sdk.Services
{
    public class SupportingDocumentsService
    {
        private RestClient restClient;
        private string baseUrl;

        public SupportingDocumentsService(RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
            this.baseUrl = baseUrl;
        }

        public DocumentMetadata DownloadSupportingDocument(string transactionUid, int documentId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_DOWNLOAD)
                .Replace("{transactionUid}", transactionUid)
                .Replace("{documentId}", documentId.ToString())
                .Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<DocumentMetadata>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download supporting document." + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download supporting document." + " Exception: " + e.Message, e);
            }
        }
        
        public DownloadedFile DownloadAllSupportingDocuments(string transactionUid)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_DOWNLOAD_ALL)
                .Replace("{transactionUid}", transactionUid)
                .Build();
            try
            {
                return restClient.GetBytes(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download all supporting documents." + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download all supporting documents." + " Exception: " + e.Message, e);
            }
        }

        public List<SupportingDocumentDocumentInfo> GetListOfSupportingDocuments(string transactionUid)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_PATH)
                .Replace("{transactionUid}", transactionUid)
                .Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<List<SupportingDocumentDocumentInfo>>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get list of supporting documents." + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get list of supporting documents." + " Exception: " + e.Message, e);
            }
        }

        public List<SupportingDocumentDocumentInfo> UploadSupportingDocuments(string transactionUid, Dictionary<string, byte[]> files)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_PATH)
                .Replace("{transactionUid}", transactionUid)
                .Build();
            try
            {
                string boundary = GenerateBoundary();
                byte[] content = CreateMultipartContent(files, boundary);
                string response = restClient.PostMultipartFile(path, content, boundary, "");

                return JsonConvert.DeserializeObject<List<SupportingDocumentDocumentInfo>>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not upload supporting document.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not upload supporting document." + " Exception: " + e.Message, e);
            }
        }
        
        public SupportingDocumentDocumentInfo RenameSupportingDocument(string transactionUid, int documentId, Dictionary<String, String> payload)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_DELETE_RENAME)
                .Replace("{transactionUid}", transactionUid)
                .Replace("{documentId}", documentId.ToString())
                .Build();
            try
            {
                string response = restClient.Put(path, JsonConvert.SerializeObject(payload));
                return JsonConvert.DeserializeObject<SupportingDocumentDocumentInfo>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not rename supporting document." + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not rename supporting document." + " Exception: " + e.Message, e);
            }
        }
        
        public void DeleteSupportingDocument(string transactionUid, int documentId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_DELETE_RENAME)
                .Replace("{transactionUid}", transactionUid)
                .Replace("{documentId}", documentId.ToString())
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete supporting document..", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete supporting document." + " Exception: " + e.Message, e);
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
        
        private byte[] CreateMultipartContent(Dictionary<string, byte[]> files, string boundary)
        {
            Encoding encoding = Encoding.UTF8;
            using (MemoryStream formDataStream = new MemoryStream())
            {
                foreach (var kvp in files)
                {
                    string fileName = kvp.Key;
                    byte[] fileBytes = kvp.Value;

                    // Multipart header for this file
                    string header = string.Format(
                        "--{0}\r\nContent-Disposition: form-data; name=\"files\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                        boundary, fileName, MimeTypeUtil.GetMIMEType(fileName));

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // File content
                    formDataStream.Write(fileBytes, 0, fileBytes.Length);

                    // Line break after file
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
                }

                // Closing boundary
                string footer = $"--{boundary}--\r\n";
                formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

                // Convert stream to byte array
                formDataStream.Position = 0;
                byte[] formData = new byte[formDataStream.Length];
                formDataStream.Read(formData, 0, formData.Length);

                return formData;
            }
        }
    }
}