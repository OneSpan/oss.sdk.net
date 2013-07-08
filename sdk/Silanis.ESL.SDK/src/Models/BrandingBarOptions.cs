//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	public class BrandingBarOptions
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