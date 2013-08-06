using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class PackageSettingsExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

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
				                            .WithoutGlobalNavigation()
				                            .WithoutBreadCrumbs() ) )
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
					               .OnPage(0)
					               .AtPosition(100, 100)))
					.Build();

            eslClient.CreatePackage(superDuperPackage);
        }
    }
}

