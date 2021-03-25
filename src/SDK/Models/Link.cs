//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class Link : Handover
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("autoRedirect")]
    public Nullable<bool> AutoRedirect
    {
                get; set;
        }

    [JsonProperty("parameters")]
    public HashSet<String> Parameters
    {
                get; set;
        }
    
	}
}