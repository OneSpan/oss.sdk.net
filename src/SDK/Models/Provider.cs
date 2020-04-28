//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class Provider
	{

		[JsonProperty("data")]
    public IDictionary<string, object> Data { get; set; }
    
		    
    [JsonProperty("id")]
    public String Id { get; set; }
    
		    
    [JsonProperty("name")]
    public String Name { get; set; }
    
		    
    [JsonProperty("provides")]
    public String Provides { get; set; }

	}
}