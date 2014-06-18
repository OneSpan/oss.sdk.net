using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
	public class DownloadCompletionReportExample : SDKSample
	{
		private string email1;
		private string senderUID;
		private Silanis.ESL.SDK.CompletionReport sdkCompletionReport;
		private Stream fileStream1;
		private PackageId packageId;

		public static void Main(string[] args)
		{
			new DownloadCompletionReportExample(Props.GetInstance()).Run();
		}

		public DownloadCompletionReportExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
		{
		}

		public DownloadCompletionReportExample(string apiUrl, string apiKey, string email1) : base( apiUrl, apiKey )
		{
			this.email1 = email1;
			this.senderUID = Converter.apiKeyToUID(apiKey);
			this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
		}

		public string Email1
		{
			get { return email1; }
		}

		public string SenderUID
		{
			get { return senderUID; }
		}

		public Silanis.ESL.SDK.CompletionReport SdkCompletionReport
		{
			get { return sdkCompletionReport; }
		}

		override public void Execute()
		{
			DocumentPackage superDuperPackage =
				PackageBuilder.NewPackageNamed("DownloadCompletionReport: " + DateTime.Now)
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

			sdkCompletionReport = eslClient.PackageService.DownloadCompletionReport(DocumentPackageStatus.DRAFT, senderUID, from, to);
            string csvCompletionReport = eslClient.PackageService.DownloadCompletionReportAsCSV(DocumentPackageStatus.DRAFT, senderUID, from, to);
		}
    }
}

