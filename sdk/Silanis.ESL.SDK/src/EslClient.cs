using System;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.SDK.Services;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.API;
using Newtonsoft.Json;
using System.Reflection;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Silanis.ESL.SDK.Builder.Internal;

namespace Silanis.ESL.SDK
{
    /// <summary>
    /// The EslClient acts as a eSignLive client.
    /// The EslClient has access to service classes that help create packages and retrieve resources from the client's account.
    /// </summary>
	public class EslClient
	{

		private string baseUrl;
        private string webpageUrl;
		private PackageService packageService;
        private ReportService reportService;
		private SessionService sessionService;
		private FieldSummaryService fieldSummaryService;
		private AuditService auditService;
        private EventNotificationService eventNotificationService;
        private CustomFieldService customFieldService;
        private GroupService groupService;
		private AccountService accountService;
        private ApprovalService approvalService;
		private Services.ReminderService reminderService;
        private TemplateService templateService;
		private AuthenticationTokenService authenticationTokenService;    
		private AttachmentRequirementService attachmentRequirementService;
        private LayoutService layoutService;
        private QRCodeService qrCodeService;
        private AuthenticationService authenticationService;
        private SystemService systemService;
        private SignatureImageService signatureImageService;
        private SigningService signingService;
        private SignerVerificationService signerVerificationService;
        private SigningStyleService signingStyleService;

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
            SetBaseUrl (baseUrl);
            SetWebpageUrl (baseUrl);

            configureJsonSerializationSettings();

            RestClient restClient = new RestClient(apiKey);
            init(restClient, apiKey);
        }

        /// <summary>
        /// EslClient constructor.
        /// Initiates service classes that can be used by the client.
        /// </summary>
        /// <param name="apiKey">The client's api key.</param>
        /// <param name="baseUrl">The staging or production url.</param>
        public EslClient (string apiKey, string baseUrl, string webpageUrl)
            : this (apiKey, baseUrl, webpageUrl, false) {}

        public EslClient (string apiKey, string baseUrl, string webpageUrl, Boolean allowAllSSLCertificates)
        {
            Asserts.NotEmptyOrNull (apiKey, "apiKey");
            Asserts.NotEmptyOrNull (baseUrl, "baseUrl");
            Asserts.NotEmptyOrNull (webpageUrl, "webpageUrl");
            SetBaseUrl (baseUrl);
            this.webpageUrl = AppendServicePath (webpageUrl);

            configureJsonSerializationSettings();

            RestClient restClient = new RestClient(apiKey, allowAllSSLCertificates);
            init(restClient, apiKey);
        }

        public EslClient (string apiKey, string baseUrl, Boolean allowAllSSLCertificates)
            : this (apiKey, baseUrl, allowAllSSLCertificates, null) {}

        public EslClient (string apiKey, string baseUrl, ProxyConfiguration proxyConfiguration)
            : this (apiKey, baseUrl, false, proxyConfiguration) {}

        public EslClient (string apiKey, string baseUrl, bool allowAllSSLCertificates, ProxyConfiguration proxyConfiguration)
            : this (apiKey, baseUrl, allowAllSSLCertificates, proxyConfiguration, new Dictionary<string, string> ()) {}
        
        public EslClient (string apiKey, string baseUrl, bool allowAllSSLCertificates, ProxyConfiguration proxyConfiguration, IDictionary<string, string> headers)
        {
            Asserts.NotEmptyOrNull (apiKey, "apiKey");
            Asserts.NotEmptyOrNull (baseUrl, "baseUrl");
            SetBaseUrl (baseUrl);
            SetWebpageUrl (baseUrl);

            configureJsonSerializationSettings ();

            RestClient restClient = new RestClient (apiKey, allowAllSSLCertificates, proxyConfiguration, headers);
            init (restClient, apiKey);
        }

