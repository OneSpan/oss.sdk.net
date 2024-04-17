using System;

namespace OneSpanSign.Sdk
{
    public class AccountSystemSettingProperties
    {
        private Nullable<int> _senderLoginMaxFailedAttempts;
        private Nullable<int> _loginSessionTimeout;
        private Nullable<int> _sessionTimeoutWarning;
        private Nullable<bool> _orderLastNameFirstName;
        
        public Nullable<int> SenderLoginMaxFailedAttempts
        {
            get
            {
                return _senderLoginMaxFailedAttempts;
            }
            set
            {
                _senderLoginMaxFailedAttempts = value;
            }
        }
        
        public Nullable<int> LoginSessionTimeout
        {
            get
            {
                return _loginSessionTimeout;
            }
            set
            {
                _loginSessionTimeout = value;
            }
        }
        
        public Nullable<int> SessionTimeoutWarning
        {
            get
            {
                return _sessionTimeoutWarning;
            }
            set
            {
                _sessionTimeoutWarning = value;
            }
        }
        
        public Nullable<Boolean> OrderLastNameFirstName
        {
            get
            {
                return _orderLastNameFirstName;
            }
            set
            {
                _orderLastNameFirstName = value;
            }
        }
    }
}