using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	internal class CeremonyLayoutSettingsConverter
    {
		private Silanis.ESL.SDK.CeremonyLayoutSettings sdkCeremonyLayoutSettings = null;
		private Silanis.ESL.API.LayoutOptions apiLayoutOptions = null;

		public CeremonyLayoutSettingsConverter(Silanis.ESL.SDK.CeremonyLayoutSettings sdkCeremonyLayoutSettings)
        {
			this.sdkCeremonyLayoutSettings = sdkCeremonyLayoutSettings;
        }

		public CeremonyLayoutSettingsConverter(Silanis.ESL.API.LayoutOptions apiLayoutOptions)
		{
			this.apiLayoutOptions = apiLayoutOptions;
		}

		public Silanis.ESL.API.LayoutOptions ToAPILayoutOptions()
		{
			if (sdkCeremonyLayoutSettings == null)
			{
				return apiLayoutOptions;
			}

			TitleBarOptions titleBarOptions = new TitleBarOptions();
			if (sdkCeremonyLayoutSettings.ShowTitle != null) {
				titleBarOptions.Title = sdkCeremonyLayoutSettings.ShowTitle.Value;
			}
			if (sdkCeremonyLayoutSettings.ProgressBar != null) {
				titleBarOptions.ProgressBar = sdkCeremonyLayoutSettings.ProgressBar.Value;
			}

			HeaderOptions headerOptions = new HeaderOptions();
			if (sdkCeremonyLayoutSettings.BreadCrumbs != null) {
				headerOptions.Breadcrumbs = sdkCeremonyLayoutSettings.BreadCrumbs.Value;
			}
			if (sdkCeremonyLayoutSettings.SessionBar != null) {
				headerOptions.SessionBar = sdkCeremonyLayoutSettings.SessionBar.Value;
			}
			if (sdkCeremonyLayoutSettings.GlobalNavigation != null) {
				headerOptions.GlobalNavigation = sdkCeremonyLayoutSettings.GlobalNavigation.Value;
			}
			if (titleBarOptions != null) {
				headerOptions.TitleBar = titleBarOptions;
			}
			GlobalActionsOptions globalActionsOptions = new GlobalActionsOptions();

			if (sdkCeremonyLayoutSettings.ShowGlobalConfirmButton != null)
			{
				globalActionsOptions.Confirm = sdkCeremonyLayoutSettings.ShowGlobalConfirmButton.Value;
			}
			if (sdkCeremonyLayoutSettings.ShowGlobalDownloadButton != null)
			{
				globalActionsOptions.Download = sdkCeremonyLayoutSettings.ShowGlobalDownloadButton.Value;
			}
			if (sdkCeremonyLayoutSettings.ShowGlobalSaveAsLayoutButton != null)
			{
				globalActionsOptions.SaveAsLayout = sdkCeremonyLayoutSettings.ShowGlobalSaveAsLayoutButton.Value;
			}
			headerOptions.GlobalActions = globalActionsOptions;

			BrandingBarOptions brandingBarOptions = null;
			if ( sdkCeremonyLayoutSettings.LogoImageLink != null || sdkCeremonyLayoutSettings.LogoImageSource != null ) {
				brandingBarOptions = new BrandingBarOptions();
				Image logo = new Image();
				logo.Link = sdkCeremonyLayoutSettings.LogoImageLink;
				logo.Src = sdkCeremonyLayoutSettings.LogoImageSource;
				brandingBarOptions.Logo = logo;
			}

			LayoutOptions result = new LayoutOptions();
			if (sdkCeremonyLayoutSettings.IFrame != null) {
				result.Iframe = sdkCeremonyLayoutSettings.IFrame.Value;
			}
			if (sdkCeremonyLayoutSettings.Navigator != null) {
				result.Navigator = sdkCeremonyLayoutSettings.Navigator.Value;
			}
			result.Footer = new FooterOptions();
			result.Header = headerOptions;
			result.BrandingBar = brandingBarOptions;

			return result;
		}
    }
}

