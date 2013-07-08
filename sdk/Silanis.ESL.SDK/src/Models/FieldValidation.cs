//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	public class FieldValidation
	{
		
		// Fields
		private IList<String> _enum = null;
		
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
    public Int32 ErrorCode
    {
                get; set;
        }
    
		    
    [JsonProperty("errorMessage")]
    public String ErrorMessage
    {
                get; set;
        }
    
		    
    [JsonProperty("maxLength")]
    public Int32 MaxLength
    {
                get; set;
        }
    
		    
    [JsonProperty("minLength")]
    public Int32 MinLength
    {
                get; set;
        }
    
		    
    [JsonProperty("pattern")]
    public String Pattern
    {
                get; set;
        }
    
		    
    [JsonProperty("required")]
    public Boolean Required
    {
                get; set;
        }
    
		
	
	}
}