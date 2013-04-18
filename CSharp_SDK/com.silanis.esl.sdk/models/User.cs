//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class User
{
    
    // Fields
    
    // Accessors
        
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
    
        
    [JsonProperty("title")]
    public String Title
    {
                get; set;
        }
    
        
    [JsonProperty("updated")]
    public Nullable<DateTime> Updated
    {
                get; set;
        }
    
    
}