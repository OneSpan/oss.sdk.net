using System;

namespace OneSpanSign.Sdk
{
    internal class SupportConfigurationConverter
    {
        private SupportConfiguration sdkSupportConfiguration;
        private OneSpanSign.API.SupportConfiguration apiSupportConfiguration;

        public SupportConfigurationConverter(SupportConfiguration sdkSupportConfiguration)
        {
            this.sdkSupportConfiguration = sdkSupportConfiguration;
        }

        public SupportConfigurationConverter(OneSpanSign.API.SupportConfiguration apiSupportConfiguration)
        {
            this.apiSupportConfiguration = apiSupportConfiguration;
        }

        public SupportConfiguration ToSDKSupportConfiguration()
        {
            if (apiSupportConfiguration == null)
            {
                return sdkSupportConfiguration;
            }

            SupportConfiguration result = new SupportConfiguration();
            result.Email = apiSupportConfiguration.Email;
            result.Phone = apiSupportConfiguration.Phone;
            return result;
        }

        public OneSpanSign.API.SupportConfiguration ToAPISupportConfiguration()
        {
            if (sdkSupportConfiguration == null)
            {
                return apiSupportConfiguration;
            }

            OneSpanSign.API.SupportConfiguration result = new OneSpanSign.API.SupportConfiguration();
            result.Email = sdkSupportConfiguration.Email;
            result.Phone = sdkSupportConfiguration.Phone;
            return result;
        }
    }
}

