using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	public class CeremonyLayoutSettings
	{
		private Nullable<bool> iFrame;

		public Nullable<bool> IFrame {
			get {
				return iFrame;
			}
			set {
				iFrame = value;
			}
		}

		private Nullable<bool> breadCrumbs;

		public Nullable<bool> BreadCrumbs {
			get {
				return breadCrumbs;
			}
			set {
				breadCrumbs = value;
			}
		}

		private Nullable<bool> sessionBar;

		public Nullable<bool> SessionBar {
			get {
				return sessionBar;
			}
			set {
				sessionBar = value;
			}
		}

		private Nullable<bool> globalNavigation;

		public Nullable<bool> GlobalNavigation {
			get {
				return globalNavigation;
			}
			set {
				globalNavigation = value;
			}
		}

		private Nullable<bool> progressBar;

		public Nullable<bool> ProgressBar {
			get {
				return progressBar;
			}
			set {
				progressBar = value;
			}
		}

		private Nullable<bool> showTitle;

		public Nullable<bool> ShowTitle {
			get {
				return showTitle;
			}
			set {
				showTitle = value;
			}
		}

		private Nullable<bool> navigator;

		public Nullable<bool> Navigator {
			get {
				return navigator;
			}
			set {
				navigator = value;
			}
		}

		private string logoImageSource;

		public string LogoImageSource {
			get {
				return logoImageSource;
			}
			set {
				logoImageSource = value;
			}
		}

		private string logoImageLink;

		public string LogoImageLink {
			get {
				return logoImageLink;
			}
			set {
				logoImageLink = value;
			}
		}

		public CeremonyLayoutSettings ()
		{
		}

		internal LayoutOptions toAPILayoutOptions()
		{
			TitleBarOptions titleBarOptions = new TitleBarOptions();
			if (showTitle != null) {
				titleBarOptions.Title = showTitle.Value;
			}
			if (progressBar != null) {
				titleBarOptions.ProgressBar = progressBar.Value;
			}

			HeaderOptions headerOptions = new HeaderOptions();
			if (breadCrumbs != null) {
				headerOptions.Breadcrumbs = breadCrumbs.Value;
			}
			if (sessionBar != null) {
				headerOptions.SessionBar = sessionBar.Value;
			}
			if (globalNavigation != null) {
				headerOptions.GlobalNavigation = globalNavigation.Value;
			}
			if (titleBarOptions != null) {
				headerOptions.TitleBar = titleBarOptions;
			}

			BrandingBarOptions brandingBarOptions = null;
			if ( logoImageLink != null || logoImageSource != null ) {
				brandingBarOptions = new BrandingBarOptions();
				Image logo = new Image();
				logo.Link = logoImageLink;
				logo.Src = logoImageSource;
				brandingBarOptions.Logo = logo;
			}

			LayoutOptions result = new LayoutOptions();
			if (iFrame != null) {
				result.Iframe = iFrame.Value;
			}
			if (navigator != null) {
				result.Navigator = navigator.Value;
			}
			result.Footer = new FooterOptions();
			result.Header = headerOptions;
			result.BrandingBar = brandingBarOptions;

			return result;
		}
	}
}

