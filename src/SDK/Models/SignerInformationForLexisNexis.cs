//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class SignerInformationForLexisNexis
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("city")]
    public String City
    {
                get; set;
        }
    
		    
    [JsonProperty("dateOfBirth")]
    public Nullable<DateTime> DateOfBirth
    {
                get; set;
        }

    [JsonProperty("firstName")]
    public String FirstName
    {
                get; set;
        }
    
    [JsonProperty("lastName")]
    public String LastName
    {
                get; set;
        }
    
		    
    [JsonProperty("socialSecurityNumber")]
    public String SocialSecurityNumber
    {
                get; set;
        }
    
		    
    [JsonProperty("state")]
    public String State
    {
                get; set;
        }
    
		    
    [JsonProperty("flatOrApartmentNumber")]
    public String FlatOrApartmentNumber
    {
                get; set;
    }
    
    [JsonProperty("houseName")]
    public String HouseName
    {
	    get; set;
    }
    
    [JsonProperty("houseNumber")]
    public String HouseNumber
    {
	    get; set;
    }

    [JsonProperty("zip")]
    public String Zip
    {
                get; set;
        }
    
		
	
	}
}