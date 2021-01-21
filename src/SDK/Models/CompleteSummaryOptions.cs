using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class CompleteSummaryOptions
	{
		    
    [JsonProperty("from")]
    public Nullable<Boolean> From
    {
                get; set;
        }

    [JsonProperty("title")]
    public Nullable<Boolean> Title
    {
	    get; set;
    }
    
    [JsonProperty("message")]
    public Nullable<Boolean> Message
    {
	    get; set;
    }
    
    [JsonProperty("download")]
    public Nullable<Boolean> Download
    {
	    get; set;
    }
    
    [JsonProperty("review")]
    public Nullable<Boolean> Review
    {
	    get; set;
    }
    
    [JsonProperty("continue")]
    public Nullable<Boolean> Continue
    {
	    get; set;
    }
	
	}
}