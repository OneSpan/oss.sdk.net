using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
	public class GetPackageListExample
	{
		public static string apiToken = "YOUR TOKEN HERE";
		public static string baseUrl = "ENVIRONMENT URL HERE";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);

			//Get the packages that have status COMPLETED, starting from the most recent package and getting 20 packages per page
			Page<DocumentPackage> packages = client.PackageService.GetPackages (DocumentPackageStatus.COMPLETED, new PageRequest(1, 20));

			PrintPage (packages);

			while (packages.HasNextPage())
			{
				packages = client.PackageService.GetPackages (DocumentPackageStatus.COMPLETED, packages.NextRequest);
				PrintPage (packages);
			}
		}

		private static void PrintPage(Page<DocumentPackage> page)
		{
			Console.WriteLine ("Got {0} packages, total = {1}", page.NumberOfElements, page.TotalElements);

			foreach (DocumentPackage package in page)
			{
				Console.WriteLine ("Package {0} has status {1}", package.Name, package.Status);
			}
		}
	}
}