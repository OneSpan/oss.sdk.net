namespace OneSpanSign.Sdk
{
    internal class AccountUploadSettingsConverter
    {
        private OneSpanSign.Sdk.AccountUploadSettings sdkAccountUploadSettings;
        private OneSpanSign.API.AccountUploadSettings apiAccountUploadSettings;   
        
        public AccountUploadSettingsConverter(OneSpanSign.API.AccountUploadSettings apiAccountUploadSettings)
        {
            this.apiAccountUploadSettings = apiAccountUploadSettings;
        }
        
        public AccountUploadSettingsConverter(OneSpanSign.Sdk.AccountUploadSettings sdkAccountUploadSettings)
        {
            this.sdkAccountUploadSettings = sdkAccountUploadSettings;
        }
        
        public OneSpanSign.API.AccountUploadSettings ToAPIAccountUploadSettings()
        {
            if (sdkAccountUploadSettings == null)
            {
                return apiAccountUploadSettings;
            }

            OneSpanSign.API.AccountUploadSettings result = new OneSpanSign.API.AccountUploadSettings();
            result.AllowedFileTypes = sdkAccountUploadSettings.AllowedFileTypes;
            return result;
        }

        public OneSpanSign.Sdk.AccountUploadSettings ToSDKAccountUploadSettings()
        {
            if (apiAccountUploadSettings == null)
            {
                return sdkAccountUploadSettings;
            }

            OneSpanSign.Sdk.AccountUploadSettings result = new OneSpanSign.Sdk.AccountUploadSettings();
            result.AllowedFileTypes = apiAccountUploadSettings.AllowedFileTypes;
            return result;
        }   
    }
}