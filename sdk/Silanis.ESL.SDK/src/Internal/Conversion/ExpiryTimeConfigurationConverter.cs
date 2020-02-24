using System;
namespace Silanis.ESL.SDK
{
    internal class ExpiryTimeConfigurationConverter
    {
        private ExpiryTimeConfiguration sdkExpiryTimeConfiguration;
        private Silanis.ESL.API.ExpiryTimeConfiguration apiExpiryTimeConfiguration;

        public ExpiryTimeConfigurationConverter (Silanis.ESL.API.ExpiryTimeConfiguration apiExpiryTimeConfiguration)
        {
            this.apiExpiryTimeConfiguration = apiExpiryTimeConfiguration;
        }

        public ExpiryTimeConfigurationConverter (ExpiryTimeConfiguration sdkExpiryTimeConfiguration)
        {
            this.sdkExpiryTimeConfiguration = sdkExpiryTimeConfiguration;
        }

        internal Silanis.ESL.API.ExpiryTimeConfiguration ToAPIExpiryTimeConfiguration ()
        {
            if (sdkExpiryTimeConfiguration == null) 
            {
                return apiExpiryTimeConfiguration;
            }
            apiExpiryTimeConfiguration = new Silanis.ESL.API.ExpiryTimeConfiguration ();
            apiExpiryTimeConfiguration.MaximumRemainingDays = sdkExpiryTimeConfiguration.MaximumRemainingDays;
            apiExpiryTimeConfiguration.RemainingDays = sdkExpiryTimeConfiguration.RemainingDays;

            return apiExpiryTimeConfiguration;
        }

        internal Silanis.ESL.SDK.ExpiryTimeConfiguration ToSDKExpiryTimeConfiguration ()
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
