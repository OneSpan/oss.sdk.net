using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class SigningUiOptions
	{
		    
    [JsonProperty("completeSummaryOptions")]
    public CompleteSummaryOptions CompleteSummaryOptions
    {
	    get; set;
    }
    
    [JsonProperty("inpersonWelcomeOptions")]
    public InpersonWelcomeOptions InpersonWelcomeOptions
    {
	    get; set;
    }
    
    [JsonProperty("inpersonHostThankYouOptions")]
    public InpersonHostThankYouOptions InpersonHostThankYouOptions
    {
	    get; set;
    }
    
    [JsonProperty("notaryWelcomeOptions")]
    public NotaryWelcomeOptions NotaryWelcomeOptions
    {
	    get; set;
    }
    
    [JsonProperty("notaryHostThankYouOptions")]
    public NotaryHostThankYouOptions NotaryHostThankYouOptions
    {
	    get; set;
    }
    
    [JsonProperty("overviewOptions")]
    public OverviewOptions OverviewOptions
    {
	    get; set;
    }

	}
}