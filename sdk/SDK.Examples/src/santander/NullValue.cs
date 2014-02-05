using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class NullArray
	{
		public static void Main( String []args ) {
			new NullArray().go();
		}

		public NullArray()
		{
		}

		public void go() {
			createPackageWithSettings();
		}

		public void createPackageWithSettings() {
			EslClient eslClient = createSantanderEnvironmentEslClient();
//			EslClient eslClient = createProductionEnvironmentEslClient();
			DocumentPackage builtPackage = PackageBuilder.NewPackageNamed("Package Name")
				.WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
					.WithDecline()
					.WithOptOut()
					.WithDocumentToolbarDownloadButton()
					.WithoutCaptureText()
					.WithCeremonyLayoutSettings(CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
						.WithoutGlobalNavigation()
						.WithoutBreadCrumbs()
						.WithoutSessionBar()
						.WithoutProgressBar()
					)
				).Build();
			eslClient.CreatePackage(builtPackage);
		}

		private EslClient createProductionEnvironmentEslClient() {
			return new EslClient("SGREQ0RpNUxZb1FZOm5FWDJvUEl3NEFjWA==", "https://apps.e-signlive.com/api");
		}

		private EslClient createSantanderEnvironmentEslClient() {
			// email of api key owner = alain_aj@live.com
			return new EslClient("YWxhaW46QnNicDJ5c0lBRGdI", "https://santander.e-signlive.com/api");
		}
	}
}

