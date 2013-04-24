//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class Image
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("link")]
    public String Link
    {
                get; set;
        }
    
		    
    [JsonProperty("src")]
    public String Src
    {
                get; set;
        }
    
		
	
	}
}