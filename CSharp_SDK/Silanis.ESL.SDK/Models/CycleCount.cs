//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class CycleCount
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("count")]
    public Int32 Count
    {
                get; set;
        }
    
		    
    [JsonProperty("cycle")]
    public Cycle Cycle
    {
                get; set;
        }
    
		
	
	}
}