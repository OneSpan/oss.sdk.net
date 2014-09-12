//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	internal class KnowledgeBasedAuthentication
	{
		
		// Fields
		
		// Accessors

        [JsonProperty("knowledgeBasedAuthenticationStatus")]
        public KnowledgeBasedAuthenticationStatus KnowledgeBasedAuthenticationStatus
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
    
		
	
	}
}