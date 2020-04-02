using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// Conversion from API to SDK Usage Report.
    /// </summary>
    internal class UsageReportConverter
    {
        private OneSpanSign.Sdk.UsageReport sdkUsageReport = null;
        private OneSpanSign.API.UsageReport apiUsageReport = null;

        internal UsageReportConverter(OneSpanSign.API.UsageReport apiUsageReport)
        {
            this.apiUsageReport = apiUsageReport;
        }

        /// <summary>
        /// Convert from API UsageReport to SDK UsageReport.
        /// </summary>
        /// <returns>The SDK usage report.</returns>
        public OneSpanSign.Sdk.UsageReport ToSDKUsageReport()
        {
            if (apiUsageReport == null)
            {
                return sdkUsageReport;
            }

            IList<OneSpanSign.API.SenderUsageReport> senderUsageReportList = apiUsageReport.Senders;

            if (senderUsageReportList.Count != 0)
            {
                OneSpanSign.Sdk.UsageReport result = new OneSpanSign.Sdk.UsageReport();
                result.From = apiUsageReport.From;
                result.To = apiUsageReport.To;

                OneSpanSign.Sdk.SenderUsageReport sdkSenderUsageReport;
                foreach (OneSpanSign.API.SenderUsageReport apiSenderUsageReport in senderUsageReportList)
                {
                    sdkSenderUsageReport = ToSDKSenderUsageReport(apiSenderUsageReport);
                    result.AddSenderUsageReport(sdkSenderUsageReport);
                }

                return result;
            }

            return sdkUsageReport;
        }

        // Convert from API to SDK SenderUsageReport.
        private OneSpanSign.Sdk.SenderUsageReport ToSDKSenderUsageReport(OneSpanSign.API.SenderUsageReport apiSenderUsageReport)
        {
            OneSpanSign.Sdk.SenderUsageReport sdkSenderUsageReport = new OneSpanSign.Sdk.SenderUsageReport();
            sdkSenderUsageReport.Sender = new SenderConverter(apiSenderUsageReport.Sender).ToSDKSender();

            IDictionary<UsageReportCategory, int> categoryCount = new Dictionary<UsageReportCategory, int>();
            foreach (KeyValuePair<string, object> entry in apiSenderUsageReport.Packages)
            { 

                UsageReportCategory usageReportCategory = UsageReportCategory.valueOf(entry.Key.ToUpper());
                categoryCount.Add(usageReportCategory, Convert.ToInt32(entry.Value));
            }
            sdkSenderUsageReport.CountByUsageReportCategory = categoryCount;

            return sdkSenderUsageReport;
        }

    }
}

