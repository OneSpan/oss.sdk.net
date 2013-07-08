using System;

namespace Silanis.ESL.SDK.Internal
{
	/// <summary>
	/// For internal use.
	/// </summary>
	public class UrlTemplate
	{
		private String baseUrl;
		private String path;

		// Package Service
		public static readonly string PACKAGE_PATH = "/packages";
		public static readonly string PACKAGE_ID_PATH = "/packages/{packageId}";
		public static readonly string DOCUMENT_PATH = "/packages/{packageId}/documents";
		public static readonly string DOCUMENT_ID_PATH = "/packages/{packageId}/documents/{documentId}";
		public static readonly string ROLE_PATH = "/packages/{packageId}/roles";
		public static readonly string ROLE_ID_PATH = "/packages/{packageId}/roles/{roleId}";
		public static readonly string PDF_PATH = "/packages/{packageId}/documents/{documentId}/pdf";
		public static readonly string ZIP_PATH = "/packages/{packageId}/documents/zip";
		public static readonly string EVIDENCE_SUMMARY_PATH = "/packages/{packageId}/evidence/summary";
		public static readonly string SIGNING_STATUS_PATH = "/packages/{packageId}/signingStatus?signer={signerId}&document={documentId}";

		// Audit Service
		public static readonly string AUDIT_PATH = "/packages/{packageId}/audit";

		// Field Summary Service
		public static readonly string FIELD_SUMMARY_PATH = "/packages/{packageId}/fieldSummary";

		// Session Service
		public static readonly string SESSION_PATH = "/sessions?package={packageId}&signer={signerId}";

		public UrlTemplate (string baseUrl)
		{
			this.baseUrl = baseUrl;
		}

		public UrlTemplate UrlFor (string path)
		{
			this.path = path;
			return this;
		}

		public UrlTemplate Replace (string pathParams, string value)
		{
			path = path.Replace (pathParams, value);
			return this;
		}

		public string Build ()
		{
			return baseUrl + path;
		}
	}
}

