//
using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	
	
	public class Account
	{
		
		// Fields
		private IList<CustomField> _customFields = new List<CustomField>();
		private IList<License> _licenses = new List<License>();
		
		// Accessors
		
    public Company Company
    {
                get; set;
        }
    
    
    public Nullable<DateTime> Created
    {
                get; set;
        }
    
    
    public IList<CustomField> CustomFields
    {
                get
        {
            return _customFields;
        }
        }
        public Account AddCustomField(CustomField value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _customFields.Add(value);
        return this;
    }
    
        
    public IDictionary<string, object> Data
    {
                get; set;
        }
    
    
    public String Id
    {
                get; set;
        }
    
    
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
        
    public String LogoUrl
    {
                get; set;
        }
    
    public String Name
    {
                get; set;
        }
    
    
    public String Owner
    {
                get; set;
        }
    
    public AccountProviders Providers
    {
                get; set;
        }
    
    public Nullable<DateTime> Updated
    {
                get; set;
        }
    
	}
}