using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class PackageSettingsExample
    {
        public static string apiToken = "Y2QyOTg1YzUtY2E4MC00M2YyLThhNTMtYjQxZmY5MTQzNmVhOkJzYnAyeXNJQURnSA==";
        //"YOUR TOKEN HERE";
        public static string baseUrl = "http://localhost:8080";
        //"ENVIRONMENT URL HERE";
        public static void Main (string[] args)
        {
            FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");
            EslClient eslClient = new EslClient(apiToken, baseUrl);
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("DocumentPackageSettings " + DateTime.Now)
				.WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
				              .WithInPerson()
				              .WithoutDecline()
							  .WithOptOut()
				              .WithOptOutReason("Reason One")
				              .WithOptOutReason("Reason Two")
				              .WithOptOutReason("Reason Three")
				              .WithHandOverLinkHref("http://www.google.ca")
				              .WithHandOverLinkText("click here")
				              .WithHandOverLinkTooltip("link tooltip")

				              .WithCeremonyLayoutSettings(CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
				                            .WithoutProgressBar()
				                            .WithoutSessionBar()
				                            .WithoutTitle()
				                            .WithoutNavigator()
				                            .WithoutGlobalNavigation()
				                            .WithoutBreadCrumbs()
				                            .WithLogoImageLink("sps")
				                            .WithLogoImageSource("sps") 
            )
            )
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
					               .OnPage(0)
					               .AtPosition(100, 100)))
					.Build();

            PackageId packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            DocumentPackage aPackage = eslClient.GetPackage(packageId);

            DocumentPackageSettings documentPackageSettings = aPackage.Settings;

            Console.Out.WriteLine("AHA");
        }
    }
}

