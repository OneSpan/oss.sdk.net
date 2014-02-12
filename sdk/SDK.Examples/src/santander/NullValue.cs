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
//			createPackage(buildPackageWithSettings());
			createPackage(buildPackageWithSantanderValues());
		}

		public void createPackage( DocumentPackage package ) {
			EslClient eslClient = createLocalEnvironmentEslClient();
			eslClient.CreatePackage(package);
		}

		public DocumentPackage buildPackageWithSettings() {
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
			return builtPackage;
		}

		private EslClient createSantanderEnvironmentEslClient() {
			// email of api key owner = alain_aj@live.com
			return new EslClient("YWxhaW46QnNicDJ5c0lBRGdI", "https://santander.e-signlive.com/api" );
		}

		private EslClient createLocalEnvironmentEslClient() {
			return new EslClient( "YmFyYmFySWQ6QnNicDJ5c0lBRGdI", "http://localhost:8181/api");
		}

		private DocumentPackage buildPackageWithSantanderValues() {
			DocumentPackage result = PackageBuilder.NewPackageNamed("TRIPP")
				.WithSigner(SignerBuilder.NewSignerWithEmail("david_lawson@silanis.com")
					.WithFirstName("WSLLBEK")
					.WithLastName("RSSPS"))
				.Build();

			return result;
		}

		private Signature createSantanderSignature( string signerEmail ) {
			Signature result = SignatureBuilder
				.AcceptanceFor(signerEmail)
				.OnPage(3)
				.AtPosition(75, 234)
				.WithSize(245, 20)
				.WithField(FieldBuilder.SignatureDate()
					.AtPosition(327.2, 345.6)
					.WithSize(88.3, 20.5)
					.OnPage(3))
				.Build();

			return result;
		}
	}
}

