using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	internal class AccountPackageSettings
	{
		
		[JsonProperty("ada")]
	    public Nullable<Boolean> Ada
	    {
		    get; set;
	    }
	    
	    [JsonProperty("declineButton")]
	    public Nullable<Boolean> DeclineButton
	    {
		    get; set;
	    }
	    
	    [JsonProperty("defaultTimeBasedExpiry")]
	    public Nullable<Boolean> DefaultTimeBasedExpiry
	    {
		    get; set;
	    }
	    
	    [JsonProperty("disableDeclineOther")]
	    public Nullable<Boolean> DisableDeclineOther
	    {
		    get; set;
	    }
	    
	    [JsonProperty("disableDownloadForUncompletedPackage")]
	    public Nullable<Boolean> DisableDownloadForUncompletedPackage
	    {
		    get; set;
	    }
	    
	    [JsonProperty("DisableFirstInPersonAffidavit")]
	    public Nullable<Boolean> DisableFirstInPersonAffidavit
	    {
		    get; set;
	    }
	    
	    [JsonProperty("disableInPersonAffidavit")]
	    public Nullable<Boolean> DisableInPersonAffidavit
	    {
		    get; set;
	    }
	    
	    [JsonProperty("disableSecondInPersonAffidavit")]
	    public Nullable<Boolean> DisableSecondInPersonAffidavit
	    {
		    get; set;
	    }
	    
	    [JsonProperty("enforceCaptureSignature")]
	    public Nullable<Boolean> EnforceCaptureSignature
	    {
		    get; set;
	    }
	    
	    [JsonProperty("extractAcroFields")]
	    public Nullable<Boolean> ExtractAcroFields
	    {
		    get; set;
	    }
	    
	    [JsonProperty("extractTextTags")]
	    public Nullable<Boolean> ExtractTextTags
	    {
		    get; set;
	    }
	    
	    [JsonProperty("globalActionsDownload")]
	    public Nullable<Boolean> GlobalActionsDownload
	    {
		    get; set;
	    }
	    
	    [JsonProperty("globalActionsHideEvidenceSummary")]
	    public Nullable<Boolean> GlobalActionsHideEvidenceSummary
	    {
		    get; set;
	    }
	    
	    [JsonProperty("hideCaptureText")]
	    public Nullable<Boolean> HideCaptureText
	    {
		    get; set;
	    }
	    
	    [JsonProperty("hideLanguageDropdown")]
	    public Nullable<Boolean> HideLanguageDropdown
	    {
		    get; set;
	    }
	    
	    [JsonProperty("hidePackageOwnerInPerson")]
	    public Nullable<Boolean> HidePackageOwnerInPerson
	    {
		    get; set;
	    }
	    
	    [JsonProperty("hideWatermark")]
	    public Nullable<Boolean> HideWatermark
	    {
		    get; set;
	    }
	    
	    [JsonProperty("inPerson")]
	    public Nullable<Boolean> InPerson
	    {
		    get; set;
	    }
	    
	    [JsonProperty("leftMenuExpand")]
	    public Nullable<Boolean> LeftMenuExpand
	    {
		    get; set;
	    }
	    
	    [JsonProperty("optionalNavigation")]
	    public Nullable<Boolean> OptionalNavigation
	    {
		    get; set;
	    }
	    
	    [JsonProperty("showNseHelp")]
	    public Nullable<Boolean> ShowNseHelp
	    {
		    get; set;
	    }
	    
	    [JsonProperty("showNseLogoInIframe")]
	    public Nullable<Boolean> ShowNseLogoInIframe
	    {
		    get; set;
	    }
	    
	    [JsonProperty("showNseOverview")]
	    public Nullable<Boolean> ShowNseOverview
	    {
		    get; set;
	    }
    
	}
}