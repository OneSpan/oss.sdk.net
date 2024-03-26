using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.API;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using System.Collections.Specialized;
using System.Linq;

namespace OneSpanSign.Sdk.Services
{
    /// <summary>
    /// The PackageService class provides methods to help create packages and download documents after 
    /// the package is complete.
    /// </summary>
    public class PackageService
    {
        private UrlTemplate template;
        private JsonSerializerSettings settings;
        private RestClient restClient;
        private ReportService reportService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneSpanSign.Sdk.PackageService"/> class.
        /// </summary>
        /// <param name="apiToken">API token.</param>
        /// <param name="baseUrl">Base URL.</param>
        public PackageService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            template = new UrlTemplate(baseUrl);
            this.settings = settings;
            reportService = new ReportService(restClient, baseUrl, settings);
        }

        /// <summary>
        /// Creates a package based on the settings of the pacakge parameter.
        /// </summary>
        /// <returns>The package id.</returns>
        /// <param name="package">The package to create.</param>
        internal PackageId CreatePackage(OneSpanSign.API.Package package)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_PATH)
				.Build();
            try
            {
                string json = JsonConvert.SerializeObject(package, settings);
                string response = restClient.Post(path, json);				
                PackageId result = JsonConvert.DeserializeObject<PackageId>(response);

                return result;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create a new package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create a new package." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Creates a package based on the settings of the package parameter.
        /// 
        /// WARNING: This method does not work if the sender has a signature
        /// 
        /// </summary>
        /// <returns>The package id.</returns>
        /// <param name="package">The package to create.</param>
        internal PackageId CreatePackageOneStep(OneSpanSign.API.Package package, ICollection<OneSpanSign.Sdk.Document> documents)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_PATH)
            .Build();
            try
            {
                string json = JsonConvert.SerializeObject(package, settings);
                byte[] payloadBytes = Converter.ToBytes(json);

                string boundary = GenerateBoundary();
                byte[] content = CreateMultipartPackage(documents, payloadBytes, boundary);

                string response = restClient.PostMultipartPackage(path, content, boundary, json); 

                PackageId result = JsonConvert.DeserializeObject<PackageId>(response);

                return result;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create a new package one step." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create a new package one step." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Updates the package's fields and roles.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        /// <param name="package">The updated package.</param>
        //		internal void UpdatePackage (PackageId packageId, OneSpanSign.API.Package package)
        //		{
        //			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
        //				.Replace ("{packageId}", packageId.Id)
        //				.Build ();
        //
        //			try {
        //				string json = JsonConvert.SerializeObject (package, settings);
        //                string response = restClient.Put(path, json);
        //			} catch (Exception e) {
        //				throw new OssException ("Could not update the package." + " Exception: " + e.Message);
        //			}
        //		}

        /// <summary>
        /// Gets the package.
        /// </summary>
        /// <returns>The package.</returns>
        /// <param name="packageId">The package id.</param>
        internal OneSpanSign.API.Package GetPackage(PackageId packageId)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH)
				.Replace("{packageId}", packageId.Id)
				.Build();

            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<OneSpanSign.API.Package>(response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get package." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Deletes the document from the package.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        /// <param name="document">The document to delete.</param>
        public void DeleteDocument(PackageId packageId, Document document)
        {
            DeleteDocument(packageId, document.Id);
        }

        public void DeleteDocument(PackageId packageId, string documentId)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_ID_PATH)
							.Replace("{packageId}", packageId.Id)
							.Replace("{documentId}", documentId)
							.Build();

            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete document from package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete document from package." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Deletes the documents from the package.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        /// <param name="documentIds">The documents to delete.</param>
        public void DeleteDocuments (PackageId packageId, params string[] documentIds)
        {
            string path = template.UrlFor (UrlTemplate.DOCUMENT_PATH)
                            .Replace ("{packageId}", packageId.Id)
                            .Build ();

            try {
                string json = JsonConvert.SerializeObject (documentIds, settings);
                restClient.Delete (path, json);
            } catch (OssServerException e) {
                throw new OssServerException ("Could not delete documents from package." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new OssException ("Could not delete documents from package." + " Exception: " + e.Message, e);
            }
        }


        /// <summary>
        /// Get the document's metadata from the package.
        /// </summary>
        /// <returns>The document's metadata.</returns>
        /// <param name="package">The DocumentPackage we want to get document from.</param>
        /// <param name="documentId">Id of document to get.</param>
        public OneSpanSign.Sdk.Document GetDocumentMetadata(DocumentPackage package, string documentId)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_ID_PATH)
                .Replace("{packageId}", package.Id.Id)
                .Replace("{documentId}", documentId)
                .Build();

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Document apiDocument = JsonConvert.DeserializeObject<OneSpanSign.API.Document>(response, settings);

                // Wipe out the members not related to the metadata

                return new DocumentConverter(apiDocument, new DocumentPackageConverter(package).ToAPIPackage()).ToSDKDocument();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the document's metadata." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the document's metadata." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Updates the document's data, but not the actually document binary..
        /// </summary>
        /// <param name="package">The DocumentPackage to update.</param>
        /// <param name="document">The Document to update.</param>
        public void UpdateDocumentMetadata(DocumentPackage package, Document document)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_ID_PATH)
                .Replace("{packageId}", package.Id.Id)
                    .Replace("{documentId}", document.Id)
                    .Build();

            OneSpanSign.API.Document internalDoc = new DocumentConverter(document).ToAPIDocument();

            // Wipe out the members not related to the metadata
            internalDoc.Approvals = null;
            internalDoc.Fields = null;
            internalDoc.Pages = null;

            try
            {
                string json = JsonConvert.SerializeObject(internalDoc, settings);
                restClient.Put(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update the document's metadata." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update the document's metadata." + " Exception: " + e.Message, e);
            }

            IContractResolver prevContractResolver = settings.ContractResolver;
            settings.ContractResolver = DocumentMetadataContractResolver.Instance;            

            try
            {
                string json = JsonConvert.SerializeObject(internalDoc, settings);
                restClient.Put(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update the document's metadata." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update the document's metadata." + " Exception: " + e.Message, e);
            }
            finally
            {
                settings.ContractResolver = prevContractResolver;
            }
        }

        public void OrderDocuments(DocumentPackage package)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_PATH)
				.Replace("{packageId}", package.Id.Id)
				.Build();

            List<OneSpanSign.API.Document> documents = new List<OneSpanSign.API.Document>();
            foreach (Document doc in package.Documents)
            {
                documents.Add(new DocumentConverter(doc).ToAPIDocument());
            }

            try
            {
                string json = JsonConvert.SerializeObject(documents, settings);
                restClient.Put(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not order documents." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not order documents." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Uploads the list of documents from external providers.
        /// In most cases, adding a document from an external provider requires pre-development configurations.
        /// Please contact us for more information.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        /// <param name="document">The documents to be uploaded</param>
        /// 
        public void AddDocumentWithExternalContent(string packageId, IList<Document> providerDocuments)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_PATH)
                .Replace("{packageId}", packageId)
                    .Build();

            IList<OneSpanSign.API.Document> apiDocuments = new List<OneSpanSign.API.Document>();
            foreach (Document document in providerDocuments)
            {
                apiDocuments.Add(new DocumentConverter(document).ToAPIDocument());
            }
            try
            {
                string json = JsonConvert.SerializeObject(apiDocuments, settings);

                string response = restClient.Post(path, json);
                //OneSpanSign.API.Document uploadedDoc = JsonConvert.DeserializeObject<OneSpanSign.API.Document>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not add document with external content." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add document with external content." + " Exception: " + e.Message, e);
            }
        }

        public IList<Document> GetDocuments()
        {
            string path = template.UrlFor(UrlTemplate.PROVIDER_DOCUMENTS).Build();

            try
            {
                string response = restClient.Get(path);
                IList<OneSpanSign.API.Document> apiResponse = 
                    JsonConvert.DeserializeObject<IList<OneSpanSign.API.Document>>(response, settings);
                IList<Document> documents = new List<Document>();
                foreach (OneSpanSign.API.Document document in apiResponse)
                {
                    documents.Add(new DocumentConverter(document, null).ToSDKDocument());
                }
                return documents;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get documents." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get documents." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Sends the package.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        public void SendPackage(PackageId packageId)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            try
            {			
                restClient.Post(path, "{\"status\":\"SENT\"}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not send the package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not send the package." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Downloads a document from the package and returns it in a byte array.
        /// </summary>
        /// <returns>The document to download.</returns>
        /// <param name="packageId">The package id.</param>
        /// <param name="documentId">The id of the document to download.</param>
        public byte[] DownloadDocument(PackageId packageId, String documentId)
        {
            string path = template.UrlFor(UrlTemplate.PDF_PATH)
				.Replace("{packageId}", packageId.Id)
					.Replace("{documentId}", documentId)
					.Build();

            try
            {
                return restClient.GetHttpAsOctetStream(path).Contents;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the pdf document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the pdf document." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Downloads the orginal document (without fields) from the package and returns a byte array.
        /// </summary>
        /// <returns>The original document in bytes.</returns>
        /// <param name="packageId">Package identifier.</param>
        /// <param name="documentId">Document identifier.</param>
        public byte[] DownloadOriginalDocument(PackageId packageId, String documentId)
        {
            string path = template.UrlFor(UrlTemplate.ORIGINAL_PATH)
                .Replace("{packageId}", packageId.Id)
                .Replace("{documentId}", documentId)
                .Build();

            try
            {
                return restClient.GetHttpAsOctetStream(path).Contents;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the original document." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the original document." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Downloads the documents from the package in a zip file and returns it in a byte array.
        /// </summary>
        /// <returns>The zipped documents in byte array.</returns>
        /// <param name="packageId">.</param>
        public byte[] DownloadZippedDocuments(PackageId packageId)
        {
            string path = template.UrlFor(UrlTemplate.ZIP_PATH)
            .Replace("{packageId}", packageId.Id)
            .Build();

            try
            {
                return restClient.GetBytes(path).Contents;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the documents to a zip file." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the documents to a zip file." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Downloads the evidence summary from the package and returns it in a byte array.
        /// </summary>
        /// <returns>The evidence summary in byte array.</returns>
        /// <param name="packageId">The package id.</param>
        public byte[] DownloadEvidenceSummary(PackageId packageId)
        {
            string path = template.UrlFor(UrlTemplate.EVIDENCE_SUMMARY_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            try
            {
                return restClient.GetBytes(path).Contents;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the evidence summary." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the evidence summary." + " Exception: " + e.Message, e);
            }
        }

        internal void UpdatePackage(PackageId packageId, Package package)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();
                
            try
            {
                restClient.Put(path, JsonConvert.SerializeObject(package, settings));
                restClient.GetBytes(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to update package settings." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to update package settings." + " Exception: " + e.Message, e);
            }
        }

        internal void ChangePackageStatusToDraft(PackageId packageId) 
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            try
            {
                restClient.Put(path, "{\"status\":\"DRAFT\"}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to change the package status to DRAFT." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to change the package status to DRAFT." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Uploads the Document and file in byte[] to the package.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        /// <param name="fileName">The name of the document.</param>
        /// <param name="fileBytes">The file to upload in bytes.</param>
        /// <param name="document">The document object that has field settings.</param>
        internal Document UploadDocument(PackageId packageId, string fileName, byte[] fileBytes, Document document)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_PATH)
                .Replace("{packageId}", packageId.Id)
				.Build();

            OneSpanSign.API.Package internalPackage = GetPackage(packageId);
            OneSpanSign.API.Document internalDoc = new DocumentConverter(document).ToAPIDocument(internalPackage);

            try
            {
                string json = JsonConvert.SerializeObject(internalDoc, settings);
                byte[] payloadBytes = Converter.ToBytes(json);

                string boundary = GenerateBoundary();
                byte[] content = CreateMultipartContent(fileName, fileBytes, payloadBytes, boundary);

                string response = restClient.PostMultipartFile(path, content, boundary, json);

                OneSpanSign.API.Document uploadedDoc = JsonConvert.DeserializeObject<OneSpanSign.API.Document>(response);
                return new DocumentConverter(uploadedDoc, internalPackage).ToSDKDocument();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not upload document to package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not upload document to package." + " Exception: " + e.Message, e);
            }
        }

        internal IList<Document> UploadDocuments(PackageId packageId, IList<Document> documents)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            OneSpanSign.API.Package internalPackage = GetPackage(packageId);
            IList<OneSpanSign.API.Document> apiDocuments = new List<OneSpanSign.API.Document>();
            foreach (Document document in documents) 
            {
                apiDocuments.Add(new DocumentConverter(document).ToAPIDocument(internalPackage));
            }

            try
            {
                string json = JsonConvert.SerializeObject(apiDocuments, settings);
                byte[] payloadBytes = Converter.ToBytes(json);

                string boundary = GenerateBoundary();
                byte[] content = CreateMultipartPackage(documents, payloadBytes, boundary);

                string response = restClient.PostMultipartPackage(path, content, boundary, json); 

                IList<Document> sdkDocuments = new List<Document>();

                if(response.StartsWith("[")) 
                {
                    IList<OneSpanSign.API.Document> uploadedDocuments = 
                        JsonConvert.DeserializeObject<IList<OneSpanSign.API.Document>>(response, settings);

                    foreach (OneSpanSign.API.Document uploadedDocument in uploadedDocuments) 
                    {
                        sdkDocuments.Add(new DocumentConverter(uploadedDocument, internalPackage).ToSDKDocument());
                    }
                } 
                else 
                {
                    OneSpanSign.API.Document uploadedDocument = 
                        JsonConvert.DeserializeObject<OneSpanSign.API.Document>(response, settings);
                    sdkDocuments.Add(new DocumentConverter(uploadedDocument, internalPackage).ToSDKDocument());
                }

                return sdkDocuments;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not upload documents to package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not upload documents to package." + " Exception: " + e.Message, e);
            }
        }

        /// <summary>
        /// Uploads the list of documents from base64 content.
        /// </summary>
        /// <param name="packageId">The package id.</param>
        /// <param name="providerDocuments">The documents to be uploaded</param>
        ///
        internal IList<Document> AddDocumentWithBase64Content(PackageId packageId, IList<Document> documents)
        {
            string path = template.UrlFor(UrlTemplate.DOCUMENT_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            OneSpanSign.API.Package internalPackage = GetPackage(packageId);
            IList<OneSpanSign.API.Document> apiDocuments = new List<OneSpanSign.API.Document>();
            foreach (Document document in documents) 
            {
                apiDocuments.Add(new DocumentConverter(document).ToAPIDocument(internalPackage));
            }

            try
            {
                string json = JsonConvert.SerializeObject(apiDocuments, settings);

                string response = restClient.Post(path, json); 

                IList<Document> sdkDocuments = new List<Document>();

                if(response.StartsWith("[")) 
                {
                    IList<OneSpanSign.API.Document> uploadedDocuments = 
                        JsonConvert.DeserializeObject<IList<OneSpanSign.API.Document>>(response, settings);

                    foreach (OneSpanSign.API.Document uploadedDocument in uploadedDocuments) 
                    {
                        sdkDocuments.Add(new DocumentConverter(uploadedDocument, internalPackage).ToSDKDocument());
                    }
                } 
                else 
                {
                    OneSpanSign.API.Document uploadedDocument = 
                        JsonConvert.DeserializeObject<OneSpanSign.API.Document>(response, settings);
                    sdkDocuments.Add(new DocumentConverter(uploadedDocument, internalPackage).ToSDKDocument());
                }

                return sdkDocuments;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not upload documents to package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not upload documents to package." + " Exception: " + e.Message, e);
            }
        }

        private byte[] CreateMultipartContent (string fileName, byte[] fileBytes, byte[] payloadBytes, string boundary)
        {

            Encoding encoding = Encoding.UTF8;
            Stream formDataStream = new MemoryStream ();

            string header = string.Format ("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n",
                                           boundary, "payload", "paylaod");
            formDataStream.Write (encoding.GetBytes (header), 0, encoding.GetByteCount (header));
            formDataStream.Write (payloadBytes, 0, payloadBytes.Length);

            formDataStream.Write (encoding.GetBytes ("\r\n"), 0, encoding.GetByteCount ("\r\n"));

            string data = string.Format ("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                                         boundary, "file", fileName, MimeTypeUtil.GetMIMEType (fileName));
            formDataStream.Write (encoding.GetBytes (data), 0, encoding.GetByteCount (data));
            formDataStream.Write (fileBytes, 0, fileBytes.Length);

            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write (encoding.GetBytes (footer), 0, encoding.GetByteCount (footer));

            //Dump the stream
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read (formData, 0, formData.Length);
            formDataStream.Close ();

            return formData;
        }

        private byte[] CreateMultipartPackage(ICollection<OneSpanSign.Sdk.Document> documents, byte[] payloadBytes, string boundary)
        {

            Encoding encoding = Encoding.UTF8;
            Stream formDataStream = new MemoryStream();

            string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n",
                                           boundary, "payload", "payload");
            formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));
            formDataStream.Write(payloadBytes, 0, payloadBytes.Length);

            formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

            foreach (OneSpanSign.Sdk.Document document in documents)
            {
                if(document.External == null) {
                    string fileName = document.FileName;
                    byte[] fileBytes = document.Content;

                    string data = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n",
                        boundary, "file", fileName);
                    formDataStream.Write(encoding.GetBytes(data), 0, encoding.GetByteCount(data));
                    formDataStream.Write(fileBytes, 0, fileBytes.Length);

                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
                }
            }

            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            //Dump the stream
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public SigningStatus GetSigningStatus(PackageId packageId, string signerId, string documentId)
        {
            string path = template.UrlFor(UrlTemplate.SIGNING_STATUS_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{signerId}", !String.IsNullOrEmpty(signerId) ? signerId : "")
				.Replace("{documentId}", !String.IsNullOrEmpty(documentId) ? documentId : "")
				.Build();

            try
            {
                string response = restClient.Get(path);
                JsonTextReader reader = new JsonTextReader(new StringReader(response));

                //Loop 'till we get to the status value
                while (reader.Read () && reader.TokenType != JsonToken.String)
                {
                }

                return SigningStatusUtility.FromString(reader.Value.ToString());
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get signing status. Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get signing status. Exception: " + e.Message, e);
            }
        }

        internal IList<Role> GetRoles(PackageId packageId)
        {
            String path = template.UrlFor(UrlTemplate.ROLE_PATH)
				.Replace("{packageId}", packageId.Id)
				.Build();

            Result<Role> response = null;
            try
            {
                string stringResponse = restClient.Get(path);
                response = JsonConvert.DeserializeObject<Result<Role>>(stringResponse, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to retrieve role list for package with id " + packageId.Id + ".  " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to retrieve role list for package with id " + packageId.Id + ".  " + e.Message, e);
            }
            return response.Results;
        }

        public void DeletePackage(PackageId id)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH).Replace("{packageId}", id.Id).Build();

            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete package. Exception: " + e.Message, e.ServerError, e);    
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete package. Exception: " + e.Message, e);	
            }
        }

        public void Restore(PackageId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH).Replace("{packageId}", id.Id).Build();

            try
            {
                restClient.Post(path, "{\"trashed\":false}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to restore the package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to restore the package." + " Exception: " + e.Message, e);
            }
        }

        public void Trash(PackageId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH).Replace("{packageId}", id.Id).Build();

            try
            {
                restClient.Post(path, "{\"trashed\":true}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to trash the package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to trash the package." + " Exception: " + e.Message, e);
            }
        }

        public void Archive(PackageId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH).Replace("{packageId}", id.Id).Build();

            try
            {
                restClient.Put(path, "{\"status\":\"ARCHIVED\"}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to archive the package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to archive the package." + " Exception: " + e.Message, e);
            }
        }

        public void MarkComplete(PackageId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH).Replace("{packageId}", id.Id).Build();

            try
            {
                restClient.Put(path, "{\"status\":\"COMPLETED\"}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to mark complete on the package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to mark complete on the package." + " Exception: " + e.Message, e);
            }
        }

        public void Edit(PackageId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            string path = template.UrlFor(UrlTemplate.PACKAGE_ID_PATH).Replace("{packageId}", id.Id).Build();

            try
            {
                restClient.Put(path, "{\"status\":\"DRAFT\"}");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to edit package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to edit package." + " Exception: " + e.Message, e);
            }
        }

        private Role FindRoleForGroup(PackageId packageId, string groupId)
        {
            IList<Role> roles = GetRoles(packageId);

            foreach (Role role in roles)
            {
                if (role.Signers.Count > 0)
                {
                    OneSpanSign.API.Signer signer = role.Signers[0];
                    if (signer.Group != null && signer.Group.Id.Equals(groupId))
                    {
                        return role;
                    }
                }
            }
            return null;
        }

        public void NotifySigner(PackageId packageId, GroupId groupId)
        {
            Role role = FindRoleForGroup(packageId, groupId.Id);
            NotifySigner(packageId, role.Id);
        }

        public void NotifySigner(PackageId packageId, string roleId)
        {

            string path = template.UrlFor(UrlTemplate.NOTIFY_ROLE_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", roleId)
				.Build();

            try
            {
                restClient.Post(path, "");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Unable to send email notification.  " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Unable to send email notification.  " + e.Message, e);
            }
        }

        public void NotifySigner(PackageId packageId, string signerEmail, string message)
        {
            string path = template.UrlFor(UrlTemplate.NOTIFICATIONS_PATH).Replace("{packageId}", packageId.Id).Build();
            StringWriter sw = new StringWriter();

            using (JsonWriter json = new JsonTextWriter(sw))
            {
                json.Formatting = Formatting.Indented;
                json.WriteStartObject();
                json.WritePropertyName("email");
                json.WriteValue(signerEmail);
                json.WritePropertyName("message");
                json.WriteValue(message);
                json.WriteEndObject();
            }

            try
            {
                restClient.Post(path, sw.ToString());
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not send email notification to signer. Exception: " + e.Message, e.ServerError, e); 
            }
            catch (Exception e)
            {
                throw new OssException("Could not send email notification to signer. Exception: " + e.Message, e);	
            }
        }

        public Page<DocumentPackage> GetPackages(DocumentPackageStatus status, PageRequest request)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_LIST_PATH)
                .Replace("{status}", new PackageStatusConverter(status).ToAPIPackageStatus())
				.Replace("{from}", request.From.ToString())
				.Replace("{to}", request.To.ToString())
				.Build();

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.Package> results = JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.Package>>(response, settings);

                return ConvertToPage(results, request);
            }
            catch (OssServerException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new OssServerException("Could not get package list. Exception: " + e.Message, e.ServerError, e);  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new OssException("Could not get package list. Exception: " + e.Message, e);	
            }
        }

        public Page<Dictionary<String, String>> GetPackagesFields (DocumentPackageStatus status, PageRequest request, ISet<String> fields)
        {
            string path = template.UrlFor (UrlTemplate.PACKAGE_FIELDS_LIST_PATH)
                .Replace ("{status}", new PackageStatusConverter (status).ToAPIPackageStatus ())
                .Replace ("{from}", request.From.ToString ())
                .Replace ("{to}", request.To.ToString ())
                .Replace ("{fields}", string.Join(",", fields))
                .Build ();

            try 
            {
                string response = restClient.Get (path);
                Result<Dictionary<String, String>> results = JsonConvert.DeserializeObject<Result<Dictionary<String, String>>> (response, settings);

                return new Page<Dictionary<String, String>> (results.Results, results.Count.Value, request);
            } 
            catch (OssServerException e) 
            {
                Console.WriteLine (e.StackTrace);
                throw new OssServerException ("Could not get package list. Exception: " + e.Message, e.ServerError, e);
            } 
            catch (Exception e) 
            {
                Console.WriteLine (e.StackTrace);
                throw new OssException ("Could not get package list. Exception: " + e.Message, e);
            }
        }

        public Page<DocumentPackage> GetUpdatedPackagesWithinDateRange(DocumentPackageStatus status, PageRequest request, DateTime from, DateTime to)
        {
            string fromDate = DateHelper.dateToIsoUtcFormat(from);
            string toDate = DateHelper.dateToIsoUtcFormat(to);

            string path = template.UrlFor(UrlTemplate.PACKAGE_LIST_STATUS_DATE_RANGE_PATH)
                    .Replace("{status}", new PackageStatusConverter(status).ToAPIPackageStatus())
                    .Replace("{from}", request.From.ToString())
                    .Replace("{to}", request.To.ToString())
                    .Replace("{lastUpdatedStartDate}", fromDate)
                    .Replace("{lastUpdatedEndDate}", toDate)
                    .Build();

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.Package> results = JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.Package>>(response, settings);

                return ConvertToPage(results, request);
            }
            catch (OssServerException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new OssServerException("Could not get package list. Exception: " + e.Message, e.ServerError, e);  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new OssException("Could not get package list. Exception: " + e.Message, e);  
            }
        }

        public Page<DocumentPackage> GetTemplates(PageRequest request)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_PATH)
                .AddParam("type", "template")
                .AddParam("from", request.From.ToString())
                .AddParam("to", request.To.ToString())
                .Build();

            return GetTemplates(request, path);
        }

        public Page<DocumentPackage> GetTemplates(PageRequest request, Visibility visibility)
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_PATH)
                .AddParam("type", "template")
                .AddParam("from", request.From.ToString())
                .AddParam("to", request.To.ToString())
                .AddParam("visibility", visibility.GetName())
                .Build();

            return GetTemplates(request, path);
        }

        private Page<DocumentPackage> GetTemplates(PageRequest request, string path) 
        {
            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.Package> results = JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.Package>>(response, settings);

                return ConvertToPage(results, request);
            }
            catch (OssServerException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new OssServerException("Could not get template list. Exception: " + e.Message, e.ServerError, e); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new OssException("Could not get template list. Exception: " + e.Message, e); 
            }
        }

        private Page<DocumentPackage> ConvertToPage(OneSpanSign.API.Result<OneSpanSign.API.Package> results, PageRequest request)
        {
            IList<DocumentPackage> converted = new List<DocumentPackage>();

            foreach (OneSpanSign.API.Package package in results.Results)
            {
                DocumentPackage dp = new DocumentPackageConverter(package).ToSDKPackage();

                converted.Add(dp);
            }

            return new Page<DocumentPackage>(converted, results.Count.Value, request);
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

        public string AddSigner(PackageId packageId, Signer signer)
        {
            Role apiPayload = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());

            string path = template.UrlFor(UrlTemplate.ADD_SIGNER_PATH)
				.Replace("{packageId}", packageId.Id)
				.Build();
            try
            {
                string json = JsonConvert.SerializeObject(apiPayload, settings);
                string response = restClient.Post(path, json);              
                Role apiRole = JsonConvert.DeserializeObject<Role>(response);

                return apiRole.Id;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not add signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add signer." + " Exception: " + e.Message, e);
            }
        }

        public Signer GetSigner(PackageId packageId, string signerId)
        {
            string path = template.UrlFor(UrlTemplate.GET_SIGNER_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", signerId)
				.Build();

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Role apiRole = JsonConvert.DeserializeObject<OneSpanSign.API.Role>(response, settings);
                return new SignerConverter(apiRole).ToSDKSigner();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not retrieve signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not retrieve signer." + " Exception: " + e.Message, e);
            }
        }

        public void UpdateSigner(PackageId packageId, Signer signer)
        {
            Role apiPayload = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());

            string path = template.UrlFor(UrlTemplate.UPDATE_SIGNER_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", signer.Id)
				.Build();
            try
            {
                string json = JsonConvert.SerializeObject(apiPayload, settings);
                restClient.Put(path, json);              
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update signer." + " Exception: " + e.Message, e);
            }
        }

        public void RemoveSigner(PackageId packageId, string signerId)
        {
            string path = template.UrlFor(UrlTemplate.REMOVE_SIGNER_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", signerId)
				.Build();

            try
            {
                restClient.Delete(path);
                return;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete signer." + " Exception: " + e.Message, e);
            }
        }

        public void OrderSigners(DocumentPackage package)
        {
            string path = template.UrlFor(UrlTemplate.ROLE_PATH)
				.Replace("{packageId}", package.Id.Id)
				.Build();

            List<OneSpanSign.API.Role> roles = new List<OneSpanSign.API.Role>();
            foreach (Signer signer in package.Signers)
            {
                roles.Add(new SignerConverter(signer).ToAPIRole(signer.Id));
            }

            try
            {
                string json = JsonConvert.SerializeObject(roles, settings);
                restClient.Put(path, json);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not order signers." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not order signers." + " Exception: " + e.Message, e);
            }
        }

        public void UnlockSigner(PackageId packageId, string senderId)
        {
            string path = template.UrlFor(UrlTemplate.ROLE_UNLOCK_PATH)
                .Replace("{packageId}", packageId.Id)
                    .Replace("{roleId}", senderId)
                    .Build();

            try
            {
                restClient.Post(path, null);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not unlock signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not unlock signer." + " Exception: " + e.Message, e);
            }
        }

        public void ConfigureDocumentVisibility(PackageId packageId, DocumentVisibility visibility)
        {
            string path = template.UrlFor( UrlTemplate.DOCUMENT_VISIBILITY_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            OneSpanSign.API.DocumentVisibility apiVisibility = new DocumentVisibilityConverter(visibility).ToAPIDocumentVisibility();
            string json = JsonConvert.SerializeObject(apiVisibility, settings);

            try 
            {
                restClient.Post(path, json);
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not configure document visibility." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not configure document visibility." + " Exception: " + e.Message, e);
            }
        }

        public DocumentVisibility GetDocumentVisibility(PackageId packageId) 
        {
            string path = template.UrlFor( UrlTemplate.DOCUMENT_VISIBILITY_PATH )
                .Replace("{packageId}", packageId.Id)
                .Build();

            try {
                string response = restClient.Get(path);
                OneSpanSign.API.DocumentVisibility apiVisibility = JsonConvert.DeserializeObject<OneSpanSign.API.DocumentVisibility>(response, settings);

                return new DocumentVisibilityConverter(apiVisibility).ToSDKDocumentVisibility();
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get document visibility." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get document visibility." + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.Sdk.Document> GetDocuments(PackageId packageId, string signerId) 
        {
            Package aPackage = GetPackage(packageId);
            DocumentPackageConverter converter = new DocumentPackageConverter(aPackage);
            DocumentPackage documentPackage = converter.ToSDKPackage();
            DocumentVisibility visibility = GetDocumentVisibility(packageId);

            return visibility.GetDocuments(documentPackage, signerId);
        }

        public IList<OneSpanSign.Sdk.Signer> GetSigners(PackageId packageId, string documentId) 
        {
            Package aPackage = GetPackage(packageId);
            DocumentPackageConverter converter = new DocumentPackageConverter(aPackage);
            DocumentPackage documentPackage = converter.ToSDKPackage();
            DocumentVisibility visibility = GetDocumentVisibility(packageId);

            return visibility.GetSigners(documentPackage, documentId);
        }

        [Obsolete("Use OneSpanSign.Sdk.Services.ReportService.DownloadCompletionReportAsCSV")]
        public string DownloadCompletionReportAsCSV(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, String senderId, DateTime from, DateTime to)
        {
            return reportService.DownloadCompletionReportAsCSV(packageStatus, senderId, from, to);
        }

        [Obsolete("Use OneSpanSign.Sdk.Services.ReportService.DownloadCompletionReport")]
        public OneSpanSign.Sdk.CompletionReport DownloadCompletionReport(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, String senderId, DateTime from, DateTime to)
        {
            return reportService.DownloadCompletionReport(packageStatus, senderId, from, to);
        }

        [Obsolete("Use OneSpanSign.Sdk.Services.ReportService.DownloadCompletionReportAsCSV")]
        public string DownloadCompletionReportAsCSV(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, DateTime from, DateTime to)
        {
            return reportService.DownloadCompletionReportAsCSV(packageStatus, from, to);
        }

        [Obsolete("Use OneSpanSign.Sdk.Services.ReportService.DownloadCompletionReport")]
        public OneSpanSign.Sdk.CompletionReport DownloadCompletionReport(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, DateTime from, DateTime to)
        {
            return reportService.DownloadCompletionReport(packageStatus, from, to);
        }

        [Obsolete("Use OneSpanSign.Sdk.Services.ReportService.DownloadUsageReportAsCSV")]
        public string DownloadUsageReportAsCSV(DateTime from, DateTime to)
        {
            return reportService.DownloadUsageReportAsCSV(from, to);
        }

        [Obsolete("Use OneSpanSign.Sdk.Services.ReportService.DownloadUsageReport")]
        public OneSpanSign.Sdk.UsageReport DownloadUsageReport(DateTime from, DateTime to)
        {
            return reportService.DownloadUsageReport(from, to);
        }

        public string GetSigningUrl(PackageId packageId, string signerId) 
        {
            Package package = GetPackage(packageId);

            return GetSigningUrl(packageId, GetRole(package, signerId));
        }

        private Role GetRole(Package package, string sigenrId) 
        {
            foreach(Role role in package.Roles) 
            {
                foreach(OneSpanSign.API.Signer signer in role.Signers) 
                {
                    if(signer.Id.Equals(sigenrId)) 
                    {
                        return role;
                    }
                }
            }
            return new Role();
        }

        private Role GetRoleByEmail(Package package, string signerEmail)
        {
            foreach(Role role in package.Roles) 
            {
                foreach(OneSpanSign.API.Signer signer in role.Signers) 
                {
                    if(signer.Email.Equals(signerEmail)) 
                    {
                        return role;
                    }
                }
            }
            return new Role();
        }

        private string GetSigningUrl(PackageId packageId, Role role) 
        {

            string path = template.UrlFor(UrlTemplate.SIGNER_URL_PATH)
                         .Replace("{packageId}", packageId.Id)
                         .Replace("{roleId}", role.Id)
                         .Build();

            try 
            {
                string response = restClient.Get(path);
                SigningUrl signingUrl = JsonConvert.DeserializeObject<OneSpanSign.API.SigningUrl>(response, settings);
                return signingUrl.Url;
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get a signing url." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get a signing url." + " Exception: " + e.Message, e);
            }
        }

        public string StartFastTrack(PackageId packageId, List<FastTrackSigner> signers) 
        {
            string token = GetFastTrackToken(packageId, true);
            string path = template.UrlFor(UrlTemplate.START_FAST_TRACK_PATH)
                         .Replace("{token}", token)
                         .Build();

            List<FastTrackRole> roles = new List<FastTrackRole>();
            foreach(FastTrackSigner signer in signers) {
                FastTrackRole role = FastTrackRoleBuilder.NewRoleWithId(signer.Id)
                        .WithName(signer.Id)
                        .WithSigner(signer)
                        .Build();
                roles.Add(role);
            }

            string json = JsonConvert.SerializeObject(roles, settings);

            try
            {
                string response = restClient.Post(path, json);
                SigningUrl signingUrl = JsonConvert.DeserializeObject<OneSpanSign.API.SigningUrl>(response, settings);
                return signingUrl.Url;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not start fast track." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not start fast track." + " Exception: " + e.Message, e);
            }
        }

        private string GetFastTrackToken(PackageId packageId, Boolean signing) 
        {
            string fastTrackUrl = GetFastTrackUrl(packageId, signing);
            string finalUrl = RedirectResolver.ResolveUrlAfterRedirect(fastTrackUrl);

            return finalUrl.Substring(finalUrl.LastIndexOf('=') + 1);
        }

        private string GetFastTrackUrl(PackageId packageId, Boolean signing) 
        {
            string path = template.UrlFor(UrlTemplate.FAST_TRACK_URL_PATH)
                                  .Replace("{packageId}", packageId.Id)
                                  .Replace("{signing}", signing.ToString())
                                  .Build();

            try 
            {
                string response = restClient.Get(path);
                SigningUrl signingUrl = JsonConvert.DeserializeObject<OneSpanSign.API.SigningUrl>(response, settings);
                return signingUrl.Url;
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get a fastTrack url." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get a fastTrack url." + " Exception: " + e.Message, e);
            }
        }

        public void SendSmsToSigner(PackageId packageId, Signer signer) 
        {
            Role role = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());
            SendSmsToSigner(packageId, role);
        }

        private void SendSmsToSigner(PackageId packageId, Role role) 
        {
            string path = template.UrlFor(UrlTemplate.SEND_SMS_TO_SIGNER_PATH)
                                  .Replace("{packageId}", packageId.Id)
                                  .Replace("{roleId}", role.Id)
                                  .Build();

            try
            {
                restClient.Post(path, null);
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not send SMS to the signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not send SMS to the signer." + " Exception: " + e.Message, e);
            }
        }

        public List<OneSpanSign.Sdk.NotaryJournalEntry> GetJournalEntries(string userId) 
        {
            List<OneSpanSign.Sdk.NotaryJournalEntry> result = new List<OneSpanSign.Sdk.NotaryJournalEntry>();

            string path = template.UrlFor(UrlTemplate.NOTARY_JOURNAL_PATH)
                    .Replace("{userId}", userId)
                    .Build();

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.NotaryJournalEntry> apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.NotaryJournalEntry>> (response, settings );

                foreach ( OneSpanSign.API.NotaryJournalEntry apiNotaryJournalEntry in apiResponse.Results ) 
                {
                    result.Add( new NotaryJournalEntryConverter( apiNotaryJournalEntry ).ToSDKNotaryJournalEntry() );
                }

                return result;

            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get Journal Entries." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get Journal Entries." + " Exception: " + e.Message, e);
            }
        }

        public DownloadedFile GetJournalEntriesAsCSV(string userId) 
        {
            string path = template.UrlFor(UrlTemplate.NOTARY_JOURNAL_CSV_PATH)
                    .Replace("{userId}", userId)
                    .Build();

            try
            {
                return restClient.GetBytes(path);
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get Journal Entries in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get Journal Entries in csv." + " Exception: " + e.Message, e);
            }
        }

        public string GetThankYouDialogContent(PackageId packageId) 
        {
            string path = template.UrlFor(UrlTemplate.THANK_YOU_DIALOG_PATH)
                    .Replace("{packageId}", packageId.Id)
                    .Build();

            try
            {
                string response = restClient.Get(path);
                Dictionary<string, string> thankYouDialogContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(response, settings);
                return thankYouDialogContent["body"];
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get thank you dialog content." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get thank you dialog content." + " Exception: " + e.Message, e);
            }
        }

        public SupportConfiguration GetConfig(PackageId packageId) 
        {
            string path = template.UrlFor(UrlTemplate.PACKAGE_INFORMATION_CONFIG_PATH)
                    .Replace("{packageId}", packageId.Id)
                    .Build();

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.SupportConfiguration apiSupportConfiguration = JsonConvert.DeserializeObject<OneSpanSign.API.SupportConfiguration>(response, settings);
                return new SupportConfigurationConverter(apiSupportConfiguration).ToSDKSupportConfiguration();
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get support configuration." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get support configuration." + " Exception: " + e.Message, e);
            }
        }

        public ReferencedConditions GetReferencedConditions(String packageId)
        {
            return this.GetReferencedConditions(packageId, null, null);
        }

        public ReferencedConditions GetReferencedConditions(String packageId, String documentId)
        {
            return this.GetReferencedConditions(packageId, documentId, null);
        }

        public ReferencedConditions GetReferencedConditions(String packageId, String documentId, String fieldId)
        {
            String path = template.UrlFor(UrlTemplate.PACKAGE_REFERENCED_CONDITIONS_PATH)
                .Replace("{packageId}", packageId)
                .AddParam("documentId", documentId)
                .AddParam("fieldId", fieldId)
                .Build();

            try
            {
                string stringResponse = restClient.Get(path);
                API.ReferencedConditions apiReferencedConditions = JsonConvert.DeserializeObject<API.ReferencedConditions>(stringResponse, settings);
                return ReferencedConditionsConverter.ToSDK(apiReferencedConditions);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not get referenced conditions." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not get referenced conditions." + " Exception: " + e.Message, e);
            }
        }
    }
}