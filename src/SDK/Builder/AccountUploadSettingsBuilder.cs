using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
    public class AccountUploadSettingsBuilder
    {
        private List<string> _allowedFileTypes;
        
        private AccountUploadSettingsBuilder()
        {
        }
        
        public static AccountUploadSettingsBuilder NewAccountUploadSettings()
        {
            return new AccountUploadSettingsBuilder();
        }
        
        public AccountUploadSettingsBuilder WithAllowedFileTypes(List<string> value)
        {
            this._allowedFileTypes = value;
            return this;
        }

        public AccountUploadSettings Build()
        {
            AccountUploadSettings result = new AccountUploadSettings();
            result.AllowedFileTypes = _allowedFileTypes;
            return result;
        }
    }
}