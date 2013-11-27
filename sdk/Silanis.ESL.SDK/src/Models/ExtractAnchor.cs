//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	internal class ExtractAnchor
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("anchorPoint")]
    public String AnchorPoint
    {
                get; set;
        }
    
		    
    [JsonProperty("characterIndex")]
    public Int32 CharacterIndex
    {
                get; set;
        }
    
		    
    [JsonProperty("height")]
    public Int32 Height
    {
                get; set;
        }
    
		    
    [JsonProperty("index")]
    public Int32 Index
    {
                get; set;
        }
    
		    
    [JsonProperty("leftOffset")]
    public Int32 LeftOffset
    {
                get; set;
        }
    
		    
    [JsonProperty("text")]
    public String Text
    {
                get; set;
        }
    
		    
    [JsonProperty("topOffset")]
    public Int32 TopOffset
    {
                get; set;
        }
    
		    
    [JsonProperty("width")]
    public Int32 Width
    {
                get; set;
        }
    
		
	
	}
}