//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class Account
	{
		
		// Fields
		private IList<License> _licenses = new List<License>();
		
		// Accessors
		    
    [JsonProperty("company")]
    public Company Company
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
    
		    
    [JsonProperty("id")]
    public String Id
    {
                get; set;
        }
    
		    
    [JsonProperty("licenses")]
    public IList<License> Licenses
    {
                get
        {
            return _licenses;
        }
        }
        public Account AddLicense(License value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _licenses.Add(value);
        return this;
    }
    
		    
    [JsonProperty("name")]
    public String Name
    {
                get; set;
        }
    
		    
    [JsonProperty("owner")]
    public String Owner
    {
                get; set;
        }
    
		    
    [JsonProperty("providers")]
    public AccountProviders Providers
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