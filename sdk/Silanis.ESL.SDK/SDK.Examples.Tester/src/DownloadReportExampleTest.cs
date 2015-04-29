using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
	[TestFixture()]
    public class DownloadReportExampleTest
    {
		[Test()]
		public void VerifyResult()
		{
            DownloadReportExample example = new DownloadReportExample(Props.GetInstance());
			example.Run();

            // Assert correct download of completion report for a sender
            CompletionReport completionReportForSender = example.sdkCompletionReportForSender;
            Assert.AreEqual(completionReportForSender.Senders.Count, 1, "There should be only 1 sender.");
            Assert.GreaterOrEqual(completionReportForSender.Senders[0].Packages.Count, 1, "Number of package completion reports should be greater than 1.");
            Assert.GreaterOrEqual(completionReportForSender.Senders[0].Packages[0].Documents.Count, 1, "Number of document completion reports should be greater than 1.");
            Assert.GreaterOrEqual(completionReportForSender.Senders[0].Packages[0].Signers.Count, 1, "Number of signer completion reports should be greater than 1.");
            Assert.IsNotNull(example.csvCompletionReportForSender);
            Assert.IsNotEmpty(example.csvCompletionReportForSender);

            // Assert correct download of completion report for all senders
            CompletionReport completionReport = example.sdkCompletionReport;
            Assert.GreaterOrEqual(completionReport.Senders.Count, 1, "Number of sender should be greater than 1.");
            Assert.GreaterOrEqual(completionReport.Senders[0].Packages.Count, 0, "Number of package completion reports should be greater than 0.");
            Assert.IsNotNull(example.csvCompletionReport);
            Assert.IsNotEmpty(example.csvCompletionReport);

            // Assert correct download of usage report
            UsageReport usageReport = example.sdkUsageReport;
            Assert.Greater(usageReport.SenderUsageReports.Count, 0, "There should be only 1 sender.");
            Assert.Greater(usageReport.SenderUsageReports[0].CountByUsageReportCategory.Count, 0, "Number of dictionary entries should be greater than 0.");
            Assert.IsTrue(usageReport.SenderUsageReports[0].CountByUsageReportCategory.ContainsKey(UsageReportCategory.DRAFT), "There should be at a draft key in packages map.");
            Assert.Greater(usageReport.SenderUsageReports[0].CountByUsageReportCategory[UsageReportCategory.DRAFT], 0, "Number of drafts should be greater than 0.");
            Assert.IsNotNull(example.csvUsageReport, "Usage report in csv cannot be null.");
            Assert.IsNotEmpty(example.csvUsageReport, "Usage report in csv cannot be empty.");

			// Assert correct download of delegation report
            DelegationReport delegationReportForAccountWithoutDate = example.sdkDelegationReportForAccountWithoutDate;
            Assert.GreaterOrEqual(delegationReportForAccountWithoutDate.DelegationEvents.Count, 0, "Number of DelegationEventReports should be greater than 0.");
            Assert.IsNotNull(example.csvDelegationReportForAccountWithoutDate, "Delegation report in csv cannot be null.");
            Assert.IsNotEmpty(example.csvDelegationReportForAccountWithoutDate, "Delegation report in csv cannot be empty.");

            DelegationReport delegationReportForAccount = example.sdkDelegationReportForAccount;
            Assert.GreaterOrEqual(delegationReportForAccount.DelegationEvents.Count, 0, "Number of DelegationEventReports should be greater than 0.");
            Assert.IsNotNull(example.csvDelegationReportForAccount, "Delegation report in csv cannot be null.");
            Assert.IsNotEmpty(example.csvDelegationReportForAccount, "Delegation report in csv cannot be empty.");

            DelegationReport delegationReportForSender = example.sdkDelegationReportForSender;
            Assert.GreaterOrEqual(delegationReportForAccountWithoutDate.DelegationEvents.Count, 0, "Number of DelegationEventReports should be greater than 0.");
            Assert.IsNotNull(example.csvDelegationReportForSender, "Delegation report in csv cannot be null.");
            Assert.IsNotEmpty(example.csvDelegationReportForSender, "Delegation report in csv cannot be empty.");
		}
    }
}
