using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class DelegationEventReport
    {
        public Nullable<DateTime> EventDate { get; set; }

        public string EventDescription { get; set; }

        public string EventType { get; set; }

        public string EventUser { get; set; }
    }
}

