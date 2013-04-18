//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class CcExpiration
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("month")]
    public Int32 Month
    {
                get; set;
        }
    
        
    [JsonProperty("year")]
    public Int32 Year
    {
                get; set;
        }
    
    
}