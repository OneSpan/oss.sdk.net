using System;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.SDK.Services;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.API;
using Newtonsoft.Json;
using System.Reflection;

namespace Silanis.ESL.SDK
{
    /// <summary>
    /// The EslClient acts as a E-SignLive client.
    /// The EslClient has access to service classes that help create packages and retrieve resources from the client's account.
    /// </summary>
	public class EslClient
	{

		private string baseUrl;
		private PackageService packageService;
		private SessionService sessionService;
		private FieldSummaryService fieldSummaryService;
		private AuditService auditService;
        private EventNotificationService eventNotificationService;
        private CustomFieldService customFieldService;
        private GroupService groupService;
		private AccountService accountService;
		private Services.ReminderService reminderService;
        private TemplateService templateService;
		private AuthenticationTokenService authenticationTokenService;    
		private AttachmentRequirementService attachmentRequirementService;
        
        private JsonSerializerSettings jsonSerializerSettings;

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
			this.baseUrl = AppendServicePath (baseUrl);

            configureJsonSerializationSettings();

            RestClient restClient = new RestClient(apiKey);
			packageService = new PackageService (restClient, this.baseUrl, jsonSerializerSettings);
			sessionService = new SessionService (apiKey, this.baseUrl);
			fieldSummaryService = new FieldSummaryService (apiKey, this.baseUrl);
			auditService = new AuditService (apiKey, this.baseUrl);

            eventNotificationService = new EventNotificationService(restClient, this.baseUrl, jsonSerializerSettings);
            customFieldService = new CustomFieldService( restClient, this.baseUrl, jsonSerializerSettings );
            groupService = new GroupService(restClient, this.baseUrl, jsonSerializerSettings);
			accountService = new AccountService(restClient, this.baseUrl, jsonSerializerSettings);
			reminderService = new ReminderService(restClient, this.baseUrl, jsonSerializerSettings);
			templateService = new TemplateService(restClient, this.baseUrl, packageService, jsonSerializerSettings);
			authenticationTokenService = new AuthenticationTokenService(restClient, this.baseUrl); 
			attachmentRequirementService = new AttachmentRequirementService(restClient, this.baseUrl, jsonSerializerSettings);
		}
        
        private void configureJsonSerializationSettings()
        {
            jsonSerializerSettings = new JsonSerializerSettings ();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jsonSerializerSettings.Converters.Add( new CultureInfoJsonCreationConverter() );
        }

        

		private String AppendServicePath(string baseUrl)
		{
			if (baseUrl.EndsWith ("/")) 
			{
				baseUrl = baseUrl.Remove (baseUrl.Length - 1);
			}

			return baseUrl;
		}

        /**
         * Facilitates access to the service that could be used to add custom field
         *
         * @return  the custom field service
         */
        public CustomFieldService GetCustomFieldService() {
            return customFieldService;
        }

        internal bool IsSdkVersionSetInPackageData(DocumentPackage package)
        {
            if (package.Attributes != null && package.Attributes.Contents.ContainsKey("sdk"))
            {
                return true;
            }            
            return false;
        }

        internal void SetSdkVersionInPackageData(DocumentPackage package)
        {
            if (package.Attributes == null)
            {
                package.Attributes = new DocumentPackageAttributes();
            }
            package.Attributes.Append( "sdk", ".NET v" + CurrentVersion );
        }

		public PackageId CreatePackage(DocumentPackage package)
        {
            if (!IsSdkVersionSetInPackageData(package))
            {
                SetSdkVersionInPackageData(package);
            }
        
			Silanis.ESL.API.Package packageToCreate = package.ToAPIPackage ();
			PackageId id = packageService.CreatePackage (packageToCreate);
            DocumentPackage retrievedPackage = GetPackage(id);

			foreach (Document document in package.Documents.Values)
			{
                UploadDocument(document, retrievedPackage);
			}

			return id;
		}

		public PackageId CreateAndSendPackage( DocumentPackage package ) 
		{
			PackageId packageId = CreatePackage (package);
			SendPackage (packageId);
			return packageId;
		}

		public void SendPackage (PackageId id)
		{
			packageService.SendPackage (id);
		}

        public PackageId CreateTemplateFromPackage(PackageId originalPackageId, DocumentPackage delta)
        {
            return templateService.CreateTemplateFromPackage( originalPackageId, delta.ToAPIPackage() );
        }

