//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class BrandingBarOptions
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("logo")]
    public Image Logo
    {
                get; set;
        }
    
		
	
	}
}