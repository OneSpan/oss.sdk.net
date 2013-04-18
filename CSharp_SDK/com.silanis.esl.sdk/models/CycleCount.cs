//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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