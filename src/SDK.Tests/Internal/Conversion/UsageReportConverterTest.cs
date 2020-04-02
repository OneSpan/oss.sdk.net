using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class UsageReportConverterTest
    {
        private OneSpanSign.Sdk.UsageReport sdkUsageReport1 = null;
        private OneSpanSign.API.UsageReport apiUsageReport1 = null;
        private UsageReportConverter converter;

        [Test]
        public void ConvertNullAPIToSDK()
        {
            apiUsageReport1 = null;
            converter = new UsageReportConverter(apiUsageReport1);
            Assert.IsNull(converter.ToSDKUsageReport());
        }

        [Test]
        public void ConvertAPIToSDK()
        {
            apiUsageReport1 = CreateTypicalAPIUsageReport();
            sdkUsageReport1 = new UsageReportConverter(apiUsageReport1).ToSDKUsageReport();

            Assert.AreEqual(sdkUsageReport1.From, apiUsageReport1.From);
            Assert.AreEqual(sdkUsageReport1.To, apiUsageReport1.To);

            OneSpanSign.API.Sender apiSender = apiUsageReport1.Senders[0].Sender;
            OneSpanSign.Sdk.Sender sdkSender = sdkUsageReport1.SenderUsageReports[0].Sender;
            Assert.AreEqual(sdkSender.Email, apiSender.Email);
            Assert.AreEqual(sdkSender.FirstName, apiSender.FirstName);
            Assert.AreEqual(sdkSender.LastName, apiSender.LastName);
        
            IDictionary<string, object> apiPackageDictionary = apiUsageReport1.Senders[0].Packages;
            IDictionary<UsageReportCategory, int> sdkPackageDictionary = sdkUsageReport1.SenderUsageReports[0].CountByUsageReportCategory;
            Assert.AreEqual(sdkPackageDictionary[UsageReportCategory.ACTIVE], apiPackageDictionary["active"]);
            Assert.AreEqual(sdkPackageDictionary[UsageReportCategory.DRAFT], apiPackageDictionary["draft"]);
            Assert.AreEqual(sdkPackageDictionary[UsageReportCategory.DECLINED], apiPackageDictionary["declined"]);
        }

        // Create an API Usage Report object
        private OneSpanSign.API.UsageReport CreateTypicalAPIUsageReport()
        {
            OneSpanSign.API.UsageReport usageReport = new OneSpanSign.API.UsageReport();
            usageReport.From = new DateTime(1234);
            usageReport.To = new DateTime(5678);

            OneSpanSign.API.Sender sender = new OneSpanSign.API.Sender();
            sender.Email = "sender@email.com";
            sender.FirstName = "SignerFirstName";
            sender.LastName = "SignerLastName";

            IDictionary<string, object> packages = new Dictionary<string, object>();
            packages.Add("active", 7);
            packages.Add("draft", 3);
            packages.Add("declined", 1);

            OneSpanSign.API.SenderUsageReport senderUsageReport = new OneSpanSign.API.SenderUsageReport();
            senderUsageReport.Sender = sender;
            senderUsageReport.Packages = packages;

            usageReport.AddSender(senderUsageReport);

            return usageReport;
        }
    }
}

