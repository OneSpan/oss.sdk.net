using System;

namespace OneSpanSign.Sdk
{
    internal class ExternalConverter
    {
        private External sdkExternal;
        private OneSpanSign.API.External apiExternal;

        public ExternalConverter(OneSpanSign.API.External apiExternal){
            this.apiExternal = apiExternal;
        }

        public ExternalConverter(External sdkExternal)
        {
            this.sdkExternal = sdkExternal;
        }

        internal OneSpanSign.API.External ToAPIExternal()
        {
            if (sdkExternal == null)
            {
                return apiExternal;
            }
            apiExternal = new OneSpanSign.API.External();
            apiExternal.Id = sdkExternal.Id;
            apiExternal.Provider = sdkExternal.Provider;
            apiExternal.ProviderName = sdkExternal.ProviderName;

            return apiExternal;
        }

        internal OneSpanSign.Sdk.External ToSDKExternal()
        {
            if (apiExternal == null)
            {
                return sdkExternal;
            }
            sdkExternal = new External();
            sdkExternal.Id = apiExternal.Id;
            sdkExternal.Provider = apiExternal.Provider;
            sdkExternal.ProviderName = apiExternal.ProviderName;

            return sdkExternal;
        }
    }
}

