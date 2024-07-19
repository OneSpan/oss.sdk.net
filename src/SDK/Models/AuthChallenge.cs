//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class AuthChallenge
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("answer")]
    public String Answer
    {
                get; set;
        }
    
		    
    [JsonProperty("maskInput")]
    public Nullable<Boolean> MaskInput
    {
                get; set;
        }
    
		    
    [JsonProperty("question")]
    public String Question
    {
                get; set;
        }
    
    [JsonProperty("challengeType", NullValueHandling = NullValueHandling.Ignore)]
    public String ChallengeType
    {
                get; set;
        }
    	
	
	}
}