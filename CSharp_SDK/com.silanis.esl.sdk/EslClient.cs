using System;

namespace CSharp_SDK
{
	public class EslClient
	{

		private string apiToken;
		private string baseUrl;
		private PackageService packageService;
		private SessionService sessionService;
		private FieldSummaryService fieldSummaryService;
		private AuditService auditService;


		public EslClient (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			this.baseUrl = baseUrl;
			packageService = new PackageService (apiToken, baseUrl);
			sessionService = new SessionService (apiToken, baseUrl);
			fieldSummaryService = new FieldSummaryService (apiToken, baseUrl);
			auditService = new AuditService (apiToken, baseUrl);
		}

		public string ApiToken {
			get {
				return this.apiToken;
			}
		}

		public string BaseUrl {
			get {
				return this.baseUrl;
			}
		}

		public PackageService PackageService {
			get {
				return this.packageService;
			}
		}

		public SessionService SessionService {
			get {
				return this.sessionService;
			}
		}

		public FieldSummaryService FieldSummaryService {
			get {
				return this.fieldSummaryService;
			}
		}

		public AuditService AuditService {
			get {
				return this.auditService;
			}
		}
	}
}	