using System;
using NUnit.Framework;

namespace SDK.Examples
{
	[TestFixture()]
	public class DownloadCompletionReportExampleTest
    {
		[Test()]
		public void VerifyResult()
		{
			DownloadCompletionReportExample example = new DownloadCompletionReportExample(Props.GetInstance());
			example.Run();

			Silanis.ESL.SDK.CompletionReport completionReport = example.SdkCompletionReport;

			Assert.GreaterOrEqual(completionReport.Senders.Count, 1, "There should be only 1 sender.");
			Assert.GreaterOrEqual(completionReport.Senders[0].Packages.Count, 1, "Number of package completion reports should be greater than 1.");
			Assert.GreaterOrEqual(completionReport.Senders[0].Packages[0].Documents.Count, 1, "Number of document completion reports should be greater than 1.");
			Assert.GreaterOrEqual(completionReport.Senders[0].Packages[0].Signers.Count, 1, "Number of signer completion reports should be greater than 1.");
		}
    }
}
