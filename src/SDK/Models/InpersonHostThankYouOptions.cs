using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class InpersonHostThankYouOptions
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
    
    [JsonProperty("recipientName")]
    public Nullable<Boolean> RecipientName
    {
	    get; set;
    }
    
    [JsonProperty("recipientEmail")]
    public Nullable<Boolean> RecipientEmail
    {
	    get; set;
    }
    
    [JsonProperty("recipientRole")]
    public Nullable<Boolean> RecipientRole
    {
	    get; set;
    }
	
    [JsonProperty("recipientStatus")]
    public Nullable<Boolean> RecipientStatus
    {
	    get; set;
    }
    
    [JsonProperty("downloadButton")]
    public Nullable<Boolean> DownloadButton
    {
	    get; set;
    }
    
    [JsonProperty("reviewDocumentsButton")]
    public Nullable<Boolean> ReviewDocumentsButton
    {
	    get; set;
    }
    
	}
}