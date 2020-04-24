using OneSpanSign.API;

namespace OneSpanSign.Sdk.Builder
{
    public class SubAccountBuilder
    {
        private string name;
        private string parentAccountId;
        private string language;
        private string timezoneId;

        private SubAccountBuilder() {}

        public static SubAccountBuilder NewSubAccount() {
            return new SubAccountBuilder();
        }

        public SubAccountBuilder WithName( string value ) {
            this.name = value;
            return this;
        }

        public SubAccountBuilder WithParentAccountId( string value ) {
            this.parentAccountId = value;
            return this;
        }

        public SubAccountBuilder WithLanguage( string value ) {
            this.language = value;
            return this;
        }

        public SubAccountBuilder WithTimezoneId( string value ) {
            this.timezoneId = value;
            return this;
        }
        

        public SubAccount Build() {
            SubAccount subAccount = new SubAccount();
            subAccount.Name = name;
            subAccount.ParentAccountId = parentAccountId;
            subAccount.Language = language;
            subAccount.TimezoneId = timezoneId;
            return subAccount;
        }
    }
}