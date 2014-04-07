using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Services
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Silanis.ESL.SDK.PackageService"/> class.
		/// </summary>
		/// <param name="apiToken">API token.</param>
		/// <param name="baseUrl">Base URL.</param>
		public PackageService (RestClient restClient, string baseUrl)
		{
            this.restClient = restClient;
			template = new UrlTemplate (baseUrl);
			settings = new JsonSerializerSettings ();
			settings.NullValueHandling = NullValueHandling.Ignore;
		}

		/// <summary>
		/// Creates a package based on the settings of the pacakge parameter.
		/// </summary>
		/// <returns>The package id.</returns>
		/// <param name="package">The package to create.</param>
		internal PackageId CreatePackage (Silanis.ESL.API.Package package)
		{
			Support.LogMethodEntry(package);
			string path = template.UrlFor (UrlTemplate.PACKAGE_PATH)
				.Build ();
			try {
				string json = JsonConvert.SerializeObject (package, settings);
                string response = restClient.Post(path, json);				
				PackageId result = JsonConvert.DeserializeObject<PackageId> (response);
				Support.LogMethodExit(result);
				return result;
			} catch (Exception e) {
				throw new EslException ("Could not create a new package." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Updates the package's fields and roles.
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="package">The updated package.</param>
		internal void UpdatePackage (PackageId packageId, Silanis.ESL.API.Package package)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
				.Replace ("{packageId}", packageId.Id)
				.Build ();

			try {
				string json = JsonConvert.SerializeObject (package, settings);
                string response = restClient.Put(path, json);
			} catch (Exception e) {
				throw new EslException ("Could not update the package." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Gets the package.
		/// </summary>
		/// <returns>The package.</returns>
		/// <param name="packageId">The package id.</param>
		internal Silanis.ESL.API.Package GetPackage (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
				.Replace ("{packageId}", packageId.Id)
				.Build ();

			try {
                string response = restClient.Get(path);
				return JsonConvert.DeserializeObject<Silanis.ESL.API.Package> (response, settings);
			} catch (Exception e) {
				throw new EslException ("Could not get package." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Deletes the document from the package.
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="document">The document to delete.</param>
		public void DeleteDocument (PackageId packageId, Document document)
		{
			string path = template.UrlFor (UrlTemplate.DOCUMENT_ID_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{documentId}", "document.Id")
                .Build ();

			try {
                restClient.Delete(path);
			} catch (Exception e) {
				throw new EslException ("Could not delete document from package." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Sends the package.
		/// </summary>
		/// <param name="packageId">The package id.</param>
		public void SendPackage (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Build ();

			try {
				byte[] content = Converter.ToBytes ("{\"status\":\"SENT\"}");
                restClient.Post(path, "{\"status\":\"SENT\"}");
			} catch (Exception e) {
				throw new EslException ("Could not send the package." + " Exception: " + e.Message);
			}
		}	

		/// <summary>
		/// Downloads a document from the package and returns it in a byte array.
		/// </summary>
		/// <returns>The document to download.</returns>
		/// <param name="packageId">The package id.</param>
		/// <param name="documentId">The id of the document to download.</param>
		public byte[] DownloadDocument (PackageId packageId, String documentId)
		{
			string path = template.UrlFor (UrlTemplate.PDF_PATH)
				.Replace ("{packageId}", packageId.Id)
					.Replace ("{documentId}", documentId)
					.Build ();

			try {
                return restClient.GetBytes(path);
			} catch (Exception e) {
				throw new EslException ("Could not download the pdf document." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Downloads the documents from the package in a zip file and returns it in a byte array.
		/// </summary>
		/// <returns>The zipped documents in byte array.</returns>
		/// <param name="packageId">.</param>
		public byte[] DownloadZippedDocuments (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.ZIP_PATH)
            .Replace ("{packageId}", packageId.Id)
            .Build ();

			try {
                return restClient.GetBytes(path);
			} catch (Exception e) {
				throw new EslException ("Could not download the documents to a zip file." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Downloads the evidence summary from the package and returns it in a byte array.
		/// </summary>
		/// <returns>The evidence summary in byte array.</returns>
		/// <param name="packageId">The package id.</param>
		public byte[] DownloadEvidenceSummary (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.EVIDENCE_SUMMARY_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Build ();

			try {
                return restClient.GetBytes(path);
			} catch (Exception e) {
				throw new EslException ("Could not download the evidence summary." + " Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Uploads the Document and file in byte[] to the package.
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="fileName">The name of the document.</param>
		/// <param name="fileBytes">The file to upload in bytes.</param>
		/// <param name="document">The document object that has field settings.</param>
		internal void UploadDocument (PackageId packageId, string fileName, byte[] fileBytes, Silanis.ESL.API.Document document)
		{
            Support.LogMethodEntry(packageId, fileName, document);
			string path = template.UrlFor (UrlTemplate.DOCUMENT_PATH)
				.Replace ("{packageId}", packageId.Id)
					.Build ();

			try {
				string json = JsonConvert.SerializeObject (document, settings);
                Support.LogDebug("document json = " + json);
				byte[] payloadBytes = Converter.ToBytes (json);

				string boundary = GenerateBoundary ();
				byte[] content = CreateMultipartContent (fileName, fileBytes, payloadBytes, boundary);

                restClient.PostMultipartFile(path, content, boundary);
                Support.LogMethodExit("Document uploaded without issue");
//				Converter.ToString (HttpMethods.MultipartPostHttp (apiToken, path, content, boundary));
			} catch (Exception e) {
				throw new EslException ("Could not upload document to package." + " Exception: " + e.Message);
			}
		}

		private byte[] CreateMultipartContent (string fileName, byte[] fileBytes, byte[] payloadBytes, string boundary)
		{

			Encoding encoding = Encoding.UTF8;
			Stream formDataStream = new MemoryStream ();

			string header = string.Format ("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n",
			                               boundary, "payload", "payload");
			formDataStream.Write (encoding.GetBytes (header), 0, encoding.GetByteCount (header));
			formDataStream.Write (payloadBytes, 0, payloadBytes.Length);

			formDataStream.Write (encoding.GetBytes ("\r\n"), 0, encoding.GetByteCount ("\r\n"));

			string data = string.Format ("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; \r\nContent-Type: {2}\r\n\r\n",
			                            boundary, "file", MimeTypeUtil.GetMIMEType (fileName));
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

		public SigningStatus GetSigningStatus (PackageId packageId, string signerId, string documentId)
		{
			String path = template.UrlFor( UrlTemplate.SIGNING_STATUS_PATH )
				.Replace( "{packageId}", packageId.Id )
				.Replace( "{signerId}", !String.IsNullOrEmpty(signerId) ? signerId : "" )
				.Replace( "{documentId}", !String.IsNullOrEmpty(documentId) ? documentId : "" )
				.Build();

			try {
                string response = restClient.Get(path);
				JsonTextReader reader = new JsonTextReader(new StringReader(response));

				//Loop 'till we get to the status value
				while (reader.Read () && reader.TokenType != JsonToken.String)
				{
				}

				return SigningStatusUtility.FromString(reader.Value.ToString ());
			} catch (Exception e) {
				throw new EslException ("Could not get signing status. Exception: " + e.Message);
			}
		}

		internal IList<Role> GetRoles( PackageId packageId ) {
			String path = template.UrlFor( UrlTemplate.ROLE_PATH )
				.Replace( "{packageId}", packageId.Id )
				.Build();

			Result<Role> response = null;
			try
			{
				string stringResponse = restClient.Get(path);
				response = JsonConvert.DeserializeObject<Result<Role>> (stringResponse, settings);
			} catch( Exception e ) {
				throw new EslException("Unable to retrieve role list for package with id " + packageId.Id + ".  " + e.Message);
			}
			return response.Results;
		}

		public void DeletePackage(PackageId id)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH).Replace ("{packageId}", id.Id).Build ();

			try
			{
                restClient.Delete(path);
			}
			catch (Exception e)
			{
				throw new EslException ("Could not delete package. Exception: " + e.Message);	
			}
		}

		private Role FindRoleForGroup( PackageId packageId, string groupId )
		{
			IList<Role> roles = GetRoles(packageId);

			foreach (Role role in roles)
			{
				if (role.Signers.Count > 0)
				{
					Silanis.ESL.API.Signer signer = role.Signers[0];
					if (signer.Group != null && signer.Group.Id.Equals(groupId))
					{
						return role;
					}
				}
			}
			return null;
		}

		public void NotifySigner( PackageId packageId, GroupId groupId )
		{
			Role role = FindRoleForGroup(packageId, groupId.Id);
			NotifySigner(packageId, role.Id);
		}

		public void NotifySigner (PackageId packageId, string roleId )
		{

			string path = template.UrlFor(UrlTemplate.NOTIFY_ROLE_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", roleId)
				.Build();

			try
			{
				restClient.Post(path, "");
			}
			catch (Exception e)
			{
				throw new EslException("Unable to send email notification.  " + e.Message);
			}
		}

		public void NotifySigner (PackageId packageId, string signerEmail, string message)
		{
			string path = template.UrlFor (UrlTemplate.NOTIFICATIONS_PATH).Replace ("{packageId}", packageId.Id).Build ();
			StringWriter sw = new StringWriter ();

			using (JsonWriter json = new JsonTextWriter(sw))
			{
				json.Formatting = Formatting.Indented;
				json.WriteStartObject ();
				json.WritePropertyName ("email");
				json.WriteValue (signerEmail);
				json.WritePropertyName ("message");
				json.WriteValue (message);
				json.WriteEndObject ();
			}

			try
			{
                restClient.Post(path, sw.ToString());
			}
			catch (Exception e)
			{
				throw new EslException ("Could not send email notification to signer. Exception: " + e.Message);	
			}
		}

		public Page<DocumentPackage> GetPackages (DocumentPackageStatus status, PageRequest request)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_LIST_PATH)
				.Replace ("{status}", status.ToString ())
				.Replace ("{from}", request.From.ToString ())
				.Replace ("{to}", request.To.ToString())
				.Build();

			try
			{
                string response = restClient.Get(path);
				Silanis.ESL.API.Result<Silanis.ESL.API.Package> results = JsonConvert.DeserializeObject<Silanis.ESL.API.Result<Silanis.ESL.API.Package>> (response, settings);

				return ConvertToPage(results, request);
			}
			catch (Exception e)
			{
				Console.WriteLine (e.StackTrace);
				throw new EslException ("Could not get package list. Exception: " + e.Message);	
			}
		}

        public Page<DocumentPackage> GetTemplates(PageRequest request) {
            string path = template.UrlFor (UrlTemplate.TEMPLATE_LIST_PATH)
                    .Replace ("{from}", request.From.ToString ())
                    .Replace ("{to}", request.To.ToString())
                    .Build();

            try 
            {
                string response = restClient.Get(path);
                Silanis.ESL.API.Result<Silanis.ESL.API.Package> results = JsonConvert.DeserializeObject<Silanis.ESL.API.Result<Silanis.ESL.API.Package>> (response, settings);

                return ConvertToPage(results, request);
            }
            catch (Exception e)
            {
                Console.WriteLine (e.StackTrace);
                throw new EslException ("Could not get template list. Exception: " + e.Message); 
            }
        }

		private Page<DocumentPackage> ConvertToPage (Silanis.ESL.API.Result<Silanis.ESL.API.Package> results, PageRequest request)
		{
			IList<DocumentPackage> converted = new List<DocumentPackage> ();

			foreach (Silanis.ESL.API.Package package in results.Results)
			{
				DocumentPackage dp = new PackageBuilder (package).Build ();

				converted.Add (dp);
			}

			return new Page<DocumentPackage> (converted, results.Count, request);
		}

		private string GenerateBoundary ()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[16];
			var random = new Random ();

			for (int i = 0; i < stringChars.Length; i++) {
				stringChars [i] = chars [random.Next (chars.Length)];
			}

			return new String (stringChars);
		}

	}
}