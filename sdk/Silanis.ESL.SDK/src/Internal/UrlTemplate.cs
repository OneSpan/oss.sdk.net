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

//		@QueryParam("query") final String query,
//		@QueryParam("search") final String search,
//		@QueryParam("from") int fromRec,
//		@QueryParam("to") int toRec,
//		@QueryParam("sort") String orderBy,
//		@QueryParam("dir") String orderDir


		// Package Service
		public static readonly string PACKAGE_PATH = "/packages";
		public static readonly string PACKAGE_LIST_PATH = "/packages?query={status}&from={from}&to={to}";
        public static readonly string TEMPLATE_LIST_PATH = "/packages?type=TEMPLATE&from={from}&to={to}";
		public static readonly string PACKAGE_ID_PATH = "/packages/{packageId}";
		public static readonly string DOCUMENT_PATH = "/packages/{packageId}/documents";
		public static readonly string DOCUMENT_ID_PATH = "/packages/{packageId}/documents/{documentId}";
		public static readonly string ROLE_PATH = "/packages/{packageId}/roles";
		public static readonly string ROLE_ID_PATH = "/packages/{packageId}/roles/{roleId}";
		public static readonly string NOTIFICATIONS_PATH = "/packages/{packageId}/notifications";
		public static readonly string PDF_PATH = "/packages/{packageId}/documents/{documentId}/pdf";
		public static readonly string ZIP_PATH = "/packages/{packageId}/documents/zip";
		public static readonly string EVIDENCE_SUMMARY_PATH = "/packages/{packageId}/evidence/summary";
		public static readonly string SIGNING_STATUS_PATH = "/packages/{packageId}/signingStatus?signer={signerId}&document={documentId}";
		public static readonly string NOTIFY_ROLE_PATH = "/packages/{packageId}/roles/{roleId}/notifications";
        public static readonly string CLONE_PACKAGE_PATH = "/packages/{packageId}/clone";


        // Event Notification Service
        public static readonly string CALLBACK_PATH = "/callback";

		// Audit Service
		public static readonly string AUDIT_PATH = "/packages/{packageId}/audit";

		// Field Summary Service
		public static readonly string FIELD_SUMMARY_PATH = "/packages/{packageId}/fieldSummary";

		// Session Service
		public static readonly string SESSION_PATH = "/sessions?package={packageId}&signer={signerId}";
		public static readonly string SENDER_SESSION_PATH = "/sessions";

        // Custom Field Service
        public static readonly string ACCOUNT_CUSTOMFIELD_PATH = "/account/customfields";
        public static readonly string USER_CUSTOMFIELD_PATH = "/user/customfields";

        // Groups Service
        public static readonly string GROUPS_PATH = "/groups";
        public static readonly string GROUPS_ID_PATH = "/groups/{groupId}";
        public static readonly string GROUPS_MEMBER_PATH = "/groups/{groupId}/members";

        // Account Service
        public static readonly string ACCOUNT_INVITE_MEMBER_PATH = "/account/senders";

		// Reminder Service
		public static readonly string REMINDER_PATH = "/packages/{packageId}/reminders";

		public static readonly string AUTHENTICATION_TOKEN_PATH = "/authenticationTokens";

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

