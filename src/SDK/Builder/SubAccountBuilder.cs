using OneSpanSign.API;

namespace OneSpanSign.Sdk.Builder
{
    public class SubAccountBuilder
    {
        private string name;
        private string parentAccountId;
        private string language;
        private string timezoneId;

        private SubAccountBuilder()
        {
        }

        public static SubAccountBuilder NewSubAccount()
        {
            return new SubAccountBuilder();
        }

        public SubAccountBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public SubAccountBuilder WithParentAccountId(string parentAccountId)
        {
            this.parentAccountId = parentAccountId;
            return this;
        }

        public SubAccountBuilder WithLanguage(string lanuguage)
        {
            this.language = lanuguage;
            return this;
        }

        public SubAccountBuilder WithTimezoneId(string timezoneId)
        {
            this.timezoneId = timezoneId;
            return this;
        }


        public SubAccount Build()
        {
            SubAccount subAccount = new SubAccount();
            subAccount.Name = name;
            subAccount.ParentAccountId = parentAccountId;
            subAccount.Language = language;
            subAccount.TimezoneId = timezoneId;
            return subAccount;
        }
    }
}