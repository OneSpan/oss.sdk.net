//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class SenderUsageReport
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("packages")]
    public IDictionary<string, object> Packages
    {
                get; set;
        }
    
		    
    [JsonProperty("sender")]
    public Sender Sender
    {
                get; set;
        }
    
		
	
	}
}