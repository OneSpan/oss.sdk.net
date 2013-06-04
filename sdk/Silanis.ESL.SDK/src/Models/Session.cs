//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class Session
	{
		
		// Fields
		private IList<String> _packages = new List<String>();
		
		// Accessors
		    
    [JsonProperty("account")]
    public Account Account
    {
                get; set;
        }
    
		    
    [JsonProperty("packages")]
    public IList<String> Packages
    {
                get
        {
            return _packages;
        }
        }
        public Session AddPackage(String value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _packages.Add(value);
        return this;
    }
    
		    
    [JsonProperty("user")]
    public User User
    {
                get; set;
        }
    
		
	
	}
}