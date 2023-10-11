//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class FieldValidation
	{
		
		// Fields
		private IList<String> _enum = new List<String>();
		
		// Accessors
		    
    [JsonProperty("enum")]
    public IList<String> Enum
    {
                get
        {
            return _enum;
        }
        }
        public FieldValidation AddEnum(String value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _enum.Add(value);
        return this;
    }
    
		    
    [JsonProperty("errorCode")]
    public Nullable<Int32> ErrorCode
    {
                get; set;
        }
    
		    
    [JsonProperty("errorMessage")]
    public String ErrorMessage
    {
                get; set;
        }
    
		    
    [JsonProperty("maxLength")]
    public Nullable<Int32> MaxLength
    {
                get; set;
        }
    
		    
    [JsonProperty("minLength")]
    public Nullable<Int32> MinLength
    {
                get; set;
        }
    
		    
    [JsonProperty("pattern")]
    public String Pattern
    {
                get; set;
        }
    
		    
    [JsonProperty("required")]
    public Nullable<Boolean> Required
    {
                get; set;
        }


    [JsonProperty ("disabled")]
    public Nullable<Boolean> Disabled
    {
            get; set;
        }
    
    
    [JsonProperty("group")]
    public String Group
    {
	    get; set;
    }
    
    
    [JsonProperty ("minimumRequired")]
    public Nullable<Int32> MinimumRequired
    {
	    get; set;
    }  
    
    [JsonProperty ("maximumRequired")]
    public Nullable<Int32> MaximumRequired
    {
	    get; set;
    }
    
    [JsonProperty("groupTooltip")]
    public String GroupTooltip
    {
	    get; set;
    }

	}
}