//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class TitleBarOptions
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("progressBar")]
    public Boolean ProgressBar
    {
                get; set;
        }
    
        
    [JsonProperty("title")]
    public Boolean Title
    {
                get; set;
        }
    
    
}