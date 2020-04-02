//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class PackageArtifactsLimits
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("documents")]
    public Nullable<Int32> Documents
    {
                get; set;
        }
    
		    
    [JsonProperty("roles")]
    public Nullable<Int32> Roles
    {
                get; set;
        }
    
		
	
	}
}