//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class CcExpiration
	{
		
    [JsonProperty("month")]
    public Nullable<Int32> Month { get; set; }
    
    [JsonProperty("year")]
    public Nullable<Int32> Year { get; set; }
	}
}