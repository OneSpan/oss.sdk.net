using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
	[TestFixture]
    public class CompletionReportConverterTest
    {
		private OneSpanSign.Sdk.CompletionReport sdkCompletionReport1 = null;
		private OneSpanSign.API.CompletionReport apiCompletionReport1 = null;
		private CompletionReportConverter converter;

		[Test]
		public void ConvertNullAPIToSDK()
		{
			apiCompletionReport1 = null;
			converter = new CompletionReportConverter(apiCompletionReport1);
			Assert.IsNull(converter.ToSDKCompletionReport());
		}

		[Test]
		public void ConvertAPIToSDK()
		{
			apiCompletionReport1 = CreateTypicalAPICompletionReport();
			sdkCompletionReport1 = new CompletionReportConverter(apiCompletionReport1).ToSDKCompletionReport();

			Assert.AreEqual(sdkCompletionReport1.From, apiCompletionReport1.From);
			Assert.AreEqual(sdkCompletionReport1.To, apiCompletionReport1.To);

			Assert.AreEqual(sdkCompletionReport1.Senders[0].Sender.Id, apiCompletionReport1.Senders[0].Sender.Id);
			Assert.AreEqual(sdkCompletionReport1.Senders[0].Sender.FirstName, apiCompletionReport1.Senders[0].Sender.FirstName);
			Assert.AreEqual(sdkCompletionReport1.Senders[0].Sender.LastName, apiCompletionReport1.Senders[0].Sender.LastName);

			OneSpanSign.API.PackageCompletionReport apiPackageCompletionReport = apiCompletionReport1.Senders[0].Packages[0];
			OneSpanSign.Sdk.PackageCompletionReport sdkPackageCompletionReport = sdkCompletionReport1.Senders[0].Packages[0];
			Assert.AreEqual(sdkPackageCompletionReport.Id, apiPackageCompletionReport.Id);
			Assert.AreEqual(sdkPackageCompletionReport.Name, apiPackageCompletionReport.Name);
			Assert.AreEqual(sdkPackageCompletionReport.DocumentPackageStatus.ToString(), apiPackageCompletionReport.Status.ToString());
			Assert.AreEqual(sdkPackageCompletionReport.Created, apiPackageCompletionReport.Created);
			Assert.AreEqual(sdkPackageCompletionReport.Documents.Count, 1);
			Assert.AreEqual(sdkPackageCompletionReport.Signers.Count, 1);

			OneSpanSign.API.DocumentsCompletionReport apiDocumentsCompletionReport = apiPackageCompletionReport.Documents[0];
			OneSpanSign.Sdk.DocumentsCompletionReport sdkDocumentsCompletionReport = sdkPackageCompletionReport.Documents[0];
			Assert.AreEqual(sdkDocumentsCompletionReport.Id, apiDocumentsCompletionReport.Id);
			Assert.AreEqual(sdkDocumentsCompletionReport.Name, apiDocumentsCompletionReport.Name);
			Assert.AreEqual(sdkDocumentsCompletionReport.FirstSigned, apiDocumentsCompletionReport.FirstSigned);
			Assert.AreEqual(sdkDocumentsCompletionReport.LastSigned, apiDocumentsCompletionReport.LastSigned);

			OneSpanSign.API.SignersCompletionReport apiSignersCompletionReport = apiPackageCompletionReport.Signers[0];
			OneSpanSign.Sdk.SignersCompletionReport sdkSignersCompletionReport = sdkPackageCompletionReport.Signers[0];
			Assert.AreEqual(sdkSignersCompletionReport.Id, apiSignersCompletionReport.Id);
			Assert.AreEqual(sdkSignersCompletionReport.Email, apiSignersCompletionReport.Email);
			Assert.AreEqual(sdkSignersCompletionReport.FirstName, apiSignersCompletionReport.FirstName);
			Assert.AreEqual(sdkSignersCompletionReport.LastName, apiSignersCompletionReport.LastName);
			Assert.AreEqual(sdkSignersCompletionReport.FirstSigned, apiSignersCompletionReport.FirstSigned);
			Assert.AreEqual(sdkSignersCompletionReport.LastSigned, apiSignersCompletionReport.LastSigned);
		}

		private OneSpanSign.API.CompletionReport CreateTypicalAPICompletionReport()
		{
			OneSpanSign.API.DocumentsCompletionReport documentCompletionReport = new OneSpanSign.API.DocumentsCompletionReport();
			documentCompletionReport.Id = "docId";
			documentCompletionReport.Completed = false;
			documentCompletionReport.Name = "documentName";
			documentCompletionReport.FirstSigned = new DateTime(9);

			OneSpanSign.API.SignersCompletionReport signersCompletionReport = new OneSpanSign.API.SignersCompletionReport();
			signersCompletionReport.Id = "signerId";
			signersCompletionReport.Email = "email@email.com";
			signersCompletionReport.FirstName = "Patty";
			signersCompletionReport.LastName = "Galant";
			signersCompletionReport.Completed = false;	

			OneSpanSign.API.PackageCompletionReport packageCompletionReport = new OneSpanSign.API.PackageCompletionReport();
            packageCompletionReport.Trashed = false;
			packageCompletionReport.Id = "packageId";
			packageCompletionReport.Name = "PackageName";
            packageCompletionReport.Status = DocumentPackageStatus.SENT.getApiValue();
			packageCompletionReport.AddSigner(signersCompletionReport);
			packageCompletionReport.AddDocument(documentCompletionReport);

			OneSpanSign.API.Sender sender = new OneSpanSign.API.Sender();
			sender.Email = "sender@email.com";
			sender.FirstName = "SignerFirstName";
			sender.LastName = "SignerLastName";

			OneSpanSign.API.SenderCompletionReport senderCompletionReport = new OneSpanSign.API.SenderCompletionReport();
			senderCompletionReport.AddPackage(packageCompletionReport);
			senderCompletionReport.Sender = sender;

			OneSpanSign.API.CompletionReport completionReport = new OneSpanSign.API.CompletionReport();
			completionReport.To = new DateTime(1234);
			completionReport.From = new DateTime(5678);
			completionReport.AddSender(senderCompletionReport);

			return completionReport;
		}
    }
}

