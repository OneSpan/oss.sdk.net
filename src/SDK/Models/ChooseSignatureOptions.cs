using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class ChooseSignatureOptions
	{
		
	[JsonProperty("allowStyling")]
    public Nullable<Boolean> AllowStyling
    {
	    get; set;
    }
    
    [JsonProperty("allowDrawing")]
    public Nullable<Boolean> AllowDrawing
    {
	    get; set;
    }
    
    [JsonProperty("allowUploading")]
    public Nullable<Boolean> AllowUploading
    {
	    get; set;
    }
    
    [JsonProperty("allowMobileSigning")]
    public Nullable<Boolean> AllowMobileSigning
    {
	    get; set;
    }
    
    }
}