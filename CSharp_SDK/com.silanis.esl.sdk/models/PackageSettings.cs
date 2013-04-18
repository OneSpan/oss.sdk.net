//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class PackageSettings
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("ceremony")]
    public CeremonySettings Ceremony
    {
                get; set;
        }
    
    
}