using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Internal;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DownloadReportExample : SDKSample
	{
        public string email1;
        private string senderUID;
        private Stream fileStream1;

        public Silanis.ESL.SDK.CompletionReport sdkCompletionReportForSender;
        public Silanis.ESL.SDK.CompletionReport sdkCompletionReport;
        public Silanis.ESL.SDK.UsageReport sdkUsageReport;
        public Silanis.ESL.SDK.DelegationReport sdkDelegationReport;
        public string csvCompletionReportForSender;
        public string csvCompletionReport;
        public string csvUsageReport;
        public string csvDelegationReport;


		public static void Main(string[] args)
		{
            new DownloadReportExample(Props.GetInstance()).Run();
		}

        public DownloadReportExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
		{
		}

        public DownloadReportExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
		{
			this.email1 = email1;
			this.senderUID = Converter.apiKeyToUID(apiKey);
			this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
		}

		override public void Execute()
		{
			DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("DownloadCompletionAndUsageReport: " + DateTime.Now)
					.DescribedAs("This is a package created using the e-SignLive SDK")
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

			packageId = eslClient.CreatePackage(superDuperPackage);

			// Date and time range to get completion report.
			DateTime from = DateTime.Today.AddDays(-1);
			DateTime to = DateTime.Now;

            // Download the completion report for a sender
            sdkCompletionReportForSender = eslClient.ReportService.DownloadCompletionReport(DocumentPackageStatus.DRAFT, senderUID, from, to);
            csvCompletionReportForSender = eslClient.ReportService.DownloadCompletionReportAsCSV(DocumentPackageStatus.DRAFT, senderUID, from, to);

            // Display package id and name of packages in DRAFT from sender
            foreach(SenderCompletionReport senderCompletionReport in sdkCompletionReportForSender.Senders) {
                Console.Write("Sender: " + senderCompletionReport.Sender.Email);
                Console.WriteLine(" has " + senderCompletionReport.Packages.Count + " packages in DRAFT");
                foreach (PackageCompletionReport packageCompletionReport in senderCompletionReport.Packages) {
                    Console.WriteLine(packageCompletionReport.Id + " , " + packageCompletionReport.Name + " , Sender : " + eslClient.GetPackage(new PackageId(packageCompletionReport.Id)).SenderInfo.Email);
                }
            }

            // Download the completion report for all senders
            sdkCompletionReport = eslClient.ReportService.DownloadCompletionReport(DocumentPackageStatus.DRAFT, from, to);
            csvCompletionReport = eslClient.ReportService.DownloadCompletionReportAsCSV(DocumentPackageStatus.DRAFT, from, to);

            // Display package id and name of packages in DRAFT from sender
            foreach(SenderCompletionReport senderCompletionReport in sdkCompletionReport.Senders) {
                Console.Write("Sender: " + senderCompletionReport.Sender.Email);
                Console.WriteLine(" has " + senderCompletionReport.Packages.Count + " packages in DRAFT");
                foreach (PackageCompletionReport packageCompletionReport in senderCompletionReport.Packages) {
                    Console.WriteLine(packageCompletionReport.Id + " , " + packageCompletionReport.Name + " , Sender : " + eslClient.GetPackage(new PackageId(packageCompletionReport.Id)).SenderInfo.Email);
                }
            }

            // Download the usage report
            sdkUsageReport = eslClient.ReportService.DownloadUsageReport(from, to);
            csvUsageReport = eslClient.ReportService.DownloadUsageReportAsCSV(from, to);

            // Download the delegation report for a sender
            sdkDelegationReport = eslClient.ReportService.DownloadDelegationReport(senderUID, from, to);
            csvDelegationReport = eslClient.ReportService.DownloadDelegationReportAsCSV(senderUID, from, to);
		}
    }
}

