using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	internal class OverviewOptions
	{
		    
    [JsonProperty("title")]
    public Nullable<Boolean> Title
    {
	    get; set;
    }
    
    [JsonProperty("body")]
    public Nullable<Boolean> Body
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