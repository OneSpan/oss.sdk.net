//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class Price
	{
		
		    
    [JsonProperty("amount")]
    public Nullable<Int32> Amount { get; set; }
    
		    
    [JsonProperty("currency")]
    public Currency Currency { get; set; }
    
	}
}