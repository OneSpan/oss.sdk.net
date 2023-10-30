namespace OneSpanSign.Sdk
{
    internal class AccountEmailReminderSettingsConverter
    {
        private OneSpanSign.Sdk.AccountEmailReminderSettings sdkAccountEmailReminderSettings;
        private OneSpanSign.API.AccountEmailReminderSettings apiAccountEmailReminderSettings;   
        
        public AccountEmailReminderSettingsConverter(OneSpanSign.API.AccountEmailReminderSettings apiAccountEmailReminderSettings)
        {
            this.apiAccountEmailReminderSettings = apiAccountEmailReminderSettings;
        }

        public AccountEmailReminderSettingsConverter(OneSpanSign.Sdk.AccountEmailReminderSettings sdkAccountEmailReminderSettings)
        {
            this.sdkAccountEmailReminderSettings = sdkAccountEmailReminderSettings;
        }
        
	    public OneSpanSign.API.AccountEmailReminderSettings ToAPIAccountEmailReminderSettings()
		{
			if (sdkAccountEmailReminderSettings == null)
			{
				return apiAccountEmailReminderSettings;
			}

			OneSpanSign.API.AccountEmailReminderSettings result = new OneSpanSign.API.AccountEmailReminderSettings();
			result.StartInDaysDelay = sdkAccountEmailReminderSettings.StartInDaysDelay;
			result.IntervalInDays = sdkAccountEmailReminderSettings.IntervalInDays;
			result.RepetitionsCount = sdkAccountEmailReminderSettings.RepetitionsCount;
            return result;
		}

		public OneSpanSign.Sdk.AccountEmailReminderSettings ToSDKAccountEmailReminderSettings()
		{
			if (apiAccountEmailReminderSettings == null)
			{
				return sdkAccountEmailReminderSettings;
			}

			OneSpanSign.Sdk.AccountEmailReminderSettings result = new OneSpanSign.Sdk.AccountEmailReminderSettings();
			result.StartInDaysDelay = sdkAccountEmailReminderSettings.StartInDaysDelay;
			result.IntervalInDays = sdkAccountEmailReminderSettings.IntervalInDays;
			result.RepetitionsCount = sdkAccountEmailReminderSettings.RepetitionsCount;
			return result;
		}
    }
}