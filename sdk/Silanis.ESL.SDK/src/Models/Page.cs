//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	public class Page
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("height")]
    public Double Height
    {
                get; set;
        }
    
		    
    [JsonProperty("id")]
    public String Id
    {
                get; set;
        }
    
		    
    [JsonProperty("index")]
    public Int32 Index
    {
                get; set;
        }
    
		    
    [JsonProperty("left")]
    public Double Left
    {
                get; set;
        }
    
		    
    [JsonProperty("top")]
    public Double Top
    {
                get; set;
        }
    
		    
    [JsonProperty("version")]
    public Int32 Version
    {
                get; set;
        }
    
		    
    [JsonProperty("width")]
    public Double Width
    {
                get; set;
        }
    
		
	
	}
}