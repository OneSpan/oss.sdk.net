using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class SubAccountConverter
    {
        private SubAccount sdkSubAccount;
        private OneSpanSign.API.SubAccount apiSubAccount;

        public SubAccountConverter(SubAccount sdkSubAccount)
        {
            this.sdkSubAccount = sdkSubAccount;
        }

        public SubAccountConverter(OneSpanSign.API.SubAccount apiSubAccount)
        {
            this.apiSubAccount = apiSubAccount;
        }

        public SubAccount ToSDKSubAccount()
        {
            if (sdkSubAccount != null)
            {
                return sdkSubAccount;
            }
            else if (apiSubAccount != null)
            {
                SubAccountBuilder builder = SubAccountBuilder.NewSubAccount()
                    .WithName(apiSubAccount.Name)
                    .WithLanguage(apiSubAccount.Language)
                    .WithTimezoneId(apiSubAccount.TimezoneId)
                    .WithParentAccountId(apiSubAccount.ParentAccountId);
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.SubAccount ToAPISubAccount()
        {
            if (apiSubAccount != null)
            {
                return apiSubAccount;
            }
            else if (sdkSubAccount != null)
            {
                OneSpanSign.API.SubAccount result = new OneSpanSign.API.SubAccount();
                result.Name = sdkSubAccount.Name;
                result.Language = sdkSubAccount.Language;
                result.TimezoneId = sdkSubAccount.TimezoneId;
                result.ParentAccountId = sdkSubAccount.ParentAccountId;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}