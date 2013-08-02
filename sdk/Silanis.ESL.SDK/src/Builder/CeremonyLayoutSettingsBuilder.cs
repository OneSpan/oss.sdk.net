using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	public class CeremonyLayoutSettingsBuilder
	{
		private Nullable<bool> iFrame = null;
		private Nullable<bool> breadCrumbs = null;
		private Nullable<bool> sessionBar = null;
		private Nullable<bool> globalNavigation = null;
		private Nullable<bool> progressBar = null;
		private Nullable<bool> showTitle = null;
		private Nullable<bool> navigator = null;
		private string logoImageSource = null;
		private string logoImageLink = null;

		public CeremonyLayoutSettingsBuilder WithIFrame()
		{
			iFrame = true;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithoutBreadCrumbs()
		{
			breadCrumbs = false;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithoutSessionBar()
		{
			sessionBar = false;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithoutGlobalNavigation()
		{
			globalNavigation = false;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithoutProgressBar()
		{
			progressBar = false;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithoutTitle()
		{
			showTitle = false;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithoutNavigator()
		{
			navigator = false;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithLogoImageSource( String logoImageSource )
		{
			this.logoImageSource = logoImageSource;
			return this;
		}

		public CeremonyLayoutSettingsBuilder WithLogoImageLink( String logoImageLink )
		{
			this.logoImageLink = logoImageLink;
			return this;
		}

		public static CeremonyLayoutSettingsBuilder NewCeremonyLayoutSettings()
		{
			return new CeremonyLayoutSettingsBuilder ();
		}

		private CeremonyLayoutSettingsBuilder ()
		{
		}

		internal CeremonyLayoutSettingsBuilder( CeremonySettings ceremonySettings ) 
		{
			iFrame = ceremonySettings.Layout.Iframe;
			breadCrumbs = ceremonySettings.Layout.Header.Breadcrumbs;
			sessionBar = ceremonySettings.Layout.Header.SessionBar;
			globalNavigation = ceremonySettings.Layout.Header.GlobalNavigation;
			progressBar = ceremonySettings.Layout.Header.TitleBar.ProgressBar;
			showTitle = ceremonySettings.Layout.Header.TitleBar.Title;
			navigator = ceremonySettings.Layout.Navigator;
			logoImageSource = ceremonySettings.Layout.BrandingBar.Logo.Src;
			logoImageLink = ceremonySettings.Layout.BrandingBar.Logo.Link;
		}

		public CeremonyLayoutSettings Build()
		{
			CeremonyLayoutSettings result = new CeremonyLayoutSettings ();
			result.IFrame = iFrame;
			result.BreadCrumbs = breadCrumbs;
			result.SessionBar = sessionBar;
			result.GlobalNavigation = globalNavigation;
			result.ProgressBar = progressBar;
			result.ShowTitle = showTitle;
			result.Navigator = navigator;
			result.LogoImageSource = logoImageSource;
			result.LogoImageLink = logoImageLink;

			return result;
		}
	}
}

