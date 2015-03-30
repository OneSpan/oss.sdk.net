using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DelegationReport
    {
        private IList<DelegationEventReport> delegationEventReports = new List<DelegationEventReport>();

        public IList<DelegationEventReport> DelegationEventReports
        {
            get
            {
                return delegationEventReports;
            }
            set{ delegationEventReports = value;}
        }
        public void AddDelegationEventReport(DelegationEventReport value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            delegationEventReports.Add(value);
        }

        public Nullable<DateTime> From
        {
            get; set;
        }

        public Nullable<DateTime> To
        {
            get; set;
        }
    }
}

