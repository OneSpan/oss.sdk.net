using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Internal;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DownloadCompletionAndUsageReportExample : SDKSample
	{
        public string email1;
        private string senderUID;
        private Stream fileStream1;

        public Silanis.ESL.SDK.CompletionReport sdkCompletionReport;
        public Silanis.ESL.SDK.UsageReport sdkUsageReport;
        public string csvCompletionReport;
        public string csvUsageReport;

		public static void Main(string[] args)
		{
            new DownloadCompletionAndUsageReportExample(Props.GetInstance()).Run();
		}

        public DownloadCompletionAndUsageReportExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
		{
		}

        public DownloadCompletionAndUsageReportExample(string apiUrl, string apiKey, string email1) : base( apiUrl, apiKey )
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

            // Download the completion report
			sdkCompletionReport = eslClient.PackageService.DownloadCompletionReport(DocumentPackageStatus.DRAFT, senderUID, from, to);
            csvCompletionReport = eslClient.PackageService.DownloadCompletionReportAsCSV(DocumentPackageStatus.DRAFT, senderUID, from, to);

            // Download the usage report
            sdkUsageReport = eslClient.PackageService.DownloadUsageReport(from, to);
            csvUsageReport = eslClient.PackageService.DownloadUsageReportAsCSV(from, to);

            // Get the number of packages in draft for sender
            IDictionary<UsageReportCategory, int> categoryCounts = sdkUsageReport.SenderUsageReports[0].CountByUsageReportCategory;
            int numOfDrafts = categoryCounts[UsageReportCategory.DRAFT];
		}
    }
}

