using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Internal.Conversion
{
    internal class SubAccountApiKeyConverter
    {
        private SubAccountApiKey sdkSubAccountApiKey;
        private OneSpanSign.API.SubAccountApiKey apiSubAccountApiKey;
        
        public SubAccountApiKeyConverter(SubAccountApiKey sdkSubAccountApiKey)
        {
            this.sdkSubAccountApiKey = sdkSubAccountApiKey;
        }

        public SubAccountApiKeyConverter(OneSpanSign.API.SubAccountApiKey apiSubAccountApiKey)
        {
            this.apiSubAccountApiKey = apiSubAccountApiKey;
        }
        
        public SubAccountApiKey ToSdkSubAccountApiKey()
        {
            if (sdkSubAccountApiKey != null)
            {
                return sdkSubAccountApiKey;
            }
            else if (apiSubAccountApiKey != null)
            {
                SubAccountApiKeyBuilder builder = SubAccountApiKeyBuilder.NewSubAccountApiKey()
                    .WithAccountUid(apiSubAccountApiKey.AccountUid)
                    .WithAccountName(apiSubAccountApiKey.AccountName)
                    .WithApiKey(apiSubAccountApiKey.ApiKey);
                
                return builder.Build();
            }
            else
            {
                return null;
            }
        }
        
        public OneSpanSign.API.SubAccountApiKey ToAPISubAccountApiKey()
        {
            if (apiSubAccountApiKey != null)
            {
                return apiSubAccountApiKey;
            }
            else if (sdkSubAccountApiKey != null)
            {
                OneSpanSign.API.SubAccountApiKey result = new OneSpanSign.API.SubAccountApiKey();
                result.AccountUid = sdkSubAccountApiKey.AccountUid;
                result.AccountName = sdkSubAccountApiKey.AccountName;
                result.ApiKey = sdkSubAccountApiKey.ApiKey;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}