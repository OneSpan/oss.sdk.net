//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class Field
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("binding")]
    public String Binding
    {
                get; set;
        }
    
		    
    [JsonProperty("data")]
    public IDictionary<string, object> Data
    {
                get; set;
        }
    
		    
    [JsonProperty("extract")]
    public Boolean Extract
    {
                get; set;
        }
    
		    
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
    
		    
    [JsonProperty("left")]
    public Double Left
    {
                get; set;
        }
    
		    
    [JsonProperty("name")]
    public String Name
    {
                get; set;
        }
    
		    
    [JsonProperty("page")]
    public Int32 Page
    {
                get; set;
        }
    
		    
    [JsonProperty("subtype")]
    public FieldSubtype Subtype
    {
                get; set;
        }
    
		    
    [JsonProperty("top")]
    public Double Top
    {
                get; set;
        }
    
		    
    [JsonProperty("type")]
    public FieldType Type
    {
                get; set;
        }
    
		    
    [JsonProperty("validation")]
    public FieldValidation Validation
    {
                get; set;
        }
    
		    
    [JsonProperty("value")]
    public String Value
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