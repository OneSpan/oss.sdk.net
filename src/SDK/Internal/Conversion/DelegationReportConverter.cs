using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class DelegationReportConverter
    {
        private OneSpanSign.Sdk.DelegationReport sdkDelegationReport = null;
        private OneSpanSign.API.DelegationReport apiDelegationReport = null;

        public DelegationReportConverter(OneSpanSign.Sdk.DelegationReport sdkDelegationReport)
        {
            this.sdkDelegationReport = sdkDelegationReport;
        }

        internal DelegationReportConverter(OneSpanSign.API.DelegationReport apiDelegationReport)
        {
            this.apiDelegationReport = apiDelegationReport;
        }

        internal OneSpanSign.API.DelegationReport ToAPIDelegationReport()
        {
            if (sdkDelegationReport == null)
            {
                return apiDelegationReport;
            }

            OneSpanSign.API.DelegationReport result = new OneSpanSign.API.DelegationReport();

            result.From = sdkDelegationReport.From;
            result.To = sdkDelegationReport.To;

            foreach(KeyValuePair<string, IList<DelegationEventReport>> sdkDelegationEventDictionary in sdkDelegationReport.DelegationEvents) 
            {
                result.DelegationEvents.Add(sdkDelegationEventDictionary.Key, GetAPIDelegationEventList(sdkDelegationEventDictionary.Value));
            }

            return result;
        }

        private IList<OneSpanSign.API.DelegationEventReport> GetAPIDelegationEventList(IList<OneSpanSign.Sdk.DelegationEventReport> sdkDelegationEventList) 
        {
            IList<OneSpanSign.API.DelegationEventReport> apiDelegationEventList = new List<OneSpanSign.API.DelegationEventReport>();
            foreach(OneSpanSign.Sdk.DelegationEventReport sdkDelegationEventReport in sdkDelegationEventList) 
            {
                apiDelegationEventList.Add(new DelegationEventReportConverter(sdkDelegationEventReport).ToAPIDelegationEventReport());
            }
            return apiDelegationEventList;
        }

        public OneSpanSign.Sdk.DelegationReport ToSDKDelegationReport()
        {
            if (apiDelegationReport == null)
            {
                return sdkDelegationReport;
            }

            OneSpanSign.Sdk.DelegationReport result = new OneSpanSign.Sdk.DelegationReport();

            result.From = apiDelegationReport.From;
            result.To = apiDelegationReport.To;

            foreach(KeyValuePair<string, IList<OneSpanSign.API.DelegationEventReport>> apiDelegationEventDictionary in apiDelegationReport.DelegationEvents) 
            {
                result.DelegationEvents.Add(apiDelegationEventDictionary.Key, GetSDKDelegationEventList(apiDelegationEventDictionary.Value));
            }

            return result;
        }

        private IList<OneSpanSign.Sdk.DelegationEventReport> GetSDKDelegationEventList(IList<OneSpanSign.API.DelegationEventReport> apiDelegationEventList) 
        {
            IList<OneSpanSign.Sdk.DelegationEventReport> sdkDelegationEventList = new List<OneSpanSign.Sdk.DelegationEventReport>();
            foreach(OneSpanSign.API.DelegationEventReport apiDelegationEventReport in apiDelegationEventList) 
            {
                sdkDelegationEventList.Add(new DelegationEventReportConverter(apiDelegationEventReport).ToSDKDelegationEventReport());
            }
            return sdkDelegationEventList;
        }
    }
}

