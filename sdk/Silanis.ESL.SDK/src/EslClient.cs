using System;

namespace Silanis.ESL.SDK
{
    /// <summary>
    /// The EslClient acts as a E-SignLive client.
    /// The EslClient has access to service classes that help create packages and retrieve resources from the client's account.
    /// </summary>
	public class EslClient
	{

		private string apiToken;
		private string baseUrl;
		private PackageService packageService;
		private SessionService sessionService;
		private FieldSummaryService fieldSummaryService;
		private AuditService auditService;

        /// <summary>
        /// EslClient constructor.
        /// Initiates service classes that can be used by the client.
        /// </summary>
        /// <param name="apiToken">The client's api token.</param>
        /// <param name="baseUrl">The staging or production url.</param>
		public EslClient (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			this.baseUrl = baseUrl;
			packageService = new PackageService (apiToken, baseUrl);
			sessionService = new SessionService (apiToken, baseUrl);
			fieldSummaryService = new FieldSummaryService (apiToken, baseUrl);
			auditService = new AuditService (apiToken, baseUrl);
		}
        
        /// <summary>
        /// ApiToken property
        /// </summary>
		public string ApiToken {
			get {
				return this.apiToken;
			}
		}

        /// <summary>
        /// BaseUrl property
        /// </summary>
		public string BaseUrl {
			get {
				return this.baseUrl;
			}
		}

        /// <summary>
        /// PackageService property
        /// </summary>
		public PackageService PackageService {
			get {
				return this.packageService;
			}
		}

        /// <summary>
        /// SessionService property
        /// </summary>
		public SessionService SessionService {
			get {
				return this.sessionService;
			}
		}

        /// <summary>
        /// FieldSummaryService property
        /// </summary>
		public FieldSummaryService FieldSummaryService {
			get {
				return this.fieldSummaryService;
			}
		}

        /// <summary>
        /// AuditService property
        /// </summary>
		public AuditService AuditService {
			get {
				return this.auditService;
			}
		}
	}
}	