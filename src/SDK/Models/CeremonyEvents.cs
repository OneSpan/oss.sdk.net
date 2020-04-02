//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class CeremonyEvents
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("complete")]
    public CeremonyEventComplete Complete
    {
                get; set;
        }
    
		
	
	}
}