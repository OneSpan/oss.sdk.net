//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	internal class Features
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("attachments")]
    public Boolean Attachments
    {
                get; set;
        }
    
		    
    [JsonProperty("customFields")]
    public Boolean CustomFields
    {
                get; set;
        }
    
		    
    [JsonProperty("encryptDocuments")]
    public Boolean EncryptDocuments
    {
                get; set;
        }
    
		    
    [JsonProperty("evidenceSummary")]
    public Boolean EvidenceSummary
    {
                get; set;
        }
    
		    
    [JsonProperty("fastTrack")]
    public Boolean FastTrack
    {
                get; set;
        }
    
		    
    [JsonProperty("forceLogin")]
    public Boolean ForceLogin
    {
                get; set;
        }
    
		    
    [JsonProperty("groups")]
    public Boolean Groups
    {
                get; set;
        }
    
		    
    [JsonProperty("tamperSealEvidence")]
    public Boolean TamperSealEvidence
    {
                get; set;
        }
    
		
	
	}
}