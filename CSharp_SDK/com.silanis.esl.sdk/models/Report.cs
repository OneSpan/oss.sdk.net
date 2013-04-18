//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Report
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("from")]
    public Nullable<DateTime> From
    {
                get; set;
        }
    
        
    [JsonProperty("to")]
    public Nullable<DateTime> To
    {
                get; set;
        }
    
    
}