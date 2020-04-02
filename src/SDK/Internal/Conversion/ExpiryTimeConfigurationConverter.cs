using System;
namespace OneSpanSign.Sdk
{
    internal class ExpiryTimeConfigurationConverter
    {
        private ExpiryTimeConfiguration sdkExpiryTimeConfiguration;
        private OneSpanSign.API.ExpiryTimeConfiguration apiExpiryTimeConfiguration;

        public ExpiryTimeConfigurationConverter (OneSpanSign.API.ExpiryTimeConfiguration apiExpiryTimeConfiguration)
        {
            this.apiExpiryTimeConfiguration = apiExpiryTimeConfiguration;
        }

        public ExpiryTimeConfigurationConverter (ExpiryTimeConfiguration sdkExpiryTimeConfiguration)
        {
            this.sdkExpiryTimeConfiguration = sdkExpiryTimeConfiguration;
        }

        internal OneSpanSign.API.ExpiryTimeConfiguration ToAPIExpiryTimeConfiguration ()
        {
            if (sdkExpiryTimeConfiguration == null) 
            {
                return apiExpiryTimeConfiguration;
            }
            apiExpiryTimeConfiguration = new OneSpanSign.API.ExpiryTimeConfiguration ();
            apiExpiryTimeConfiguration.MaximumRemainingDays = sdkExpiryTimeConfiguration.MaximumRemainingDays;
            apiExpiryTimeConfiguration.RemainingDays = sdkExpiryTimeConfiguration.RemainingDays;

            return apiExpiryTimeConfiguration;
        }

        internal OneSpanSign.Sdk.ExpiryTimeConfiguration ToSDKExpiryTimeConfiguration ()
        {
            if (apiExpiryTimeConfiguration == null) 
            {
                return sdkExpiryTimeConfiguration;
            }
            sdkExpiryTimeConfiguration = new ExpiryTimeConfiguration ();
            sdkExpiryTimeConfiguration.MaximumRemainingDays = apiExpiryTimeConfiguration.MaximumRemainingDays;
            sdkExpiryTimeConfiguration.RemainingDays = apiExpiryTimeConfiguration.RemainingDays;

            return sdkExpiryTimeConfiguration;
        }
    }
}
