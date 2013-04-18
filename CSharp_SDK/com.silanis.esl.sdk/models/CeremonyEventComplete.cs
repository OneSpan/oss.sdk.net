//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class CeremonyEventComplete
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("dialog")]
    public Boolean Dialog
    {
                get; set;
        }
    
        
    [JsonProperty("redirect")]
    public String Redirect
    {
                get; set;
        }
    
    
}