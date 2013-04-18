//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class PackageArtifactsLimits
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("documents")]
    public Int32 Documents
    {
                get; set;
        }
    
        
    [JsonProperty("roles")]
    public Int32 Roles
    {
                get; set;
        }
    
    
}