//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	public class Quota
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("cycle")]
    public Cycle Cycle
    {
                get; set;
        }
    
		    
    [JsonProperty("limit")]
    public Int32 Limit
    {
                get; set;
        }
    
		    
    [JsonProperty("scope")]
    public Scope Scope
    {
                get; set;
        }
    
		    
    [JsonProperty("target")]
    public Target Target
    {
                get; set;
        }
    
		
	
	}
}