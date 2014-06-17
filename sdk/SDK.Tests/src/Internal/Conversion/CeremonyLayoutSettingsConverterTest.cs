using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture()]
    public class CeremonyLayoutSettingsConverterTest
    {
		private Silanis.ESL.SDK.CeremonyLayoutSettings sdkCeremonyLayoutSettings1 = null;
		private Silanis.ESL.API.LayoutOptions apiLayoutOptions1 = null;
		private Silanis.ESL.API.LayoutOptions apiLayoutOptions2 = null;
		private Silanis.ESL.SDK.CeremonyLayoutSettingsConverter converter = null;

		[Test()]
		public void ConvertNullSDKToAPI()
		{
			sdkCeremonyLayoutSettings1 = null;
			converter = new CeremonyLayoutSettingsConverter(sdkCeremonyLayoutSettings1);
			Assert.IsNull(converter.ToAPILayoutOptions());
		}

		[Test()]
		public void ConvertNullAPIToAPI()
		{
			apiLayoutOptions1 = null;
			converter = new CeremonyLayoutSettingsConverter(apiLayoutOptions1);

			Assert.IsNull(converter.ToAPILayoutOptions());
		}

		[Test()]
		public void ConvertAPIToAPI()
		{
			apiLayoutOptions1 = CreateTypicalAPICeremonyLayoutSettings();
			converter = new CeremonyLayoutSettingsConverter(apiLayoutOptions1);
			apiLayoutOptions2 = converter.ToAPILayoutOptions();

			Assert.IsNotNull(apiLayoutOptions2);
			Assert.AreEqual(apiLayoutOptions2, apiLayoutOptions1);
		}

		[Test()]
		public void ConvertSDKToAPI()
		{
			sdkCeremonyLayoutSettings1 = CreateTypicalSDKCeremonyLayoutSettings();
			apiLayoutOptions1 = new CeremonyLayoutSettingsConverter(sdkCeremonyLayoutSettings1).ToAPILayoutOptions();

			Assert.IsNotNull(apiLayoutOptions1);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.LogoImageLink, apiLayoutOptions1.BrandingBar.Logo.Link);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.LogoImageSource, apiLayoutOptions1.BrandingBar.Logo.Src);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.IFrame, apiLayoutOptions1.Iframe);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.ShowTitle, apiLayoutOptions1.Header.TitleBar.Title);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.SessionBar, apiLayoutOptions1.Header.SessionBar);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.ProgressBar, apiLayoutOptions1.Header.TitleBar.ProgressBar);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.BreadCrumbs, apiLayoutOptions1.Header.Breadcrumbs);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.GlobalNavigation, apiLayoutOptions1.Header.GlobalNavigation);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.ShowGlobalConfirmButton, apiLayoutOptions1.Header.GlobalActions.Confirm);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.ShowGlobalDownloadButton, apiLayoutOptions1.Header.GlobalActions.Download);
			Assert.AreEqual(sdkCeremonyLayoutSettings1.ShowGlobalSaveAsLayoutButton, apiLayoutOptions1.Header.GlobalActions.SaveAsLayout);
		}

		private Silanis.ESL.SDK.CeremonyLayoutSettings CreateTypicalSDKCeremonyLayoutSettings()
		{
			Silanis.ESL.SDK.CeremonyLayoutSettings settings = new Silanis.ESL.SDK.CeremonyLayoutSettings();
			settings.LogoImageLink = "logoImageLink";
			settings.LogoImageSource = "logoImageSource";
			settings.IFrame = true;
			settings.ShowTitle = true;
			settings.SessionBar = true;
			settings.ProgressBar = true;
			settings.BreadCrumbs = true;
			settings.GlobalNavigation = true;
			settings.ShowGlobalConfirmButton = true;
			settings.ShowGlobalDownloadButton = true;
			settings.ShowGlobalSaveAsLayoutButton = true;

			return settings;
		}

		private Silanis.ESL.API.LayoutOptions CreateTypicalAPICeremonyLayoutSettings()
		{
			Silanis.ESL.API.LayoutOptions layoutOptions = new Silanis.ESL.API.LayoutOptions();
			return layoutOptions;
		}
    }
}

