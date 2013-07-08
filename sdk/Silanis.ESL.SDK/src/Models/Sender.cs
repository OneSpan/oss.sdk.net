//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	public class Sender
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("account")]
    public Account Account
    {
                get; set;
        }
    
		    
    [JsonProperty("activated")]
    public Nullable<DateTime> Activated
    {
                get; set;
        }
    
		    
    [JsonProperty("address")]
    public Address Address
    {
                get; set;
        }
    
		    
    [JsonProperty("company")]
    public String Company
    {
                get; set;
        }
    
		    
    [JsonProperty("created")]
    public Nullable<DateTime> Created
    {
                get; set;
        }
    
		    
    [JsonProperty("data")]
    public IDictionary<string, object> Data
    {
                get; set;
        }
    
		    
    [JsonProperty("email")]
    public String Email
    {
                get; set;
        }
    
		    
    [JsonProperty("external")]
    public External External
    {
                get; set;
        }
    
		    
    [JsonProperty("firstName")]
    public String FirstName
    {
                get; set;
        }
    
		    
    [JsonProperty("id")]
    public String Id
    {
                get; set;
        }
    
		    
    [JsonProperty("lastName")]
    public String LastName
    {
                get; set;
        }
    
		    
    [JsonProperty("locked")]
    public Nullable<DateTime> Locked
    {
                get; set;
        }
    
		    
    [JsonProperty("name")]
    public String Name
    {
                get; set;
        }
    
		    
    [JsonProperty("phone")]
    public String Phone
    {
                get; set;
        }
    
		    
    [JsonProperty("signature")]
    public SignatureStyle Signature
    {
                get; set;
        }
    
		    
    [JsonProperty("status")]
    public SenderStatus Status
    {
                get; set;
        }
    
		    
    [JsonProperty("title")]
    public String Title
    {
                get; set;
        }
    
		    
    [JsonProperty("type")]
    public SenderType Type
    {
                get; set;
        }
    
		    
    [JsonProperty("updated")]
    public Nullable<DateTime> Updated
    {
                get; set;
        }
    
		
	
	}
}