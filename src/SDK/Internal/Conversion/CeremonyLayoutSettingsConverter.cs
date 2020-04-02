using System;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
	internal class CeremonyLayoutSettingsConverter
    {
		private OneSpanSign.Sdk.CeremonyLayoutSettings sdkCeremonyLayoutSettings = null;
		private OneSpanSign.API.LayoutOptions apiLayoutOptions = null;

		public CeremonyLayoutSettingsConverter(OneSpanSign.Sdk.CeremonyLayoutSettings sdkCeremonyLayoutSettings)
        {
			this.sdkCeremonyLayoutSettings = sdkCeremonyLayoutSettings;
        }

		public CeremonyLayoutSettingsConverter(OneSpanSign.API.LayoutOptions apiLayoutOptions)
		{
			this.apiLayoutOptions = apiLayoutOptions;
		}

		public OneSpanSign.API.LayoutOptions ToAPILayoutOptions()
		{
			if (sdkCeremonyLayoutSettings == null)
			{
				return apiLayoutOptions;
			}

            HeaderOptions headerOptions = new HeaderOptions();
            if (sdkCeremonyLayoutSettings.ShowTitle != null || sdkCeremonyLayoutSettings.ProgressBar != null) {
                TitleBarOptions titleBarOptions = new TitleBarOptions();
                if (sdkCeremonyLayoutSettings.ShowTitle != null) {
                    titleBarOptions.Title = sdkCeremonyLayoutSettings.ShowTitle.Value;
                }
                if (sdkCeremonyLayoutSettings.ProgressBar != null) {
                    titleBarOptions.ProgressBar = sdkCeremonyLayoutSettings.ProgressBar.Value;
                }
                headerOptions.TitleBar = titleBarOptions;
            }
                
			if (sdkCeremonyLayoutSettings.BreadCrumbs != null) {
				headerOptions.Breadcrumbs = sdkCeremonyLayoutSettings.BreadCrumbs.Value;
			}
			if (sdkCeremonyLayoutSettings.SessionBar != null) {
				headerOptions.SessionBar = sdkCeremonyLayoutSettings.SessionBar.Value;
			}
			if (sdkCeremonyLayoutSettings.GlobalNavigation != null) {
				headerOptions.GlobalNavigation = sdkCeremonyLayoutSettings.GlobalNavigation.Value;
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
			result.Header = headerOptions;
			result.BrandingBar = brandingBarOptions;

			return result;
		}

        public OneSpanSign.Sdk.CeremonyLayoutSettings ToSDKCeremonyLayoutSettings()
        {
            if (apiLayoutOptions == null)
            {
                return sdkCeremonyLayoutSettings;
            }

            CeremonyLayoutSettings result = new CeremonyLayoutSettings();

            result.IFrame = apiLayoutOptions.Iframe;

            if (apiLayoutOptions.Header != null)
            {
                result.BreadCrumbs = apiLayoutOptions.Header.Breadcrumbs;
                result.SessionBar = apiLayoutOptions.Header.SessionBar;
                result.GlobalNavigation = apiLayoutOptions.Header.GlobalNavigation;

                if (apiLayoutOptions.Header.GlobalActions != null)
                {
                    result.ShowGlobalConfirmButton = apiLayoutOptions.Header.GlobalActions.Confirm;
                    result.ShowGlobalDownloadButton = apiLayoutOptions.Header.GlobalActions.Download;
                    result.ShowGlobalSaveAsLayoutButton = apiLayoutOptions.Header.GlobalActions.SaveAsLayout;
                }

                if (apiLayoutOptions.Header.TitleBar != null)
                {
                    result.ShowTitle = apiLayoutOptions.Header.TitleBar.Title;
                    result.ProgressBar = apiLayoutOptions.Header.TitleBar.ProgressBar;
                }
            }

            result.Navigator = apiLayoutOptions.Navigator;

            if (apiLayoutOptions.BrandingBar != null && apiLayoutOptions.BrandingBar.Logo != null)
            {
                result.LogoImageLink = apiLayoutOptions.BrandingBar.Logo.Link;
                result.LogoImageSource = apiLayoutOptions.BrandingBar.Logo.Src;
            }

            return result;
        }
    }
}

