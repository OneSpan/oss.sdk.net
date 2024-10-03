using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Services
{
    public class ReportService
    {
        private JsonSerializerSettings settings;
        private RestClient restClient;
        private string baseUrl;

        public ReportService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.baseUrl = baseUrl;
            this.settings = settings;
        }

        private string BuildCompletionReportUrl(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, String senderId, DateTime from, DateTime to)
        {
            string toDate = DateHelper.dateToIsoUtcFormat(to);
            string fromDate = DateHelper.dateToIsoUtcFormat(from);

            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.COMPLETION_REPORT_PATH)
                .Replace("{from}", fromDate)
                    .Replace("{to}", toDate)
                    .Replace("{status}", new PackageStatusConverter(packageStatus).ToAPIPackageStatus())
                    .Replace("{senderId}", senderId)
                    .Build();
        }

        private string BuildCompletionReportUrl(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, DateTime from, DateTime to)
        {
            string toDate = DateHelper.dateToIsoUtcFormat(to);
            string fromDate = DateHelper.dateToIsoUtcFormat(from);

            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.COMPLETION_REPORT_FOR_ALL_SENDERS_PATH)
                .Replace("{from}", fromDate)
                    .Replace("{to}", toDate)
                    .Replace("{status}", new PackageStatusConverter(packageStatus).ToAPIPackageStatus())
                    .Build();
        }

        private string BuildUsageReportUrl(DateTime from, DateTime to)
        {
            string toDate = DateHelper.dateToIsoUtcFormat(to);
            string fromDate = DateHelper.dateToIsoUtcFormat(from);

            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.USAGE_REPORT_PATH)
                .Replace("{from}", fromDate)
                    .Replace("{to}", toDate)
                    .Build(); 
        }

        private string BuildDelegationReportUrl()
        {
            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DELEGATION_REPORT_PATH)
                    .Build(); 
        }

        private string BuildDelegationReportUrl(DateTime from, DateTime to)
        {
            string toDate = DateHelper.dateToIsoUtcFormat(to);
            string fromDate = DateHelper.dateToIsoUtcFormat(from);

            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DELEGATION_REPORT_PATH).Build() + "?from={from}&to={to}"
                    .Replace("{from}", fromDate)
                    .Replace("{to}", toDate); 
        }

        private string BuildDelegationReportUrl(string senderId, DateTime from, DateTime to)
        {
            string toDate = DateHelper.dateToIsoUtcFormat(to);
            string fromDate = DateHelper.dateToIsoUtcFormat(from);

            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.DELEGATION_REPORT_PATH).Build() + "?senderId={senderId}&from={from}&to={to}"
                .Replace("{senderId}", senderId)
                .Replace("{from}", fromDate)
                .Replace("{to}", toDate); 
        }

        public OneSpanSign.Sdk.CompletionReport DownloadCompletionReport(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, String senderId, DateTime from, DateTime to)
        {
            try
            {
                string path = BuildCompletionReportUrl(packageStatus, senderId, from, to);
                string response = restClient.Get(path);
                OneSpanSign.API.CompletionReport apiCompletionReport = JsonConvert.DeserializeObject<OneSpanSign.API.CompletionReport>(response, settings);
                return new CompletionReportConverter(apiCompletionReport).ToSDKCompletionReport();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the completion report." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the completion report." + " Exception: " + e.Message, e);
            }
        }

        public string DownloadCompletionReportAsCSV(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, String senderId, DateTime from, DateTime to)
        {
            try
            {
                string path = BuildCompletionReportUrl(packageStatus, senderId, from, to);
                string response = restClient.Get(path, "text/csv");
                return response;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the completion report in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the completion report in csv." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.Sdk.CompletionReport DownloadCompletionReport(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, DateTime from, DateTime to)
        {
            try
            {
                string path = BuildCompletionReportUrl(packageStatus, from, to);
                string response = restClient.Get(path);
                OneSpanSign.API.CompletionReport apiCompletionReport = JsonConvert.DeserializeObject<OneSpanSign.API.CompletionReport>(response, settings);
                return new CompletionReportConverter(apiCompletionReport).ToSDKCompletionReport();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the completion report." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the completion report." + " Exception: " + e.Message, e);
            }
        }

        public string DownloadCompletionReportAsCSV(OneSpanSign.Sdk.DocumentPackageStatus packageStatus, DateTime from, DateTime to)
        {
            try
            {
                string path = BuildCompletionReportUrl(packageStatus, from, to);
                string response = restClient.Get(path, "text/csv");
                return response;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the completion report in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the completion report in csv." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.Sdk.UsageReport DownloadUsageReport(DateTime from, DateTime to)
        {
            string path = BuildUsageReportUrl(from, to);

            try
            {
                string response = restClient.Get(path);
                OneSpanSign.API.UsageReport apiUsageReport = JsonConvert.DeserializeObject<OneSpanSign.API.UsageReport>(response, settings);
                return new UsageReportConverter(apiUsageReport).ToSDKUsageReport();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the usage report." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the usage report." + " Exception: " + e.Message, e);
            }
        }

        public string DownloadUsageReportAsCSV(DateTime from, DateTime to)
        {
            string path = BuildUsageReportUrl(from, to);

            try
            {
                string response = restClient.Get(path, "text/csv");
                return response;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the usage report in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the usage report in csv." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.Sdk.DelegationReport DownloadDelegationReport()
        {
            try
            {
                string path = BuildDelegationReportUrl();
                string response = restClient.Get(path);
                OneSpanSign.API.DelegationReport apiDelegationReport = JsonConvert.DeserializeObject<OneSpanSign.API.DelegationReport>(response, settings);
                return new DelegationReportConverter(apiDelegationReport).ToSDKDelegationReport();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the delegation report." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the delegation report." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.Sdk.DelegationReport DownloadDelegationReport(DateTime from, DateTime to)
        {
            try
            {
                string path = BuildDelegationReportUrl(from, to);
                string response = restClient.Get(path);
                OneSpanSign.API.DelegationReport apiDelegationReport = JsonConvert.DeserializeObject<OneSpanSign.API.DelegationReport>(response, settings);
                return new DelegationReportConverter(apiDelegationReport).ToSDKDelegationReport();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the delegation report." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the delegation report." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.Sdk.DelegationReport DownloadDelegationReport(string senderId, DateTime from, DateTime to)
        {
            try
            {
                string path = BuildDelegationReportUrl(senderId, from, to);
                string response = restClient.Get(path);
                OneSpanSign.API.DelegationReport apiDelegationReport = JsonConvert.DeserializeObject<OneSpanSign.API.DelegationReport>(response, settings);
                return new DelegationReportConverter(apiDelegationReport).ToSDKDelegationReport();
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the delegation report." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the delegation report." + " Exception: " + e.Message, e);
            }
        }

        public string DownloadDelegationReportAsCSV()
        {
            try
            {
                string path = BuildDelegationReportUrl();
                string response = restClient.Get(path, "text/csv");
                return response;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the delegation report in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the delegation report in csv." + " Exception: " + e.Message, e);
            }
        }

        public string DownloadDelegationReportAsCSV(DateTime from, DateTime to)
        {
            try
            {
                string path = BuildDelegationReportUrl(from, to);
                string response = restClient.Get(path, "text/csv");
                return response;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the delegation report in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the delegation report in csv." + " Exception: " + e.Message, e);
            }
        }

        public string DownloadDelegationReportAsCSV(string senderId, DateTime from, DateTime to)
        {
            try
            {
                string path = BuildDelegationReportUrl(senderId, from, to);
                string response = restClient.Get(path, "text/csv");
                return response;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not download the delegation report in csv." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not download the delegation report in csv." + " Exception: " + e.Message, e);
            }
        }

    }
}

