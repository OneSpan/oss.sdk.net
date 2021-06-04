using OneSpanSign.API;

namespace OneSpanSign.Sdk.Builder
{
    public class SubAccountApiKeyBuilder
    {
        private string accountUid;
        private string accountName;
        private string apiKey;

        private SubAccountApiKeyBuilder()
        {
        }

        public static SubAccountApiKeyBuilder NewSubAccountApiKey()
        {
            return new SubAccountApiKeyBuilder();
        }

        public SubAccountApiKeyBuilder WithAccountUid(string accountUid)
        {
            this.accountUid = accountUid;
            return this;
        }

        public SubAccountApiKeyBuilder WithAccountName(string accountName)
        {
            this.accountName = accountName;
            return this;
        }

        public SubAccountApiKeyBuilder WithApiKey(string apiKey)
        {
            this.apiKey = apiKey;
            return this;
        }

        public SubAccountApiKey Build()
        {
            SubAccountApiKey subAccountApiKey = new SubAccountApiKey();
            subAccountApiKey.AccountUid = accountUid;
            subAccountApiKey.AccountName = accountName;
            subAccountApiKey.ApiKey = apiKey;

            return subAccountApiKey;
        }
    }
}