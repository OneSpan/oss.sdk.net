//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class LayoutOptions
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("brandingBar")]
    public BrandingBarOptions BrandingBar
    {
                get; set;
        }
    
        
    [JsonProperty("footer")]
    public FooterOptions Footer
    {
                get; set;
        }
    
        
    [JsonProperty("header")]
    public HeaderOptions Header
    {
                get; set;
        }
    
        
    [JsonProperty("iframe")]
    public Boolean Iframe
    {
                get; set;
        }
    
        
    [JsonProperty("navigator")]
    public Boolean Navigator
    {
                get; set;
        }
    
    
}