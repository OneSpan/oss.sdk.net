using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DownloadReportExample : SDKSample
	{
        public PackageId package2Id;
        public OneSpanSign.Sdk.CompletionReport sdkCompletionReportForSenderDraft, sdkCompletionReportForSenderSent, sdkCompletionReportDraft, sdkCompletionReportSent;
        public OneSpanSign.Sdk.UsageReport sdkUsageReport;
        public OneSpanSign.Sdk.DelegationReport sdkDelegationReportForAccountWithoutDate;
        public OneSpanSign.Sdk.DelegationReport sdkDelegationReportForAccount;
        public OneSpanSign.Sdk.DelegationReport sdkDelegationReportForSender;

        public string csvCompletionReportForSenderDraft, csvCompletionReportForSenderSent, csvCompletionReportDraft, csvCompletionReportSent;
        public string csvUsageReport;
        public string csvDelegationReportForAccountWithoutDate;
        public string csvDelegationReportForAccount;
        public string csvDelegationReportForSender;


		public static void Main(string[] args)
		{
            new DownloadReportExample().Run();
		}

		override public void Execute()
		{
			DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
					.DescribedAs("This is a package created using the eSignLive SDK")
					.ExpiresOn(DateTime.Now.AddMonths(100))
					.WithEmailMessage("This message should be delivered to all signers")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
						.WithCustomId("Client1")
						.WithFirstName("John")
						.WithLastName("Smith")
						.WithTitle("Managing Director")
						.WithCompany("Acme Inc.")
					)
					.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
						.FromStream(fileStream1, DocumentType.PDF)
						.WithSignature(SignatureBuilder.SignatureFor(email1)
							.OnPage(0)
							.WithField(FieldBuilder.CheckBox()
								.OnPage(0)
								.AtPosition(400, 200)
								.WithValue(FieldBuilder.CHECKBOX_CHECKED)
							)
							.AtPosition(100, 100)
						)
					)
					.Build();

			packageId = ossClient.CreatePackage(superDuperPackage);

            DocumentPackage superDuperPackage2 =
                PackageBuilder.NewPackageNamed("DownloadReportForSent: " + DateTime.Now)
                    .DescribedAs("This is a package created using the eSignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("Client1")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithTitle("Managing Director")
                                .WithCompany("Acme Inc.")
                                )
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .FromStream(fileStream2, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .WithField(FieldBuilder.CheckBox()
                               .OnPage(0)
                               .AtPosition(400, 200)
                               .WithValue(FieldBuilder.CHECKBOX_CHECKED)
                               )
                                   .AtPosition(100, 100)
                                   )
                                  )
                    .Build();

            package2Id = ossClient.CreateAndSendPackage(superDuperPackage2);

			// Date and time range to get completion report.
            DateTime from = DateTime.Now.AddMinutes(-5);
            DateTime to = DateTime.Now.AddMinutes(5);

            // Download the completion report for a sender
            sdkCompletionReportForSenderDraft = ossClient.ReportService.DownloadCompletionReport(DocumentPackageStatus.DRAFT, senderUID, from, to);
            csvCompletionReportForSenderDraft = ossClient.ReportService.DownloadCompletionReportAsCSV(DocumentPackageStatus.DRAFT, senderUID, from, to);

            sdkCompletionReportForSenderSent = ossClient.ReportService.DownloadCompletionReport(DocumentPackageStatus.SENT, senderUID, from, to);
            csvCompletionReportForSenderSent = ossClient.ReportService.DownloadCompletionReportAsCSV(DocumentPackageStatus.SENT, senderUID, from, to);

            // Download the completion report for all senders
            sdkCompletionReportDraft = ossClient.ReportService.DownloadCompletionReport(DocumentPackageStatus.DRAFT, from, to);
            csvCompletionReportDraft = ossClient.ReportService.DownloadCompletionReportAsCSV(DocumentPackageStatus.DRAFT, from, to);

            sdkCompletionReportSent = ossClient.ReportService.DownloadCompletionReport(DocumentPackageStatus.SENT, from, to);
            csvCompletionReportSent = ossClient.ReportService.DownloadCompletionReportAsCSV(DocumentPackageStatus.SENT, from, to);

            // Download the usage report
            sdkUsageReport = ossClient.ReportService.DownloadUsageReport(from, to);
            csvUsageReport = ossClient.ReportService.DownloadUsageReportAsCSV(from, to);

            // Download the delegation report for a sender
            sdkDelegationReportForAccountWithoutDate = ossClient.ReportService.DownloadDelegationReport();
            csvDelegationReportForAccountWithoutDate = ossClient.ReportService.DownloadDelegationReportAsCSV();

            sdkDelegationReportForAccount = ossClient.ReportService.DownloadDelegationReport(from, to);
            csvDelegationReportForAccount = ossClient.ReportService.DownloadDelegationReportAsCSV(from, to);

            sdkDelegationReportForSender = ossClient.ReportService.DownloadDelegationReport(senderUID, from, to);
            csvDelegationReportForSender = ossClient.ReportService.DownloadDelegationReportAsCSV(senderUID, from, to);
		}
    }
}

