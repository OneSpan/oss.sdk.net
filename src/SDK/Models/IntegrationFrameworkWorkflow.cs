using Newtonsoft.Json;
namespace OneSpanSign.API
{
    internal class IntegrationFrameworkWorkflow
    {
		
		// Fields
		
		// Accessors

        [JsonProperty("name")]
        public string Name
        {
            get; set;
        }
    
        [JsonProperty("connector")]
        public Connector Connector
        {
            get; set;
        }
    	
	
	}
}