using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSharp_SDK
{
	public class PackageService
	{
		private	string apiToken;
		private UrlTemplate template;
		private JsonSerializerSettings settings;

		public PackageService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);

			settings = new JsonSerializerSettings ();
			settings.NullValueHandling = NullValueHandling.Ignore;
		}

		public PackageId CreatePackage (Package package)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_PATH)
				.Build ();
			try {
				string json = JsonConvert.SerializeObject (package, settings);
				byte[] content = Converter.ToBytes (json);
				
				string response = Converter.ToString (HttpMethods.PostHttp (apiToken, path, content));
				
				return JsonConvert.DeserializeObject<PackageId> (response);
			} catch (Exception e) {
				throw new EslException ("Could not create a new package.");
			}

		}

		public void UpdatePackage (PackageId packageId, Package package)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
				.Replace ("{packageId}", packageId.Id)
				.Build ();

			try {
				string json = JsonConvert.SerializeObject (package, settings);
				byte[] content = Converter.ToBytes (json);

				HttpMethods.PutHttp (apiToken, path, content);
			} catch (Exception e) {
				throw new EslException ("Could not update the package.");
			}
		}

		public Package GetPackage (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
				.Replace ("{packageId}", packageId.Id)
				.Build ();

			try {
				string response = Converter.ToString (HttpMethods.GetHttp (apiToken, path));

				return JsonConvert.DeserializeObject<Package> (response, settings);
			} catch (Exception e) {
				throw new EslException ("Could not get package.");
			}
		}

		public void DeleteDocument (PackageId packageId, Document document)
		{
			string path = template.UrlFor (UrlTemplate.DOCUMENT_ID_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{documentId}", "document.Id")
                .Build ();

			try {
				HttpMethods.DeleteHttp (apiToken, path);
			} catch (Exception e) {
				throw new EslException ("Could not delete document from package.");
			}
		}

		public void SendPackage (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.PACKAGE_ID_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Build ();

			try {
				byte[] content = Converter.ToBytes ("{\"status\":\"SENT\"}");

				HttpMethods.PostHttp (apiToken, path, content);
			} catch (Exception e) {
				throw new EslException ("Could not send the package.");
			}
		}

		public List<Role> GetRoles (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.ROLE_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Build ();

			try {
				string response = Converter.ToString (HttpMethods.GetHttp (apiToken, path));
				RoleList roleList = JsonConvert.DeserializeObject<RoleList> (response);

				return roleList.Roles;
			} catch (Exception e) {
				throw new EslException ("Could not get roles.");
			}
		}

		public Role AddRole (PackageId packageId, Role role)
		{
			string path = template.UrlFor (UrlTemplate.ROLE_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Build ();

			try {
				string json = JsonConvert.SerializeObject (role, settings);
				byte[] content = Converter.ToBytes (json);
				string response = Converter.ToString (HttpMethods.PostHttp (apiToken, path, content));

				return JsonConvert.DeserializeObject<Role> (response, settings);
			} catch (Exception e) {
				throw new EslException ("Could not add role.");
			}
		}

		public void DeleteRole (PackageId packageId, Role role)
		{
			string path = template.UrlFor (UrlTemplate.ROLE_ID_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{roleId}", role.Id)
                .Build ();

			try {
				HttpMethods.DeleteHttp (apiToken, path);
			} catch (Exception e) {
				throw new EslException ("Could not delete role.");
			}
		}

		public byte[] DownloadDocument (PackageId packageId, Document document)
		{
			string path = template.UrlFor (UrlTemplate.PDF_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{documentId}", document.Id)
                .Build ();

			try {
				return HttpMethods.GetHttp (apiToken, path);
			} catch (Exception e) {
				throw new EslException ("Could not download the pdf document.");
			}
		}

		public byte[] DownloadZippedDocuments (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.ZIP_PATH)
            .Replace ("{packageId}", packageId.Id)
            .Build ();

			try {
				return HttpMethods.GetHttp (apiToken, path);
			} catch (Exception e) {
				throw new EslException ("Could not download the documents to a zip file.");
			}
		}

		public byte[] DownloadEvidenceSummary (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.EVIDENCE_SUMMARY_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Build ();

			try {
				return HttpMethods.GetHttp (apiToken, path);
			} catch (Exception e) {
				throw new EslException ("Could not download the evidence summary.");
			}
		}

		public void UploadDocument (PackageId packageId, string fileName, byte[] fileBytes, Document document)
		{
			string path = template.UrlFor (UrlTemplate.DOCUMENT_PATH)
				.Replace ("{packageId}", packageId.Id)
					.Build ();

			try {
				string json = JsonConvert.SerializeObject (document, settings);
				byte[] payloadBytes = Converter.ToBytes (json);

				string boundary = GenerateBoundary ();
				byte[] content = createMultipartContent (fileName, fileBytes, payloadBytes, boundary);

				Converter.ToString (HttpMethods.MultipartPostHttp (apiToken, path, content, boundary));
			} catch (Exception e) {
				throw new EslException ("Could not upload document to package.");
			}
		}

		private byte[] createMultipartContent (string fileName, byte[] fileBytes, byte[] payloadBytes, string boundary)
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

