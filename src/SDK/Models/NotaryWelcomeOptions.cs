using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class NotaryWelcomeOptions
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
    
    [JsonProperty("recipientActionRequired")]
    public Nullable<Boolean> RecipientActionRequired
    {
	    get; set;
    }
    
    [JsonProperty("notaryTag")]
    public Nullable<Boolean> NotaryTag
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

	}
}