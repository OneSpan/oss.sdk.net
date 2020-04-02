//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class PackageSettings
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("ceremony")]
    public CeremonySettings Ceremony
    {
                get; set;
        }
    
		
	
	}
}