        private void init(RestClient restClient, String apiKey)
        {
            packageService = new PackageService(restClient, this.baseUrl, jsonSerializerSettings);
            reportService = new ReportService(restClient, this.baseUrl, jsonSerializerSettings);
            systemService = new SystemService(restClient, this.baseUrl, jsonSerializerSettings);
            signingService = new SigningService(restClient, this.baseUrl, jsonSerializerSettings);
            signingStyleService = new SigningStyleService (restClient, this.baseUrl, jsonSerializerSettings);
            signerVerificationService = new SignerVerificationService(restClient, this.baseUrl, jsonSerializerSettings);
            signatureImageService = new SignatureImageService(restClient, this.baseUrl, jsonSerializerSettings);
            sessionService = new SessionService(apiKey, this.baseUrl);
            fieldSummaryService = new FieldSummaryService(new FieldSummaryApiClient(apiKey, this.baseUrl));
            auditService = new AuditService(apiKey, this.baseUrl);
            eventNotificationService = new EventNotificationService(new EventNotificationApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            customFieldService = new CustomFieldService( new CustomFieldApiClient(restClient, this.baseUrl, jsonSerializerSettings) );
            groupService = new GroupService(new GroupApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            accountService = new AccountService(new AccountApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            approvalService = new ApprovalService(new ApprovalApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            reminderService = new ReminderService(new ReminderApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            templateService = new TemplateService(new TemplateApiClient(restClient, this.baseUrl, jsonSerializerSettings), packageService);
            authenticationTokenService = new AuthenticationTokenService(restClient, this.baseUrl); 
            attachmentRequirementService = new AttachmentRequirementService(restClient, this.baseUrl, jsonSerializerSettings);
            layoutService = new LayoutService(new LayoutApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            qrCodeService = new QRCodeService(new QRCodeApiClient(restClient, this.baseUrl, jsonSerializerSettings));
            authenticationService = new AuthenticationService(this.webpageUrl);
        }

        private void configureJsonSerializationSettings()
        {
            jsonSerializerSettings = new JsonSerializerSettings ();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jsonSerializerSettings.Converters.Add( new CultureInfoJsonCreationConverter() );
        }

        private void SetBaseUrl(string baseUrl) 
        {
            this.baseUrl = baseUrl;
            this.baseUrl = AppendServicePath (this.baseUrl);
        }

        private void SetWebpageUrl(string baseUrl) 
        {
            webpageUrl = baseUrl;
            if (webpageUrl.EndsWith("/api")) 
            {
                webpageUrl = webpageUrl.Replace("/api", "");
            }
            webpageUrl = AppendServicePath (webpageUrl);
        }
            
		private string AppendServicePath(string baseUrl)
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

        /**
        * Facilitates access to the service that could be used to add signing style
        *
        * @return  the signing style service
        */
        public SigningStyleService GetSigningStyleService ()
        {
            return signingStyleService;
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
            ValidateSignatures(package);
            if (!IsSdkVersionSetInPackageData(package))
            {
                SetSdkVersionInPackageData(package);
            }
        
			Silanis.ESL.API.Package packageToCreate = new DocumentPackageConverter(package).ToAPIPackage();
			PackageId id = packageService.CreatePackage (packageToCreate);
            try 
            {
                UploadDocuments (id, package.Documents);
            }
            catch (Exception e) 
            {
                packageService.DeletePackage (id);
                throw new EslException ("Could not create a new package." + " Exception: " + e.Message, e);
            }
            return id;
		}

        public PackageId CreatePackageOneStep(DocumentPackage package)
        {
            ValidateSignatures(package);
            if (!IsSdkVersionSetInPackageData(package))
            {
                SetSdkVersionInPackageData(package);
            }

            Silanis.ESL.API.Package packageToCreate = new DocumentPackageConverter(package).ToAPIPackage();
            foreach(Silanis.ESL.SDK.Document document in package.Documents){
                packageToCreate.AddDocument(new DocumentConverter(document).ToAPIDocument(packageToCreate));
            }
            PackageId id = packageService.CreatePackageOneStep (packageToCreate, package.Documents);

            return id;
        }

        public void SignDocument(PackageId packageId, string documentName) 
        {
            SignDocument (packageId, documentName, new CapturedSignature (""));
        }

        public void SignDocument (PackageId packageId, string documentName, CapturedSignature capturedSignature)
        {
            Silanis.ESL.API.Package package = packageService.GetPackage (packageId);
            foreach (Silanis.ESL.API.Document document in package.Documents) {
                if (document.Name.Equals (documentName)) {
                    document.Approvals.Clear ();
                    SignedDocument signedDocument = signingService.ConvertToSignedDocument (document);
                    signedDocument.Handdrawn =capturedSignature.Handdrawn;
                    signingService.SignDocument (packageId, signedDocument);
                }
            }
        }

        public void SignDocuments(PackageId packageId) 
        {
            SignDocuments (packageId, new CapturedSignature (""));
        }

        public void SignDocuments (PackageId packageId, CapturedSignature capturedSignature)
        {
            SignedDocuments signedDocuments = new SignedDocuments ();
            signedDocuments.Handdrawn = capturedSignature.Handdrawn;
            Package package = packageService.GetPackage (packageId);
            foreach (Silanis.ESL.API.Document document in package.Documents) {
                document.Approvals.Clear ();
                signedDocuments.AddDocument (document);
            }
            signingService.SignDocuments (packageId, signedDocuments);
        }

        public void SignDocuments(PackageId packageId, string signerId) 
        {
            SignDocuments (packageId, signerId, new CapturedSignature (""));
        }

        public void SignDocuments (PackageId packageId, string signerId, CapturedSignature capturedSignature)
        {
            string bulkSigningKey = "Bulk Signing on behalf of";

            IDictionary<string, string> signerSessionFields = new Dictionary<string, string> ();
            signerSessionFields.Add (bulkSigningKey, signerId);
            string signerAuthenticationToken = authenticationTokenService.CreateSignerAuthenticationToken (packageId, signerId, signerSessionFields);

            string signerSessionId = authenticationService.GetSessionIdForSignerAuthenticationToken (signerAuthenticationToken);

            SignedDocuments signedDocuments = new SignedDocuments ();
            signedDocuments.Handdrawn = capturedSignature.Handdrawn;
            Package package = packageService.GetPackage (packageId);
            foreach (Silanis.ESL.API.Document document in package.Documents) {
                document.Approvals.Clear ();
                signedDocuments.AddDocument (document);
            }
            signingService.SignDocuments (packageId, signedDocuments, signerSessionId);
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
			return templateService.CreateTemplateFromPackage( originalPackageId, new DocumentPackageConverter(delta).ToAPIPackage() );
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
            ValidateSignatures(delta);
            SetNewSignersIndexIfRoleWorkflowEnabled(templateId, delta);
			return templateService.CreatePackageFromTemplate( templateId, new DocumentPackageConverter(delta).ToAPIPackage() );
        }

        private void SetNewSignersIndexIfRoleWorkflowEnabled (PackageId templateId, DocumentPackage documentPackage) 
        {
            DocumentPackage template = new DocumentPackageConverter(packageService.GetPackage(templateId)).ToSDKPackage();
            if (CheckSignerOrdering(template)) 
            {
                int firstSignerIndex = GetMaxSigningOrder(template, documentPackage) + 1;
                foreach (Signer signer in documentPackage.Signers) 
                {
                    Signer templatePlaceholder = template.GetPlaceholder(signer.Id);
                    if (templatePlaceholder != null) 
                    {
                        signer.SigningOrder = templatePlaceholder.SigningOrder;
                    }

                    if (signer.SigningOrder <= 0) 
                    {
                        signer.SigningOrder = firstSignerIndex;
                        firstSignerIndex++;
                    }
                }
            }
        }

        private int GetMaxSigningOrder(DocumentPackage template, DocumentPackage documentPackage) 
        {
            List<Signer> signers = new List<Signer>();
            signers.AddRange(documentPackage.Signers);
            signers.AddRange(template.Signers);
            int maxSigningOrder = 0;
            foreach (Signer signer in signers) 
            {
                if (signer.SigningOrder > maxSigningOrder) 
                {
                    maxSigningOrder = signer.SigningOrder;
                }
            }
            return maxSigningOrder;
        }

        private void ValidateSignatures(DocumentPackage documentPackage) 
        {
            foreach(Document document in documentPackage.Documents) 
            {
                ValidateMixingSignatureAndAcceptance(document);
            }
        }

        private void ValidateMixingSignatureAndAcceptance(Document document) 
        {
            if(CheckAcceptanceSignatureStyle(document)) {
                foreach(Signature signature in document.Signatures) {
                    if (signature.Style != SignatureStyle.ACCEPTANCE )
                        throw new EslException("It is not allowed to use acceptance signature styles and other signature styles together in one document.", null);
                }
            }
        }

        private bool CheckAcceptanceSignatureStyle(Document document) 
        {
            foreach (Signature signature in document.Signatures) {
                if (signature.Style == SignatureStyle.ACCEPTANCE)
                    return true;
            }
            return false;
        }

        private bool CheckSignerOrdering(DocumentPackage template)
        {
            List<Signer> signers = new List<Signer>();
            signers.AddRange(template.Signers);
            signers.AddRange(template.Placeholders);
            foreach (Signer signer in signers) 
            {
                if (signer.SigningOrder > 0) 
                {
                    return true;
                }
            }
            return false;
        }

		public PackageId CreateTemplate(DocumentPackage template)
		{
			PackageId templateId = templateService.CreateTemplate(new DocumentPackageConverter(template).ToAPIPackage());

			foreach (Document document in template.Documents)
			{
                UploadDocument(document, templateId);
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

        public byte[] DownloadOriginalDocument(PackageId packageId, string documentId)
        {
            return packageService.DownloadOriginalDocument(packageId, documentId);
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

            DocumentPackage documentPackage = new DocumentPackageConverter(package).ToSDKPackage();
            return documentPackage;
		}

        public void UpdatePackage(Silanis.ESL.SDK.PackageId packageId, DocumentPackage documentPackage)
        {
            packageService.UpdatePackage( packageId, new DocumentPackageConverter(documentPackage).ToAPIPackage() );
        }

        public void ChangePackageStatusToDraft(PackageId packageId) 
        {
            packageService.ChangePackageStatusToDraft(packageId);
        }

        public void ConfigureDocumentVisibility(PackageId packageId, DocumentVisibility visibility) 
        {
            packageService.ConfigureDocumentVisibility(packageId, visibility);
        }

        public DocumentVisibility getDocumentVisibility(PackageId packageId) 
        {
            return packageService.GetDocumentVisibility(packageId);
        }

        public IList<Document> GetDocuments( PackageId packageId, string signerId ) 
        {
            return packageService.GetDocuments(packageId, signerId);
        }

        public IList<Signer> GetSigners( PackageId packageId, string documentId ) 
        {
            return packageService.GetSigners(packageId, documentId);
        }
        
		public SigningStatus GetSigningStatus (PackageId packageId, string signerId, string documentId)
		{
			return packageService.GetSigningStatus (packageId, signerId, documentId);
		}

        [Obsolete("Please use UploadDocument(Document document, PackageId packageId ) instead of this method.")]
		public Document UploadDocument(Document document, DocumentPackage documentPackage ) 
        {
			return UploadDocument( document.FileName, document.Content, document, documentPackage.Id );
		}

        public Document UploadDocument(Document document, PackageId packageId ) 
        {
            return UploadDocument( document.FileName, document.Content, document, packageId );
        }

        public IList<Document> UploadDocuments(PackageId packageId, params Document[] documents) 
        {
            return UploadDocuments( packageId, new List<Document>(documents));
        }

        public IList<Document> UploadDocuments(PackageId packageId, IList<Document> documents) 
        {
            if (documents.Count == 0)
            {
                return new List<Document>();
            }
            else
            {
                return packageService.UploadDocuments( packageId, documents );
            }
        }

        [Obsolete("Please use UploadDocument(string fileName, byte[] fileContent, Document document, PackageId packageId) instead of this method.")]
		public Document UploadDocument(string fileName, byte[] fileContent, Document document, DocumentPackage documentPackage)
        {
            return UploadDocument(fileName, fileContent, document, documentPackage.Id);
        }

        public Document UploadDocument(string fileName, byte[] fileContent, Document document, PackageId packageId)
        {
            return packageService.UploadDocument(packageId, fileName, fileContent, document);
        }

        public void UploadAttachment(PackageId packageId, string attachmentId, string filename, byte[] fileBytes, string signerId) {
            string signerSessionFieldKey = "Upload Attachment on behalf of";

            IDictionary<string, string> signerSessionFields = new Dictionary<string, string>();
            signerSessionFields.Add(signerSessionFieldKey, signerId);
            string signerAuthenticationToken = authenticationTokenService.CreateSignerAuthenticationToken(packageId, signerId, signerSessionFields);
            string signerSessionId = authenticationService.GetSessionIdForSignerAuthenticationToken(signerAuthenticationToken);

            attachmentRequirementService.UploadAttachment(packageId, attachmentId, filename, fileBytes, signerSessionId);
        }

        public void CreateSignerVerification(Silanis.ESL.SDK.PackageId packageId, String roleId, SignerVerification signerVerification)
        {
            Silanis.ESL.API.Verification verification = new SignerVerificationConverter(signerVerification).ToAPISignerVerification(); 
            signerVerificationService.CreateSignerVerification( packageId, roleId, verification);
        }

        public SignerVerification GetSignerVerification (PackageId id, string roleId)
        {
            Silanis.ESL.API.Verification verification = signerVerificationService.GetSignerVerification (id, roleId);

            SignerVerification signerVerification = new SignerVerificationConverter(verification).ToSDKSignerVerification();
            return signerVerification;
        }

        public void UpdateSignerVerification(Silanis.ESL.SDK.PackageId packageId, String roleId, SignerVerification signerVerification)
        {
            Silanis.ESL.API.Verification verification = new SignerVerificationConverter(signerVerification).ToAPISignerVerification(); 
            signerVerificationService.UpdateSignerVerification( packageId, roleId, verification);
        }

        public void DeleteSignerVerification(Silanis.ESL.SDK.PackageId packageId, String roleId)
        {
            signerVerificationService.DeleteSignerVerification( packageId, roleId);
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

        public ReportService ReportService {
            get {
                return this.reportService;
            }
        }

        public SignatureImageService SignatureImageService {
            get {
                return this.signatureImageService;
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

        public ApprovalService ApprovalService
        {
            get
            {
                return approvalService;
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
        
        public string CurrentVersion
        {
            get
            {
                return VersionUtil.getVersion();
            }
        }   

		public AttachmentRequirementService AttachmentRequirementService
		{
			get
			{
				return attachmentRequirementService;
			}
		}

        public LayoutService LayoutService
        {
            get
            {
                return layoutService;
            }
        }

        public QRCodeService QrCodeService
        {
            get
            {
                return qrCodeService;
            }
        }

        public SystemService SystemService
        {
            get
            {
                return systemService;
            }
        }

        public SigningService SigningService
        {
            get
            {
                return signingService;
            }
        }

        public SignerVerificationService SignerVerificationService
        {
            get
            {
                return signerVerificationService;
            }
        }
	}
}	
