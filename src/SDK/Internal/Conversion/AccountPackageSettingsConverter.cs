namespace OneSpanSign.Sdk
{
	internal class AccountPackageSettingsConverter
    {
		private OneSpanSign.Sdk.AccountPackageSettings sdkAccountPackageSettings;
		private OneSpanSign.API.AccountPackageSettings apiAccountPackageSettings;

		public AccountPackageSettingsConverter(OneSpanSign.API.AccountPackageSettings apiAccountPackageSettings)
        {
			this.apiAccountPackageSettings = apiAccountPackageSettings;
        }

		public AccountPackageSettingsConverter(OneSpanSign.Sdk.AccountPackageSettings sdkAccountPackageSettings)
		{
			this.sdkAccountPackageSettings = sdkAccountPackageSettings;
		}

		public OneSpanSign.API.AccountPackageSettings ToAPIAccountPackageSettings()
		{
			if (sdkAccountPackageSettings == null)
			{
				return apiAccountPackageSettings;
			}

			OneSpanSign.API.AccountPackageSettings result = new OneSpanSign.API.AccountPackageSettings();

			result.Ada = sdkAccountPackageSettings.Ada;
			result.DeclineButton = sdkAccountPackageSettings.DeclineButton;
			result.DefaultTimeBasedExpiry = sdkAccountPackageSettings.DefaultTimeBasedExpiry;
			result.DisableDeclineOther = sdkAccountPackageSettings.DisableDeclineOther;
			result.DisableDownloadForUncompletedPackage = sdkAccountPackageSettings.DisableDownloadForUncompletedPackage;
			result.DisableFirstInPersonAffidavit = sdkAccountPackageSettings.DisableFirstInPersonAffidavit;
			result.DisableInPersonAffidavit = sdkAccountPackageSettings.DisableInPersonAffidavit;
			result.DisableSecondInPersonAffidavit = sdkAccountPackageSettings.DisableSecondInPersonAffidavit;
			result.EnforceCaptureSignature = sdkAccountPackageSettings.EnforceCaptureSignature;
			result.ExtractAcroFields = sdkAccountPackageSettings.ExtractAcroFields;
			result.ExtractTextTags = sdkAccountPackageSettings.ExtractTextTags;
			result.GlobalActionsDownload = sdkAccountPackageSettings.GlobalActionsDownload;
			result.GlobalActionsHideEvidenceSummary = sdkAccountPackageSettings.GlobalActionsHideEvidenceSummary;
			result.HideCaptureText = sdkAccountPackageSettings.HideCaptureText;
			result.HideLanguageDropdown = sdkAccountPackageSettings.HideLanguageDropdown;
			result.HidePackageOwnerInPerson = sdkAccountPackageSettings.HidePackageOwnerInPerson;
			result.HideWatermark = sdkAccountPackageSettings.HideWatermark;
			result.InPerson = sdkAccountPackageSettings.InPerson;
			result.LeftMenuExpand = sdkAccountPackageSettings.LeftMenuExpand;
			result.OptionalNavigation = sdkAccountPackageSettings.OptionalNavigation;
			result.ShowNseHelp = sdkAccountPackageSettings.ShowNseHelp;
			result.ShowNseLogoInIframe = sdkAccountPackageSettings.ShowNseLogoInIframe;
			result.ShowNseOverview = sdkAccountPackageSettings.ShowNseOverview;
			
            return result;
		}

		public OneSpanSign.Sdk.AccountPackageSettings ToSDKAccountPackageSettings()
		{
			if (apiAccountPackageSettings == null)
			{
				return sdkAccountPackageSettings;
			}

			OneSpanSign.Sdk.AccountPackageSettings result = new OneSpanSign.Sdk.AccountPackageSettings();
			result.Ada = apiAccountPackageSettings.Ada;
			result.DeclineButton = apiAccountPackageSettings.DeclineButton;
			result.DefaultTimeBasedExpiry = apiAccountPackageSettings.DefaultTimeBasedExpiry;
			result.DisableDeclineOther = apiAccountPackageSettings.DisableDeclineOther;
			result.DisableDownloadForUncompletedPackage = apiAccountPackageSettings.DisableDownloadForUncompletedPackage;
			result.DisableFirstInPersonAffidavit = apiAccountPackageSettings.DisableFirstInPersonAffidavit;
			result.DisableInPersonAffidavit = apiAccountPackageSettings.DisableInPersonAffidavit;
			result.DisableSecondInPersonAffidavit = apiAccountPackageSettings.DisableSecondInPersonAffidavit;
			result.EnforceCaptureSignature = apiAccountPackageSettings.EnforceCaptureSignature;
			result.ExtractAcroFields = apiAccountPackageSettings.ExtractAcroFields;
			result.ExtractTextTags = apiAccountPackageSettings.ExtractTextTags;
			result.GlobalActionsDownload = apiAccountPackageSettings.GlobalActionsDownload;
			result.GlobalActionsHideEvidenceSummary = apiAccountPackageSettings.GlobalActionsHideEvidenceSummary;
			result.HideCaptureText = apiAccountPackageSettings.HideCaptureText;
			result.HideLanguageDropdown = apiAccountPackageSettings.HideLanguageDropdown;
			result.HidePackageOwnerInPerson = apiAccountPackageSettings.HidePackageOwnerInPerson;
			result.HideWatermark = apiAccountPackageSettings.HideWatermark;
			result.InPerson = apiAccountPackageSettings.InPerson;
			result.LeftMenuExpand = apiAccountPackageSettings.LeftMenuExpand;
			result.OptionalNavigation = apiAccountPackageSettings.OptionalNavigation;
			result.ShowNseHelp = apiAccountPackageSettings.ShowNseHelp;
			result.ShowNseLogoInIframe = apiAccountPackageSettings.ShowNseLogoInIframe;
			result.ShowNseOverview = apiAccountPackageSettings.ShowNseOverview;

			return result;
		}
		
    }
}

