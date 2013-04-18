//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class AuthChallenge
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("answer")]
    public String Answer
    {
                get; set;
        }
    
        
    [JsonProperty("maskInput")]
    public Boolean MaskInput
    {
                get; set;
        }
    
        
    [JsonProperty("question")]
    public String Question
    {
                get; set;
        }
    
    
}