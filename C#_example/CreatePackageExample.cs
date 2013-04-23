using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
	public class CreatePackageExample
	{
		public static string apiToken = "api token";
		public static string baseUrl = "https://stage-api.e-signlive.com/aws/rest/services";

		public static void Main (string[] args)
		{
            byte[] file = File.ReadAllBytes("C://path/to/file/document.pdf");
            string fileName = System.IO.Path.GetFileName("C://path/to/file/document.pdf");

            // Create new esl client with api token and base url
            EslClient eslClient = new EslClient (apiToken, baseUrl);

            // Create a new package
            PackageId packageId = eslClient.PackageService.CreatePackage(PackageUtil.NewPackage());

            // Upload document to package
            eslClient.PackageService.UploadDocument(packageId, fileName, file, PackageUtil.CreateDocument());

            // Get the package
            Package package = eslClient.PackageService.GetPackage(packageId);

            // Send the package
            eslClient.PackageService.SendPackage(packageId);

            // Create a session token for signer
            // Access the session using the following url:
            SessionToken sessionToken = eslClient.SessionService.CreateSessionToken(packageId, PackageUtil.NewSigner());
            Console.WriteLine("http://stage-web.e-signlive.com/access?sessionToken=" + sessionToken.Token);

            // After the package is complete, you can download the documents

            // Download the zip file of all the documents in the package
            byte[] bytes = eslClient.PackageService.DownloadZippedDocuments(packageId);
            FileStream fileStream = new FileStream("C://path/to/save/file/document.pdf", FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
		}
	}
}

