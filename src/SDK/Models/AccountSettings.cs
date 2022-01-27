using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	internal class AccountSettings
	{
		[JsonProperty("accountFeatureSettings")]
		public AccountFeatureSettings AccountFeatureSettings
		{
			get; set;
		}
		
	    [JsonProperty("accountPackageSettings")]
	    public AccountPackageSettings AccountPackageSettings
	    {
		    get; set;
	    }
	    
	}
}