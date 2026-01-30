using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class ChooseSignatureSettings
	{
		    
    [JsonProperty("signature")]
    public ChooseSignatureOptions ChooseSignatureOptions
    {
	    get; set;
    }

	}
}