//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	internal class DelegationReport
	{
		
		// Fields
		private IList<DelegationEventReport> _delegationEventReports = new List<DelegationEventReport>();
		
		// Accessors
		    
    [JsonProperty("delegationEventReports")]
    public IList<DelegationEventReport> DelegationEventReports
    {
                get
        {
            return _delegationEventReports;
        }
        }
        public DelegationReport AddDelegationEventReport(DelegationEventReport value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _delegationEventReports.Add(value);
        return this;
    }
    
		    
    [JsonProperty("from")]
    public Nullable<DateTime> From
    {
                get; set;
        }
    
		    
    [JsonProperty("to")]
    public Nullable<DateTime> To
    {
                get; set;
        }
    
		
	
	}
}