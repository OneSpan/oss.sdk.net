//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Delivery
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("download")]
    public Boolean Download
    {
                get; set;
        }
    
        
    [JsonProperty("email")]
    public Boolean Email
    {
                get; set;
        }
    
        
    [JsonProperty("provider")]
    public Boolean Provider
    {
                get; set;
        }
    
    
}