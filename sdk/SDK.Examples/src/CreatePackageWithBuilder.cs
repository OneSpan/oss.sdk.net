using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class CreatePackageWithBuilder
	{
		public static string apiToken = "UWM3cnpsbHNvemhIOnNlY3JldA==";
		public static string baseUrl = "http://localhost:8080";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);


			//in person
			DocumentPackage package = PackageBuilder.NewPackageNamed ("C# Package " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.ExpiresOn(DateTime.Now.AddDays(5))
					.WithEmailMessage("This message should appear in email invitation to signers")
					.InPerson(true)
					.Build ();

			PackageId id = client.CreatePackage (package);

			Console.WriteLine ("Package {0} was created", id.Id);
		}
	}
}