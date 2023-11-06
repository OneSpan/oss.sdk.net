namespace OneSpanSign.Sdk.Internal.Conversion
{
    internal class AccountSystemSettingPropertiesConverter
    {
        private OneSpanSign.Sdk.AccountSystemSettingProperties sdkAccountSystemSettingProperties;
        private OneSpanSign.API.AccountSystemSettingProperties apiAccountSystemSettingProperties;   
        
        public AccountSystemSettingPropertiesConverter(OneSpanSign.API.AccountSystemSettingProperties apiAccountSystemSettingProperties)
        {
            this.apiAccountSystemSettingProperties = apiAccountSystemSettingProperties;
        }

        public AccountSystemSettingPropertiesConverter(OneSpanSign.Sdk.AccountSystemSettingProperties sdkAccountSystemSettingProperties)
        {
            this.sdkAccountSystemSettingProperties = sdkAccountSystemSettingProperties;
        }
        
        public OneSpanSign.API.AccountSystemSettingProperties ToAPIAccountSystemSettingProperties()
        {
            if (sdkAccountSystemSettingProperties == null)
            {
                return apiAccountSystemSettingProperties;
            }

            OneSpanSign.API.AccountSystemSettingProperties result = new OneSpanSign.API.AccountSystemSettingProperties();
            result.LoginSessionTimeout = sdkAccountSystemSettingProperties.LoginSessionTimeout;
            result.SenderLoginMaxFailedAttempts = sdkAccountSystemSettingProperties.SenderLoginMaxFailedAttempts;
            result.SessionTimeoutWarning = sdkAccountSystemSettingProperties.SessionTimeoutWarning;
            return result;
        }

        public OneSpanSign.Sdk.AccountSystemSettingProperties ToSDKAccountSystemSettingProperties()
        {
            if (apiAccountSystemSettingProperties == null)
            {
                return sdkAccountSystemSettingProperties;
            }

            OneSpanSign.Sdk.AccountSystemSettingProperties result = new OneSpanSign.Sdk.AccountSystemSettingProperties();
            result.LoginSessionTimeout = apiAccountSystemSettingProperties.LoginSessionTimeout;
            result.SessionTimeoutWarning = apiAccountSystemSettingProperties.SessionTimeoutWarning;
            result.SenderLoginMaxFailedAttempts = apiAccountSystemSettingProperties.SenderLoginMaxFailedAttempts;
            return result;
        }
    }
} 