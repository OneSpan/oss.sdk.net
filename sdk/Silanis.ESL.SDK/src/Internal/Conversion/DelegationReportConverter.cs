using System;

namespace Silanis.ESL.SDK
{
    public class DelegationReportConverter
    {
        private Silanis.ESL.SDK.DelegationReport sdkDelegationReport = null;
        private Silanis.ESL.API.DelegationReport apiDelegationReport = null;

        public DelegationReportConverter(Silanis.ESL.SDK.DelegationReport sdkDelegationReport)
        {
            this.sdkDelegationReport = sdkDelegationReport;
        }

        internal DelegationReportConverter(Silanis.ESL.API.DelegationReport apiDelegationReport)
        {
            this.apiDelegationReport = apiDelegationReport;
        }

        internal Silanis.ESL.API.DelegationReport ToAPIDelegationReport()
        {
            if (sdkDelegationReport == null)
            {
                return apiDelegationReport;
            }

            Silanis.ESL.API.DelegationReport result = new Silanis.ESL.API.DelegationReport();

            result.From = sdkDelegationReport.From;
            result.To = sdkDelegationReport.To;

            foreach (Silanis.ESL.SDK.DelegationEventReport sdkDelegationEventReport in sdkDelegationReport.DelegationEventReports) 
            {
                result.AddDelegationEventReport (new DelegationEventReportConverter(sdkDelegationEventReport).ToAPIDelegationEventReport());
            }

            return result;
        }

        public Silanis.ESL.SDK.DelegationReport ToSDKDelegationReport()
        {
            if (apiDelegationReport == null)
            {
                return sdkDelegationReport;
            }

            Silanis.ESL.SDK.DelegationReport result = new Silanis.ESL.SDK.DelegationReport();

            result.From = apiDelegationReport.From;
            result.To = apiDelegationReport.To;

            foreach (Silanis.ESL.API.DelegationEventReport apiDelegationEventReport in apiDelegationReport.DelegationEventReports) 
            {
                result.AddDelegationEventReport (new DelegationEventReportConverter(apiDelegationEventReport).ToSDKDelegationEventReport());
            }

            return result;
        }
    }
}

