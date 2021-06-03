using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class CompleteSummaryOptions
	{
		
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
    
    [JsonProperty("documentSection")]
    public Nullable<Boolean> DocumentSection
    {
	    get; set;
    }
    
    [JsonProperty("uploadSection")]
    public Nullable<Boolean> UploadSection
    {
	    get; set;
    }
	
	}
}