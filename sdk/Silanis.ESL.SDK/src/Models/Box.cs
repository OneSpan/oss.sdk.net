//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	internal class Box
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("height")]
    public Double Height
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
    
		    
    [JsonProperty("width")]
    public Double Width
    {
                get; set;
        }
    
		
	
	}
}