        public PackageId CreateTemplateFromPackage(PackageId originalPackageId, string templateName)
        {
            DocumentPackage sdkPackage = PackageBuilder.NewPackageNamed( templateName ).Build();
            return CreateTemplateFromPackage( originalPackageId, sdkPackage );
        }
        
        public PackageId CreatePackageFromTemplate(PackageId templateId, string packageName)
        {
            DocumentPackage sdkPackage = PackageBuilder.NewPackageNamed( packageName ).Build();
            return CreatePackageFromTemplate( templateId, sdkPackage );
        }
        
        public PackageId CreatePackageFromTemplate(PackageId templateId, DocumentPackage delta)
        {
            return templateService.CreatePackageFromTemplate( templateId, delta.ToAPIPackage() );
        }

		public PackageId CreateTemplate(DocumentPackage template)
		{
			PackageId templateId = templateService.CreateTemplate(template.ToAPIPackage());
			DocumentPackage createdTemplate = GetPackage(templateId);

			foreach (Document document in template.Documents.Values)
			{
				UploadDocument(document, createdTemplate);
			}

			return templateId;
		}

		[Obsolete("Call AuthenticationTokenService.CreateSenderAuthenticationToken() instead.")]
		public SessionToken CreateSenderSessionToken()
		{
			return sessionService.CreateSenderSessionToken();
		}

		[Obsolete("Call AuthenticationTokenService.CreateSignerAuthenticationToken() instead.")]
		public SessionToken CreateSessionToken(PackageId packageId, string signerId)
		{
			return CreateSignerSessionToken(packageId, signerId); 
		}

		public SessionToken CreateSignerSessionToken(PackageId packageId, string signerId)
		{
			return sessionService.CreateSignerSessionToken (packageId, signerId);
		}

        //use createUserAuthenticationToken which returns a string for the token
        [Obsolete("Call AuthenticationTokenService.CreateUserAuthenticationToken() instead.")]
		public AuthenticationToken CreateAuthenticationToken()
		{
			return authenticationTokenService.CreateAuthenticationToken();
		}

		public byte[] DownloadDocument (PackageId packageId, string documentId)
		{
			return packageService.DownloadDocument (packageId, documentId);
		}

		public byte[] DownloadEvidenceSummary (PackageId packageId)
		{
			return packageService.DownloadEvidenceSummary (packageId);
		}

		public byte[] DownloadZippedDocuments (PackageId packageId)
		{
			return packageService.DownloadZippedDocuments (packageId);
		}

		public DocumentPackage GetPackage (PackageId id)
		{
			Silanis.ESL.API.Package package = packageService.GetPackage (id);

			return new PackageBuilder (package).Build ();
		}

        public void UpdatePackage(Silanis.ESL.SDK.PackageId packageId, DocumentPackage sentSettings)
        {
            packageService.UpdatePackage( packageId, sentSettings.ToAPIPackage() );
        }
        
		public SigningStatus GetSigningStatus (PackageId packageId, string signerId, string documentId)
		{
			return packageService.GetSigningStatus (packageId, signerId, documentId);
		}

		public Document UploadDocument(Document document, DocumentPackage documentPackage ) {
			return UploadDocument( document.FileName, document.Content, document, documentPackage );
		}

		public Document UploadDocument(String fileName, byte[] fileContent, Document document, DocumentPackage documentPackage)
        {
			Document uploaded = packageService.UploadDocument(documentPackage, fileName, fileContent, document);

			documentPackage.Documents[uploaded.Name] = uploaded;
			return uploaded;
        }

		public Document UploadDocument( Document document, PackageId packageId ) {
			DocumentPackage documentPackage = GetPackage(packageId);

			return UploadDocument(document, documentPackage);
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
		        
        public TemplateService TemplateService
		{
			get
			{
				return templateService;
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

        public EventNotificationService EventNotificationService
        {
            get
            {
                return eventNotificationService;
            }
        }

        public GroupService GroupService
        {
            get
            {
                return groupService;
            }
        }

		public AccountService AccountService
		{
			get
			{
				return accountService;
			}
		}

		public ReminderService ReminderService
		{
			get
			{
				return reminderService;
			}
		}
        
        public AuthenticationTokenService AuthenticationTokenService
        {
            get
            {
                return authenticationTokenService;
            }
        }
        
        public Version CurrentVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }   

		public AttachmentRequirementService AttachmentRequirementService
		{
			get
			{
				return attachmentRequirementService;
			}
		}
	}
}	
