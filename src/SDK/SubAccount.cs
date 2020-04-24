using System;

namespace OneSpanSign.Sdk
{
	
	
	public class SubAccount : Account
	{
		public SubAccount(){
			Language = "en";
			TimezoneId = "GMT";
		}

		
    public String ParentAccountId
    {
                get; set;
        }

    public String Language
    {
                get; set;
        }
    
    public String TimezoneId
    {
                get; set;
        }
	}
}