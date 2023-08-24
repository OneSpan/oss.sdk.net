//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class KnowledgeBasedAuthentication
	{
		
		// Fields
		
		// Accessors

        [JsonProperty("knowledgeBasedAuthenticationStatus")]
        public string KnowledgeBasedAuthenticationStatus
        {
            get; set;
        }

    [JsonProperty("signerInformationForEquifaxCanada")]
    public SignerInformationForEquifaxCanada SignerInformationForEquifaxCanada
    {
                get; set;
        }
    
		    
    [JsonProperty("signerInformationForEquifaxUSA")]
    public SignerInformationForEquifaxUSA SignerInformationForEquifaxUSA
    {
                get; set;
        }
    
    [JsonProperty("signerInformationForLexisNexis")]
    public SignerInformationForLexisNexis SignerInformationForLexisNexis
    {
	    get; set;
    }	
	
	}
}