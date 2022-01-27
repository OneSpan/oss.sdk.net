namespace OneSpanSign.Sdk
{
    public class AccountSettings
    {
        private AccountFeatureSettings accountFeatureSettings;
        private AccountPackageSettings accountPackageSettings;
 
        public AccountFeatureSettings AccountFeatureSettings
        {
            get
            {
                return accountFeatureSettings;
            }
            set
            {
                accountFeatureSettings = value;
            }
        }
        
        public AccountPackageSettings AccountPackageSettings
        {
            get
            {
                return accountPackageSettings;
            }
            set
            {
                accountPackageSettings = value;
            }
        }
        
    }
}