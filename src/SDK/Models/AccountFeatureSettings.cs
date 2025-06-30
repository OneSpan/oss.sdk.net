using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	internal class AccountFeatureSettings
	{
		
		[JsonProperty("allowCheckboxConsentApproval")]
	    public Nullable<Boolean> AllowCheckboxConsentApproval
	    {
		    get; set;
	    }
	    
	    [JsonProperty("allowInPersonForAccountSenders")]
	    public Nullable<Boolean> AllowInPersonForAccountSenders
	    {
		    get; set;
	    }
	    
	    [JsonProperty("attachments")]
	    public Nullable<Boolean> Attachments
	    {
		    get; set;
	    }
	    
	    [JsonProperty("conditionalFields")]
	    public Nullable<Boolean> ConditionalFields
	    {
		    get; set;
	    }
	    
	    [JsonProperty("customFields")]
	    public Nullable<Boolean> CustomFields
	    {
		    get; set;
	    }
	    
	    [JsonProperty("delegation")]
	    public Nullable<Boolean> Delegation
	    {
		    get; set;
	    }
	    
	    [JsonProperty("deliverDocumentsByEmail")]
	    public Nullable<Boolean> DeliverDocumentsByEmail
	    {
		    get; set;
	    }
	    
	    [JsonProperty("disableFooter")]
	    public Nullable<Boolean> DisableFooter
	    {
		    get; set;
	    }
	    
	    [JsonProperty("disableInPersonActivationEmail")]
	    public Nullable<Boolean> DisableInPersonActivationEmail
	    {
		    get; set;
	    }
	    
	    [JsonProperty("documentVisibility")]
	    public Nullable<Boolean> DocumentVisibility
	    {
		    get; set;
	    }
	    
	    [JsonProperty("emailDocumentsAndEvidenceSummary")]
	    public Nullable<Boolean> EmailDocumentsAndEvidenceSummary
	    {
		    get; set;
	    }
	    
	    [JsonProperty("enforceAuth")]
	    public Nullable<Boolean> EnforceAuth
	    {
		    get; set;
	    }
	    
	    [JsonProperty("EvidenceSummary")]
	    public Nullable<Boolean> EvidenceSummary
	    {
		    get; set;
	    }
	    
	    [JsonProperty("flattenSignerDocuments")]
	    public Nullable<Boolean> FlattenSignerDocuments
	    {
		    get; set;
	    }
	    
	    [JsonProperty("forceLogin")]
	    public Nullable<Boolean> ForceLogin
	    {
		    get; set;
	    }
	    
	    [JsonProperty("ForceTransactionOwnerLogin")]
	    public Nullable<Boolean> ForceTransactionOwnerLogin
	    {
		    get; set;
	    }
	    
	    [JsonProperty("groups")]
	    public Nullable<Boolean> Groups
	    {
		    get; set;
	    }
	    
	    [JsonProperty("inAppReports")]
	    public Nullable<Boolean> InAppReports
	    {
		    get; set;
	    }
	    
	    [JsonProperty("maskResponse")]
	    public Nullable<Boolean> MaskResponse
	    {
		    get; set;
	    }
	    
	    [JsonProperty("mobileCapture")]
	    public Nullable<Boolean> MobileCapture
	    {
		    get; set;
	    }
	    
	    [JsonProperty("optionalSignature")]
	    public Nullable<Boolean> OptionalSignature
	    {
		    get; set;
	    }
	    
	    [JsonProperty("passwordManagement")]
	    public Nullable<Boolean> PasswordManagement
	    {
		    get; set;
	    }
	    
	    [JsonProperty("PreventConsentRemoval")]
	    public Nullable<Boolean> PreventConsentRemoval
	    {
		    get; set;
	    }
	    
	    [JsonProperty("qnaAuth")]
	    public Nullable<Boolean> QnaAuth
	    {
		    get; set;
	    }
	    
	    [JsonProperty("SendToMobile")]
	    public Nullable<Boolean> SendToMobile
	    {
		    get; set;
	    }
	    
	    [JsonProperty("uploadSignatureImage")]
	    public Nullable<Boolean> UploadSignatureImage
	    {
		    get; set;
	    }
	    
	    [JsonProperty("overrideRecipientsPreferredLanguage")]
	    public Nullable<Boolean> OverrideRecipientsPreferredLanguage
	    {
		    get; set;
	    }
	    
	    [JsonProperty("enableRecipientHistory")]
	    public Nullable<Boolean> EnableRecipientHistory
	    {
		    get; set;
	    }
	    
	    [JsonProperty("allowSignersDownloadEvidenceSummary")]
	    public Nullable<Boolean> AllowSignersDownloadEvidenceSummary
	    {
		    get; set;
	    }
	    
	    [JsonProperty("documentWidget")]
	    public Nullable<Boolean> DocumentWidget
	    {
		    get; set;
	    }
	    
	
	}
}