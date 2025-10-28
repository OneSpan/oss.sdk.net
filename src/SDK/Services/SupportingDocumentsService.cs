using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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
                throw new OssServerException(
                    "Could not download all supporting documents." + " Exception: " + e.Message,
                    e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download all supporting documents." + " Exception: " + e.Message, e);
            }
        }

        public List<SupportingDocumentsInfo> GetListOfSupportingDocuments(string transactionUid)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_PATH)
                .Replace("{transactionUid}", transactionUid)
                .Build();
            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<List<SupportingDocumentsInfo>>(response);
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

        public List<SupportingDocumentsInfo> UploadSupportingDocuments(string transactionUid,
            Dictionary<string, byte[]> files)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_PATH)
                .Replace("{transactionUid}", transactionUid)
                .Build();
            try
            {
                string boundary = GenerateBoundary();
                Task<byte[]> content = CreateMultipartContentAsync(files, boundary);
                string response = restClient.PostMultipartFile(path, content.Result, boundary, "");

                return JsonConvert.DeserializeObject<List<SupportingDocumentsInfo>>(response);
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

        public SupportingDocumentsInfo RenameSupportingDocument(string transactionUid, int documentId,
            Dictionary<String, String> payload)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.SUPPORTING_DOCUMENTS_DELETE_RENAME)
                .Replace("{transactionUid}", transactionUid)
                .Replace("{documentId}", documentId.ToString())
                .Build();
            try
            {
                string response = restClient.Put(path, JsonConvert.SerializeObject(payload));
                return JsonConvert.DeserializeObject<SupportingDocumentsInfo>(response);
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
            return "----Boundary" + Guid.NewGuid().ToString("N");
        }
       
        private async Task<byte[]> CreateMultipartContentAsync(Dictionary<string, byte[]> files, string boundary)
        {
            using (var multipartContent = new MultipartFormDataContent(boundary))
            {
                foreach (var kvp in files)
                {
                    string fileName = kvp.Key;
                    byte[] fileBytes = kvp.Value;

                    var byteArrayContent = new ByteArrayContent(fileBytes);
                    byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeTypeUtil.GetMIMEType(fileName));

                    // Add file under the same field name "files"
                    multipartContent.Add(byteArrayContent, "files", fileName);
                }

                using (var memoryStream = new MemoryStream())
                {
                    await multipartContent.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}