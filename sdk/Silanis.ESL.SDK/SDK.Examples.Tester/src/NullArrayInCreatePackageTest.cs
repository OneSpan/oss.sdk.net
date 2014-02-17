using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class NullArrayInCreatePackageTest
    {
        [Test()]
        public void TestCase()
        {
			InitiateSession();
        }

		private DocumentPackageSettingsBuilder buildDocumentPackageSettings1() {
			return DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
				.WithDecline()
				.WithOptOut()
				.WithDocumentToolbarDownloadButton()
				.WithoutCaptureText()
				.WithCeremonyLayoutSettings(CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
					.WithoutGlobalNavigation()
					.WithoutBreadCrumbs()
					.WithoutSessionBar()
					.WithoutProgressBar()
				);
		}

		private DocumentPackageSettingsBuilder buildDocumentPackageSettings2() {
			return DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
//				.WithHandOverLinkHref(m_callbackURLWithArguments)
				.WithHandOverLinkText("Continue")
				.WithHandOverLinkTooltip("Click 'Continue' to continue with the SimplePro process.")
				.WithoutOptOut()
				.WithCeremonyLayoutSettings(CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
					.WithoutGlobalNavigation()
					.WithoutBreadCrumbs()
					.WithoutSessionBar()
					.WithIFrame()
					.WithoutTitle()
					.WithNavigator()
					//.WithLogoImageLink("http://cresinsurance.com")
					//.WithLogoImageSource("http://www.cresinsurance.com/images/creslogo.jpg")
				);
		}

		public string InitiateSession()
		{
			//dlawson@silanis.com, production
			string apiKey = "SGREQ0RpNUxZb1FZOm5FWDJvUEl3NEFjWA=="; 
			string apiUrl = "https://apps.e-signlive.com/api";

			//dlawson@silanis.com, sandbox
//			string apiKey = "MUhwSTNFa2FiV2NLOnB5MkJ3YUxydGpFQg==";
//			string apiUrl = "https://sandbox.e-signlive.com/api";

//			string apiKey = "YmFyYmFySWQ6QnNicDJ5c0lBRGdI";
//			string apiUrl = "http://localhost:8181/api";

			string title = "title";
			string quoteId = "quoteId";
			string firstName = "firstName";
			string lastName = "lastName";
			string email = "dave.silanis@gmail.com";


			// Note - passed in title and name not currently used, but we're leaving in in case people 
			// end up wanting to pass in data for name and title through the eSigning mechanism, rather
			// than through the reports.

			// Create new esl client with api token and base url.
			EslClient client = new EslClient(apiKey, apiUrl);

			PackageBuilder packageBuilder = PackageBuilder.NewPackageNamed("To continue your policy creation for quote #" + quoteId + ", click the 'Continue' button below")
				.DescribedAs("Document package for SimplePRO for " + firstName + " " + lastName);

			// For now, only one signer.
			Signer signer = SignerBuilder.NewSignerWithEmail(email)
				.WithFirstName(firstName)
				.WithLastName(lastName)
				.WithCustomId(email)
				.Build();
			packageBuilder.WithSigner(signer)
				.Build();

//			foreach (Document document in documents)
			{
				DocumentBuilder documentBuilder = DocumentBuilder.NewDocumentNamed("documentName").FromFile("/tmp/document.pdf");

				// Note that we are currently using the same signer for each signature block.
//				foreach (SignatureTemplate template in document.SignatureTemplates)
				{
					int page = 0;//(template.UseLastPage == true) ? document.TotalPages - 1 : template.Page - 1; /*					 ESign pages are 0-based... */

					SignatureBuilder sigBuilder;
//					if (acceptDocuments)
//					{
						sigBuilder = SignatureBuilder.AcceptanceFor(signer.Id);

//					}
//					else
					{
						sigBuilder = SignatureBuilder.SignatureFor(signer.Id)
							.OnPage(page)
//							.AtPosition(template.Position.X, template.Position.Y)
//							.WithSize(template.Size.Width, template.Size.Height);
							.AtPosition(100, 100)
							.WithSize(200, 40);
					}
//					if (template.AdditionalFields != null)
//					{
//						foreach (SignatureTemplate field in template.AdditionalFields)
//						{
//							switch (field.SignatureType)
//							{
//								case SignatureType.BoundDateField:
//									{
					sigBuilder.WithField(FieldBuilder.SignatureDate()
						.OnPage(page)
						.AtPosition(100, 150)
						.WithSize(200, 40));
//										sigBuilder.WithField(FieldBuilder.SignatureDate()
//											.OnPage(page)
//											.AtPosition(field.Position.X, field.Position.Y)
//											.WithSize(field.Size.Width, field.Size.Height))
//											.Build();
//										break;
//									}
//								case SignatureType.UnboundTitleField:
//									{
					sigBuilder.WithField(FieldBuilder.TextField()
						.OnPage(page)
						.AtPosition(100, 200)
						.WithSize(200, 40)
						.WithValidation(FieldValidatorBuilder.Alphabetic()
							.Required()));
//										sigBuilder.WithField(FieldBuilder.TextField()
//											.OnPage(page)
//											.WithValue(title)
//											.AtPosition(field.Position.X, field.Position.Y)
//											.WithSize(field.Size.Width, field.Size.Height)
//											.WithValidation(FieldValidatorBuilder.Alphabetic()
//												.Required()))
//											.Build();
//										break;
//									}
//								case SignatureType.UnboundNameField:
//									{
					sigBuilder.WithField(FieldBuilder.TextField()
						.OnPage(page)
						.AtPosition(100, 250)
						.WithSize(200, 40)
						.WithValidation(FieldValidatorBuilder.Alphabetic()
							.Required()));
//										sigBuilder.WithField(FieldBuilder.TextField()
//											.OnPage(page)
//											.WithValue(name)
//											.AtPosition(field.Position.X, field.Position.Y)
//											.WithSize(field.Size.Width, field.Size.Height)
//											.WithValidation(FieldValidatorBuilder.Alphabetic()
//												.Required()))
//											.Build();
//										break;
//									}
//							}
//						}
//					}
					documentBuilder.WithSignature(sigBuilder);
				}

				packageBuilder.WithDocument(documentBuilder);
			}

//			packageBuilder.WithSettings(buildDocumentPackageSettings1());
			packageBuilder.WithSettings(buildDocumentPackageSettings2());

			PackageId id = client.CreatePackage(packageBuilder.Build());

			client.SendPackage(id);

			// Get the session token for integrated signing.
			string sessionToken = client.SessionService.CreateSessionToken(id, signer.Id).Token;

			return id.Id;
		}

    }
}

