using System;
using Silanis.ESL.SDK.Internal;

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
        /// <param name="apiKey">The client's api key.</param>
        /// <param name="baseUrl">The staging or production url.</param>
		public EslClient (string apiKey, string baseUrl)
		{
			Asserts.NotEmptyOrNull (apiKey, "apiKey");
			Asserts.NotEmptyOrNull (baseUrl, "baseUrl");
			this.apiToken = apiKey;
			this.baseUrl = AppendServicePath (baseUrl);
			packageService = new PackageService (apiKey, this.baseUrl);
			sessionService = new SessionService (apiKey, this.baseUrl);
			fieldSummaryService = new FieldSummaryService (apiKey, this.baseUrl);
			auditService = new AuditService (apiKey, this.baseUrl);
		}

		private String AppendServicePath(string baseUrl)
		{
			if (baseUrl.EndsWith ("/")) 
			{
				baseUrl = baseUrl.Remove (baseUrl.Length - 1);
			}

			if (!baseUrl.EndsWith ("/aws/rest/services")) 
			{
				baseUrl += "/aws/rest/services";
			}

			return baseUrl;
		}

		public PackageId CreatePackage (DocumentPackage package)
		{
			Package packageToCreate = package.ToAPIPackage ();
			PackageId id = packageService.CreatePackage (packageToCreate);

			return id;
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