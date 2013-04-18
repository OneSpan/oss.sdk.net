//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class HeaderOptions
{
    
    // Fields
    
    // Accessors
        
    [JsonProperty("breadcrumbs")]
    public Boolean Breadcrumbs
    {
                get; set;
        }
    
        
    [JsonProperty("feedback")]
    public Boolean Feedback
    {
                get; set;
        }
    
        
    [JsonProperty("globalActions")]
    public GlobalActionsOptions GlobalActions
    {
                get; set;
        }
    
        
    [JsonProperty("globalNavigation")]
    public Boolean GlobalNavigation
    {
                get; set;
        }
    
        
    [JsonProperty("sessionBar")]
    public Boolean SessionBar
    {
                get; set;
        }
    
        
    [JsonProperty("titleBar")]
    public TitleBarOptions TitleBar
    {
                get; set;
        }
    
    
}