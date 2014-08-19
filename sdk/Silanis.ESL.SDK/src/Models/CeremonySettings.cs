//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	internal class CeremonySettings
	{
		
		// Fields
		private IList<String> _optOutReasons = new List<String>();
		
		// Accessors
		    
    [JsonProperty("declineButton")]
    public Nullable<Boolean> DeclineButton
    {
                get; set;
        }
    
		    
    [JsonProperty("disableDownloadForUncompletedPackage")]
    public Nullable<Boolean> DisableDownloadForUncompletedPackage
    {
                get; set;
        }
    
		    
    [JsonProperty("disableFirstInPersonAffidavit")]
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
    
		    
    [JsonProperty("documentToolbarOptions")]
    public DocumentToolbarOptions DocumentToolbarOptions
    {
                get; set;
        }
    
		    
    [JsonProperty("events")]
    public CeremonyEvents Events
    {
                get; set;
        }
    
		    
    [JsonProperty("handOver")]
    public Link HandOver
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
    
		    
    [JsonProperty("layout")]
    public LayoutOptions Layout
    {
                get; set;
        }
    
		    
    [JsonProperty("maxAuthFailsAllowed")]
    public Nullable<Int32> MaxAuthFailsAllowed
    {
                get; set;
        }
    
		    
    [JsonProperty("optOutButton")]
    public Nullable<Boolean> OptOutButton
    {
                get; set;
        }
    
		    
    [JsonProperty("optOutReasons")]
    public IList<String> OptOutReasons
    {
                get
        {
            return _optOutReasons;
        }
        }
        public CeremonySettings AddOptOutReason(String value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _optOutReasons.Add(value);
        return this;
    }
    
		    
    [JsonProperty("style")]
    public LayoutStyle Style
    {
                get; set;
        }
    
		
	
	}
}