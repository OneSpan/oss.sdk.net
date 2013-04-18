//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Credentials
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("email")]
    public String Email
    {
                get; set;
        }
    
        
    [JsonProperty("newPassword")]
    public String NewPassword
    {
                get; set;
        }
    
        
    [JsonProperty("password")]
    public String Password
    {
                get; set;
        }
    
    
}