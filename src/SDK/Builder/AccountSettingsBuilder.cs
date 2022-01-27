namespace OneSpanSign.Sdk
{
    public class AccountSettingsBuilder
    {

        private AccountFeatureSettings accountFeatureSettings;
        private AccountPackageSettings accountPackageSettings;
       
        private AccountSettingsBuilder()
        {
        }

        public static AccountSettingsBuilder NewAccountSettings() {
            return new AccountSettingsBuilder();
        }

        public AccountSettingsBuilder WithAccountFeatureSettings(AccountFeatureSettings accountFeatureSettings) {
            this.accountFeatureSettings = accountFeatureSettings;
            return this;
        }

        public AccountSettingsBuilder WithAccountPackageSettings(AccountPackageSettings accountPackageSettings) {
            this.accountPackageSettings = accountPackageSettings;
            return this;
        }

        public AccountSettings Build() {
            AccountSettings result = new AccountSettings();
            result.AccountFeatureSettings = accountFeatureSettings;
            result.AccountPackageSettings = accountPackageSettings;

            return result;
        }
    }
}