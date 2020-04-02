//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class CeremonyEventComplete
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("dialog")]
    public Nullable<Boolean> Dialog
    {
                get; set;
        }
    
		    
    [JsonProperty("redirect")]
    public String Redirect
    {
                get; set;
        }
    
		
	
	}
}