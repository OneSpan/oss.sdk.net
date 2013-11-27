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
    
		    
    [JsonProperty("fasttrack")]
    public Boolean Fasttrack
    {
                get; set;
        }
    
		    
    [JsonProperty("groups")]
    public Boolean Groups
    {
                get; set;
        }
    
		
	
	}
}