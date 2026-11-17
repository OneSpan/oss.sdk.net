using System;

namespace OneSpanSign.Sdk
{
    public class AccountSystemSettingProperties
    {
        private Nullable<int> _senderLoginMaxFailedAttempts;
        private Nullable<int> _loginSessionTimeout;
        private Nullable<int> _sessionTimeoutWarning;
        private Nullable<bool> _orderLastNameFirstName;
        private string _signingDateDefaultFormat;
        private Nullable<bool> _signingDateAllowSenderChange;
        private Nullable<bool> _signingDateAllowCustomPattern;

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

        public string SigningDateDefaultFormat
        {
            get
            {
                return _signingDateDefaultFormat;
            }
            set
            {
                _signingDateDefaultFormat = value;
            }
        }

        public Nullable<Boolean> SigningDateAllowSenderChange
        {
            get
            {
                return _signingDateAllowSenderChange;
            }
            set
            {
                _signingDateAllowSenderChange = value;
            }
        }

        public Nullable<Boolean> SigningDateAllowCustomPattern
        {
            get
            {
                return _signingDateAllowCustomPattern;
            }
            set
            {
                _signingDateAllowCustomPattern = value;
            }
        }
    }
}