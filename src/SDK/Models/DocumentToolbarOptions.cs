//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class DocumentToolbarOptions
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("downloadButton")]
    public Nullable<Boolean> DownloadButton
    {
                get; set;
        }
    
		
	
	}
}