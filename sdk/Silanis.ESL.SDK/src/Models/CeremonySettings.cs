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
    public Boolean DeclineButton
    {
                get; set;
        }
    
		    
    [JsonProperty("disableInPersonAffidavit")]
    public Boolean DisableInPersonAffidavit
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
    public Boolean HideCaptureText
    {
                get; set;
        }
    
		    
    [JsonProperty("hideWatermark")]
    public Boolean HideWatermark
    {
                get; set;
        }
    
		    
    [JsonProperty("inPerson")]
    public Boolean InPerson
    {
                get; set;
        }
    
		    
    [JsonProperty("layout")]
    public LayoutOptions Layout
    {
                get; set;
        }
    
		    
    [JsonProperty("maxAuthFailsAllowed")]
    public Int32 MaxAuthFailsAllowed
    {
                get; set;
        }
    
		    
    [JsonProperty("optOutButton")]
    public Boolean OptOutButton
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