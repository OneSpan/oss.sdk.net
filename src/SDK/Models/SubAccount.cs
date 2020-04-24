using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
	
	
	internal class SubAccount : Account
	{
		internal SubAccount(){
			Language = "en";
			TimezoneId = "GMT";
		}

		    
    [JsonProperty("parentAccountId")]
    public String ParentAccountId
    {
                get; set;
        }
    
		    
    [JsonProperty("language")]
    public String Language
    {
                get; set;
        }


    [JsonProperty("timezoneId")]
    public String TimezoneId
    {
                get; set;
        }
	}
}