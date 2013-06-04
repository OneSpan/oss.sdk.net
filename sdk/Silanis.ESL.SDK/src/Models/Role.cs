//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.SDK
{
	
	
	public class Role
	{
		
		// Fields
		private IList<Signer> _signers = new List<Signer>();
		
		// Accessors
		    
    [JsonProperty("data")]
    public IDictionary<string, object> Data
    {
                get; set;
        }
    
		    
    [JsonProperty("emailMessage")]
    public BaseMessage EmailMessage
    {
                get; set;
        }
    
		    
    [JsonProperty("id")]
    public String Id
    {
                get; set;
        }
    
		    
    [JsonProperty("index")]
    public Int32 Index
    {
                get; set;
        }
    
		    
    [JsonProperty("locked")]
    public Boolean Locked
    {
                get; set;
        }
    
		    
    [JsonProperty("name")]
    public String Name
    {
                get; set;
        }
    
		    
    [JsonProperty("reassign")]
    public Boolean Reassign
    {
                get; set;
        }
    
		    
    [JsonProperty("signers")]
    public IList<Signer> Signers
    {
                get
        {
            return _signers;
        }
        }
        public Role AddSigner(Signer value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _signers.Add(value);
        return this;
    }
    
		    
    [JsonProperty("type")]
    public RoleType Type
    {
                get; set;
        }
    
		
	
	}
}