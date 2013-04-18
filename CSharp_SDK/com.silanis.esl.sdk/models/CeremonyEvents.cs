//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class CeremonyEvents
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("complete")]
    public CeremonyEventComplete Complete
    {
                get; set;
        }
    
    
}