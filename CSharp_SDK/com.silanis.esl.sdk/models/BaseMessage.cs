//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class BaseMessage
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("content")]
    public String Content
    {
                get; set;
        }
    
    
}