using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class DownloadDocumentsExample
	{
		public static string apiToken = "YOUR TOKEN HERE";
		public static string baseUrl = "ENVIRONMENT URL HERE";

		public static void Main (string[] args)
		{
			// Create new oss client with api token and base url
			OssClient client = new OssClient (apiToken, baseUrl);
			PackageId packageId = new PackageId ("GLK2xasqLvFe2wc4qwO5iTKyjx42");

            byte[] downloadedDocument = client.DownloadDocument (packageId, "testing");
            File.WriteAllBytes (Directory.GetCurrentDirectory() + "/SampleDocuments/downloaded.pdf", downloadedDocument);

            byte[] downloadedEvidence = client.DownloadEvidenceSummary (packageId);
            File.WriteAllBytes (Directory.GetCurrentDirectory() + "/evidence-summary.pdf", downloadedEvidence);

            byte[] downloadedZip = client.DownloadZippedDocuments (packageId);
            File.WriteAllBytes (Directory.GetCurrentDirectory() + "/package-documents.zip", downloadedZip);
		}
	}
}