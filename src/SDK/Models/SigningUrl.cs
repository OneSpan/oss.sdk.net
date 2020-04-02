//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class SigningUrl
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("packageId")]
    public String PackageId
    {
                get; set;
        }
    
		    
    [JsonProperty("roleId")]
    public String RoleId
    {
                get; set;
        }
    
		    
    [JsonProperty("url")]
    public String Url
    {
                get; set;
        }
    
		
	
	}
}