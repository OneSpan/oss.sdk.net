using System;

namespace OneSpanSign.Sdk
{
    public class DelegationEventReportConverter
    {
        private OneSpanSign.Sdk.DelegationEventReport sdkDelegationEventReport = null;
        private OneSpanSign.API.DelegationEventReport apiDelegationEventReport = null;

        public DelegationEventReportConverter(OneSpanSign.Sdk.DelegationEventReport sdkDelegationEventReport)
        {
            this.sdkDelegationEventReport = sdkDelegationEventReport;
        }

        internal DelegationEventReportConverter(OneSpanSign.API.DelegationEventReport apiDelegationEventReport)
        {
            this.apiDelegationEventReport = apiDelegationEventReport;
        }

        internal OneSpanSign.API.DelegationEventReport ToAPIDelegationEventReport()
        {
            if (sdkDelegationEventReport == null)
            {
                return apiDelegationEventReport;
            }

            OneSpanSign.API.DelegationEventReport result = new OneSpanSign.API.DelegationEventReport();

            result.EventDate = sdkDelegationEventReport.EventDate;
            result.EventDescription = sdkDelegationEventReport.EventDescription;
            result.EventType = sdkDelegationEventReport.EventType;
            result.EventUser = sdkDelegationEventReport.EventUser;

            return result;
        }

        public OneSpanSign.Sdk.DelegationEventReport ToSDKDelegationEventReport()
        {
            if (apiDelegationEventReport == null)
            {
                return sdkDelegationEventReport;
            }

            OneSpanSign.Sdk.DelegationEventReport result = new OneSpanSign.Sdk.DelegationEventReport();

            result.EventDate = apiDelegationEventReport.EventDate;
            result.EventDescription = apiDelegationEventReport.EventDescription;
            result.EventType = apiDelegationEventReport.EventType;
            result.EventUser = apiDelegationEventReport.EventUser;

            return result;
        }
    }
}

