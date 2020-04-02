using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	/// <summary>
	/// Converter for API CompletionReport to SDK CompletionReport. Contains conversions for all the
	/// sub-completion reports (ex: SenderCompletionReport, PackageCompletionReport, DocumentCompletionReport,
	/// SignerCompletionReport).
	/// </summary>
	internal class CompletionReportConverter
    {
		private OneSpanSign.Sdk.CompletionReport sdkCompletionReport = null;
		private OneSpanSign.API.CompletionReport apiCompletionReport = null;

		public CompletionReportConverter(OneSpanSign.API.CompletionReport apiCompletionReport)
        {
			this.apiCompletionReport = apiCompletionReport;
        }

		/// <summary>
		/// Convert from API CompletionReport to SDK CompletionReport.
		/// </summary>
		/// <returns>The SDK completion report.</returns>
		public OneSpanSign.Sdk.CompletionReport ToSDKCompletionReport()
		{
			if (apiCompletionReport == null)
			{
				return sdkCompletionReport;
			}

			IList<OneSpanSign.API.SenderCompletionReport> senderCompletionReportList = apiCompletionReport.Senders;

			if (senderCompletionReportList.Count != 0)
			{
				OneSpanSign.Sdk.CompletionReport result = new OneSpanSign.Sdk.CompletionReport();
				result.From = apiCompletionReport.From;
				result.To = apiCompletionReport.To;

				OneSpanSign.Sdk.SenderCompletionReport sdkSenderCompletionReport = new OneSpanSign.Sdk.SenderCompletionReport();
				foreach (OneSpanSign.API.SenderCompletionReport apiSenderCompletionReport in senderCompletionReportList)
				{
					sdkSenderCompletionReport = ToSDKSenderCompletionReport(apiSenderCompletionReport);

					IList<OneSpanSign.API.PackageCompletionReport> packageCompletionReportList = apiSenderCompletionReport.Packages;
					OneSpanSign.Sdk.PackageCompletionReport sdkPackageCompletionReport;
					foreach (OneSpanSign.API.PackageCompletionReport apiPackageCompletionReport in packageCompletionReportList)
					{
						sdkPackageCompletionReport = ToSDKPackageCompletionReport(apiPackageCompletionReport);

						IList<OneSpanSign.API.DocumentsCompletionReport> documentCompletionReportList = apiPackageCompletionReport.Documents;
						OneSpanSign.Sdk.DocumentsCompletionReport sdkDocumentsCompletionReport;
						foreach (OneSpanSign.API.DocumentsCompletionReport apiDocumentsCompletionReport in documentCompletionReportList)
						{
							sdkDocumentsCompletionReport = ToSDKDocumentsCompletionReport(apiDocumentsCompletionReport);
							sdkPackageCompletionReport.AddDocument(sdkDocumentsCompletionReport);
						}

						IList<OneSpanSign.API.SignersCompletionReport> signersCompletionReportList = apiPackageCompletionReport.Signers;
						OneSpanSign.Sdk.SignersCompletionReport sdkSignersCompletionReport;
						foreach (OneSpanSign.API.SignersCompletionReport apiSignersCompletionReport in signersCompletionReportList)
						{
							sdkSignersCompletionReport = ToSDKSignersCompletionReport(apiSignersCompletionReport);
							sdkPackageCompletionReport.AddSigner(sdkSignersCompletionReport);
						}

						sdkSenderCompletionReport.AddPackage(sdkPackageCompletionReport);
					}

					result.AddSender(sdkSenderCompletionReport);
				}

				return result;
			}

			return sdkCompletionReport;

		}

		// Convert from API to SDK SenderCompletionReport
		private OneSpanSign.Sdk.SenderCompletionReport ToSDKSenderCompletionReport(OneSpanSign.API.SenderCompletionReport apiSenderCompletionReport)
		{
			OneSpanSign.Sdk.SenderCompletionReport sdkSenderCompletionReport = new OneSpanSign.Sdk.SenderCompletionReport();
			sdkSenderCompletionReport.Sender = new SenderConverter(apiSenderCompletionReport.Sender).ToSDKSender();

			return sdkSenderCompletionReport;
		}

		// Convert from API to SDK PackageCompletionReport
		private OneSpanSign.Sdk.PackageCompletionReport ToSDKPackageCompletionReport(OneSpanSign.API.PackageCompletionReport apiPackageCompletionReport)
		{
			OneSpanSign.Sdk.PackageCompletionReport sdkPackageCompletionReport = new OneSpanSign.Sdk.PackageCompletionReport(apiPackageCompletionReport.Name);
			sdkPackageCompletionReport.Id = apiPackageCompletionReport.Id;
            
			sdkPackageCompletionReport.Created = apiPackageCompletionReport.Created;
			sdkPackageCompletionReport.DocumentPackageStatus = new PackageStatusConverter(apiPackageCompletionReport.Status).ToSDKPackageStatus();
		    sdkPackageCompletionReport.Trashed = apiPackageCompletionReport.Trashed.Value;

			return sdkPackageCompletionReport;
		}

		// Convert from API to SDK DocumentsCompletionReport
		private OneSpanSign.Sdk.DocumentsCompletionReport ToSDKDocumentsCompletionReport(OneSpanSign.API.DocumentsCompletionReport apiDocumentsCompletionReport)
        {
            OneSpanSign.Sdk.DocumentsCompletionReport sdkDocumentCompletionReport = new OneSpanSign.Sdk.DocumentsCompletionReport(apiDocumentsCompletionReport.Name);
            sdkDocumentCompletionReport.Id = apiDocumentsCompletionReport.Id;
            if (apiDocumentsCompletionReport.Completed.HasValue)
            {
                sdkDocumentCompletionReport.Completed = apiDocumentsCompletionReport.Completed.Value;
            }

			if (apiDocumentsCompletionReport.FirstSigned.HasValue)
			{
				sdkDocumentCompletionReport.FirstSigned = apiDocumentsCompletionReport.FirstSigned;
			}

			if (apiDocumentsCompletionReport.LastSigned.HasValue)
			{
				sdkDocumentCompletionReport.LastSigned = apiDocumentsCompletionReport.LastSigned;
			}

			return sdkDocumentCompletionReport;
		}

		// Convert from API to SDK SignersCompletionReport
		private OneSpanSign.Sdk.SignersCompletionReport ToSDKSignersCompletionReport(OneSpanSign.API.SignersCompletionReport apiSignersCompletionReport)
        {
            OneSpanSign.Sdk.SignersCompletionReport sdkSignersCompletionReport = new OneSpanSign.Sdk.SignersCompletionReport(apiSignersCompletionReport.FirstName, apiSignersCompletionReport.LastName);
            sdkSignersCompletionReport.Email = apiSignersCompletionReport.Email;
            sdkSignersCompletionReport.Id = apiSignersCompletionReport.Id;
            
            if (apiSignersCompletionReport.Completed.HasValue)
            {
                sdkSignersCompletionReport.Completed = apiSignersCompletionReport.Completed.Value;
            }

			if (apiSignersCompletionReport.FirstSigned.HasValue)
			{
				sdkSignersCompletionReport.FirstSigned = apiSignersCompletionReport.FirstSigned;
			}

			if (apiSignersCompletionReport.LastSigned.HasValue)
			{
				sdkSignersCompletionReport.LastSigned = apiSignersCompletionReport.LastSigned;
			}

			return sdkSignersCompletionReport;
		}
    }
}

