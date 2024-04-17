using System;

namespace OneSpanSign.Sdk
{
    public class AccountSystemSettingPropertiesBuilder
    {
        private Nullable<int> _senderLoginMaxFailedAttempts;
        private Nullable<int> _loginSessionTimeout;
        private Nullable<int> _sessionTimeoutWarning;
        private Nullable<bool> _orderLastNameFirstName;
        
        private AccountSystemSettingPropertiesBuilder()
        {
        }

        public static AccountSystemSettingPropertiesBuilder NewAccountSystemSettingPropertiesBuilder()
        {
            return new AccountSystemSettingPropertiesBuilder();
        }
        
        public AccountSystemSettingPropertiesBuilder WithSenderLoginMaxFailedAttempts(int senderLoginMaxFailedAttempts)
        {
            this._senderLoginMaxFailedAttempts = senderLoginMaxFailedAttempts;
            return this;
        }
        
        public AccountSystemSettingPropertiesBuilder WithLoginSessionTimeout(int loginSessionTimeout)
        {
            this._loginSessionTimeout = loginSessionTimeout;
            return this;
        }
        
        public AccountSystemSettingPropertiesBuilder WithSessionTimeoutWarning(int sessionTimeoutWarning)
        {
            this._sessionTimeoutWarning = sessionTimeoutWarning;
            return this;
        }

        public AccountSystemSettingPropertiesBuilder WithOrderLastNameFirstName(Boolean orderLastNameFirstName)
        {
            this._orderLastNameFirstName = orderLastNameFirstName;
            return this;
        }
        
        public AccountSystemSettingProperties Build()
        {
            AccountSystemSettingProperties result = new AccountSystemSettingProperties();
            result.SenderLoginMaxFailedAttempts = _senderLoginMaxFailedAttempts;
            result.LoginSessionTimeout = _loginSessionTimeout;
            result.SessionTimeoutWarning = _sessionTimeoutWarning;
            result.OrderLastNameFirstName = _orderLastNameFirstName;
            return result;
        }
    }
}